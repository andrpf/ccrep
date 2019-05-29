using Microsoft.AspNet.Identity;
using CcRep.Areas.Reports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CcRep.Models._dc;
using CcRep.Models.vk;
using System.Linq.Dynamic;
using CcRep.Components.Correction;
using CcRep.Components.Handlers;
using System.Web;
using LinqKit;

namespace CcRep.Managers.Transactions
{
    public class GridDataHandler
    {
        CcRepContext db;

        public string ErrorString { get; private set; }

        const string DUPLICATE_STR = "DUPLICATE";
      //  const string NEW_STR = "NEW";
        const string CLAIM_VALUE_ALL = "*";
        const string CLAIM_VALUE_TRUE = "True";

        public GridDataHandler(CcRepContext db)
        {
            this.db = db;
        }

        public struct ResponseData
        {
            public int recordsTotal;
            public int recordsFiltered;
            public string[][] data;
        }

        ResponseData data_empty = new ResponseData
        {   recordsTotal = 0, recordsFiltered = 0, data = new string[][]{ }  };

        string GetClassName(string full_name)
        {
            string[] names = full_name.Split('_');
            return names[0];
        }

        QueryBuildRule CreateParam(string id, string type, string _operator, string value)
        {
            return new QueryBuildRule() {
                id = id,
                type = type,
                _operator = _operator,
                value = value
            };
        }

        public ResponseData GetData(FindTranDataModel req, HeaderRep headerRep, bool is_test = false)
        {
            string user_id = CurrentUserId;
           
            if (!is_test && user_id == null)
            {
                ErrorString = "Не был определен пользователь для входа в систему.";
                return data_empty;
            }

            try
            {
                // Установка всех фильтров
                var filterAddInfRep = new HandleFilterParams<AddInfRep>();
                var filterBasicRep  = new HandleFilterParams<BasicRep>();
                var filterClientRep = new HandleFilterParams<ClientRep>();
                var filterIssuerRep = new HandleFilterParams<IssuerRep>();
                var filterNoticeRep = new HandleFilterParams<NoticeRep>();
                var filterAddRep    = new HandleFilterParams<AddRep>();
                var filterRep406    = new HandleFilterParams<Rep406>();
                var filterSupdocRep = new HandleFilterParams<SupdocRep>();

                bool is_rep_406 = false;
                bool is_cr = false;
                bool is_ir = false;
                bool is_nr = false;
                bool is_add_rep = false;
                bool is_sr = false;
            
                // Условие фильтра дубликатов и статуса NEW
                //  if (!req.onlyDuple)
                //      filterAddInfRep.AddField(CreateParam("AddInfRep_Status", "string", "not_equal", NEW_STR));

                filterAddInfRep.AddField(CreateParam("AddInfRep_Status", "string",
                             req.onlyDuple ? "equal" : "not_equal", DUPLICATE_STR));

                // Добавить правила
                if (req.Rules != null)
                    foreach (QueryBuildRule qbr in req.Rules)
                    {
                        string table_name = GetClassName(qbr.id);

                        if (table_name == "AddInfRep")
                            filterAddInfRep.AddField(qbr);
                        else
                        if (table_name == "BasicRep")
                            filterBasicRep.AddField(qbr);
                        else
                        if (table_name == "ClientRep")
                        {
                            filterClientRep.AddField(qbr);
                            is_cr = true;
                        }
                        else
                        if (table_name == "IssuerRep")
                        {
                            filterIssuerRep.AddField(qbr);
                            is_ir = true;
                        }
                        else
                        if (table_name == "NoticeRep")
                        {
                            filterNoticeRep.AddField(qbr);
                            is_nr = true;
                        }
                        else
                        if (table_name == "AddRep")
                        {
                            filterAddRep.AddField(qbr);
                            is_add_rep = true;
                        }
                        else
                        if (table_name == "Rep406")
                        {
                            filterRep406.AddField(qbr);
                            is_rep_406 = true;
                        }
                        else
                        if (table_name == "SupdocRep")
                        {
                            filterSupdocRep.AddField(qbr);
                            is_sr = true;
                        }
                        else
                            throw new ApplicationException("Несуществующее имя таблицы: " + table_name);
                    }


                // Получить все выражения
                bool all = false;
                var check_filials = filterBasicRep.GetExpression(GetFilterFilials(user_id, out all), "Filial", all);
                var check_clients = filterClientRep.GetExpression(GetFilterClients(user_id, out all), "TypeRez", all);
                var check_supdocs = filterAddInfRep.GetExpression(GetFilterSupdocs(user_id, out all), "SupdocFl", all);

                var check_addinf_rep = filterAddInfRep.GetExpression();
                var check_basic_rep = filterBasicRep.GetExpression();
                var check_client_rep = filterClientRep.GetExpression();
                var check_issuer_rep = filterIssuerRep.GetExpression();
                var check_notice_rep = filterNoticeRep.GetExpression();
                var check_add_rep = filterAddRep.GetExpression();
                var check_rep_406 = filterRep406.GetExpression();
                var check_supdoc_rep = filterSupdocRep.GetExpression();

                int total_records = db.AddInfReps.Where(x => x.IdRep == headerRep.Id).Select(x => x.IdOper).Count();

                // Запрос операций, удовлетворяющих всем условиям всех фильтров.
                var Operations = from ar in db.AddInfReps.AsExpandable()
                                 join br in db.BasicReps.AsExpandable() on ar.IdOper equals br.IdOper
                                 join cr_left in db.ClientReps.AsExpandable() on ar.IdOper equals cr_left.IdOper into cr_1
                                 join ir_left in db.IssuerReps.AsExpandable() on ar.IdOper equals ir_left.IdOper into ir_1
                                 join nr_left in db.NoticeReps.AsExpandable() on ar.IdOper equals nr_left.IdOper into nr_1
                                 join r406_left in db.Rep406s.AsExpandable() on ar.IdOper equals r406_left.IdOper into r406_1
                                 join sr_left in db.SupdocReps.AsExpandable() on ar.IdOper equals sr_left.IdOper into sr_1
                                 join add_rep_left in db.AddReps.AsExpandable() on ar.IdOper equals add_rep_left.IdOper into add_rep_1
                                 from cr in cr_1.DefaultIfEmpty()
                                 from nr in nr_1.DefaultIfEmpty()
                                 from ir in ir_1.DefaultIfEmpty()
                                 from r406 in r406_1.DefaultIfEmpty()
                                 from sr in sr_1.DefaultIfEmpty()
                                 from add_rep in add_rep_1.DefaultIfEmpty()
                                 where headerRep.Id == ar.IdRep && (!is_rep_406 || (is_rep_406 && ar.Form == "406")) &&
                                        (!is_cr || (is_cr && cr != null)) && (!is_ir || (is_ir && ir != null)) &&
                                        (!is_nr || (is_nr && nr != null)) && (!is_add_rep || (is_add_rep && add_rep != null)) &&
                                        (!is_sr || (is_sr && sr != null)) &&
                                        check_filials.Invoke(br) && (cr == null || check_clients.Invoke(cr)) &&
                                        check_supdocs.Invoke(ar) && check_addinf_rep.Invoke(ar) &&
                                        check_basic_rep.Invoke(br) && (cr == null || check_client_rep.Invoke(cr)) &&
                                        (ir == null || check_issuer_rep.Invoke(ir)) && (nr == null || check_notice_rep.Invoke(nr)) &&
                                        (add_rep == null || check_add_rep.Invoke(add_rep)) && (r406 == null || check_rep_406.Invoke(r406)) &&
                                        (sr == null || check_supdoc_rep.Invoke(sr))
                                 orderby br.Filial, br.PostDate
                                 select new GridData
                                 {
                                     // AddInfRep
                                     Form = ar.Form,
                                     Section = ar.Section,
                                     Status = ar.Status,
                                     SupdocFl = ar.SupdocFl,
                                     DateLock = ar.DateLock,
                                     DateRemove = ar.DateRemove,
                                     NumJD = ar.NumJD,
                                     NumDVK = ar.NumDVK,
                                     DateRequest = ar.DateRequest,
                                     DateGetDoc = ar.DateGetDoc,
                                     CNum = ar.CNum,
                                     CBAccount = ar.CBAccount,

                                     // BasicRep
                                     Filial = br.Filial,
                                     PostDate = br.PostDate,
                                     TypeTooling = br.TypeTooling,
                                     OperType = br.OperType,
                                     DirectionPay = br.DirectionPay,
                                     Count = br.Count,
                                     Share = br.Share,
                                     Ccy = br.Ccy,
                                     AmountAll = br.AmountAll,
                                     AmntIncome = br.AmntIncome,
                                     RefNum = br.RefNum,
                                     CodeVO = br.CodeVO, // (cr.TypeRez == PERSON ? CODE_VO_PERSON : br.CodeVO),
                                     ValueDate = br.ValueDate,
                                     Swift = br.Swift,
                                     SwiftFil = br.SwiftFil,

                                     // ClientRep
                                     NameRez = (cr == null ? string.Empty : cr.NameRez),
                                     TypeRez = (cr == null ? string.Empty : cr.TypeRez),
                                     BicPartner = (cr == null ? string.Empty : cr.BicPartner),
                                     CountryRez = (cr == null ? string.Empty : cr.CountryRez),
                                     INN = (cr == null ? string.Empty : cr.INN),
                                     NameNerez = (cr == null ? string.Empty : cr.NameNerez),
                                     TypeNerez = (cr == null ? string.Empty : cr.TypeNerez),
                                     CountryNerez = (cr == null ? string.Empty : cr.CountryNerez),
                                     BankName = (cr == null ? string.Empty : cr.BankName),
                                     CountryBank = (cr == null ? string.Empty : cr.CountryBank),
                                     PartnerName = (cr == null ? string.Empty : cr.PartnerName),
                                     CountryPartner = (cr == null ? string.Empty : cr.CountryPartner),
                                     CustNo = (cr == null ? string.Empty : cr.CustNo),

                                     // IssuerRep
                                     IssuerName = (ir == null ? string.Empty : ir.IssuerName),
                                     SecurityCode = (ir == null ? string.Empty : ir.SecurityCode),
                                     RegNumIssuer = (ir == null ? string.Empty : ir.RegNumIssuer),
                                     DateRegIssuer = (ir == null ? null : ir.DateRegIssuer),
                                     RepayDate = (ir == null ? null : ir.RepayDate),
                                     CcyIssuer = (ir == null ? string.Empty : ir.CcyIssuer),
                                     IssuerCode = (ir == null ? null : ir.IssuerCode),
                                     IssuerReestr = (ir == null ? string.Empty : ir.IssuerReestr),

                                     // VK_406_REP
                                     OperKind = (r406 == null ? null : r406.OperKind),
                                     Notice40 = (r406 == null ? string.Empty : r406.Notice40),
                                     AmountTo = (r406 == null ? null : r406.AmountTo),
                                     AmountFrom = (r406 == null ? null : r406.AmountFrom),
                                     BicBank = (r406 == null ? string.Empty : r406.BicBank),
                                     CountryBank406 = (r406 == null ? string.Empty : r406.CountryBank406),
                                     AddCode10 = (r406 == null ? string.Empty : r406.AddCode10),
                                     KindRez = (r406 == null ? string.Empty : r406.KindRez),
                                     KindNerez = (r406 == null ? string.Empty : r406.KindNerez),
                                     KindContract = (r406 == null ? string.Empty : r406.KindContract),
                                     TypeContract = (r406 == null ? string.Empty : r406.TypeContract),
                                     Notice15 = (r406 == null ? string.Empty : r406.Notice15),
                                     Notice16 = (r406 == null ? string.Empty : r406.Notice16),
                                     NoticeOther = (r406 == null ? string.Empty : r406.NoticeOther),

                                     // VK_SUPDOC_REP
                                     CodeSupdoc = (sr == null ? string.Empty : sr.CodeSupdoc),
                                     SupdocName = (sr == null ? string.Empty : sr.SupdocName),
                                     Prinvest = (sr == null ? null : sr.Prinvest),
                                     EntryDate = (sr == null ? null : sr.EntryDate),
                                     SupdocType = (sr == null ? string.Empty : sr.SupdocType),
                                     RegNumBranch = (sr == null ? string.Empty : sr.RegNumBranch),
                                     ClientName = (sr == null ? string.Empty : sr.ClientName),
                                     SourceSupdoc = (sr == null ? string.Empty : sr.SourceSupdoc),
                                     PassportNum = (sr == null ? string.Empty : sr.PassportNum),
                                     DocDate = (sr == null ? null : sr.DocDate),
                                     UpdateSdDate = (sr == null ? null : sr.UpdateSdDate),

                                     OperationId = ar.IdOper
                                 };

                int count_rows = Operations.Count();
                GridData[] arr_operations = new GridData[] { };

                if (count_rows != 0)
                {
                    CorrectProperties.Correction<GridData>(Operations);

                    // Добавить правила сортировки полей
                    StringBuilder sb = new StringBuilder();
                    if (req.OrderRules != null)
                        foreach (FindTranOrder fto in req.OrderRules)
                            sb.AppendFormat("{0} {1},", GridData.GetFieldName(fto.column), fto.dir);

                    string order_by = sb.ToString().TrimEnd(',');


                    if (req.StartRowNo >= count_rows)
                    {
                        ErrorString = "Некорректный номер строки для выбора из списка операций.";
                        return data_empty;
                    }

                    int page_length = ((req.StartRowNo + req.PageLength) <= count_rows) ? req.PageLength : (count_rows - req.StartRowNo);

                    // Получение сортированного списка
                    if (!String.IsNullOrEmpty(order_by))
                        Operations = Operations.OrderBy(order_by);

                    arr_operations = Operations.Skip(req.StartRowNo).Take(page_length).ToArray();
                }

                return new ResponseData
                {
                    recordsTotal = total_records, // количество всех записей отчета
                    recordsFiltered = count_rows, // количество отфильтрованных записей
                    data = GetDataArray(arr_operations) // массив со значениями строк
                };
            }
            catch (Exception ex)
            {
                ErrorString = "Некорректное условие в фильтре: " + ex.Message;
                return data_empty;
            }
        }

        string[][] GetDataArray(GridData[] operations)
        {
            var data = new List<string[]>();
            foreach (GridData inf in operations)
            {
                var record = new string[] {
                                            // VK_ADDINF_REP
                                            inf.Form, inf.Section, inf.Status, inf.SupdocFl,
                                            // VK_BASIC_REP
                                            inf.Filial, inf.PostDate.ToStr(0), inf.TypeTooling,
                                            inf.OperType.ToString(), inf.DirectionPay,
                                            inf.Count.ToString(), inf.Share.ToStr("0"),
                                            inf.Ccy, inf.AmountAll.ToStr(),
                                            inf.AmntIncome.ToStr(), inf.RefNum,
                                            inf.CodeVO, inf.ValueDate.ToStr(0),
                                            inf.Swift, inf.SwiftFil,
                                            // VK_CLIENT_REP
                                            inf.NameRez, inf.TypeRez, inf.BicPartner,
                                            inf.CountryRez, inf.INN, inf.NameNerez,
                                            inf.TypeNerez, inf.CountryNerez, inf.BankName,
                                            inf.CountryBank, inf.PartnerName, inf.CountryPartner, inf.CustNo,
                                            // VK_ISSUER_REP
                                            inf.IssuerName, inf.SecurityCode, inf.RegNumIssuer,
                                            inf.DateRegIssuer.ToStr(0), inf.RepayDate.ToStr(0),
                                            inf.CcyIssuer, inf.IssuerCode.ToString(), inf.IssuerReestr,
                                            // VK_406_REP
                                            inf.OperKind.ToString(), inf.Notice40, inf.AmountTo.ToStr(),
                                            inf.AmountFrom.ToStr(), inf.BicBank,
                                            inf.CountryBank406, inf.AddCode10, inf.KindRez, inf.KindNerez,
                                            inf.KindContract, inf.TypeContract, inf.Notice15, inf.Notice16,
                                            inf.NoticeOther,
                                            // VK_SUPDOC_REP
                                            inf.CodeSupdoc, inf.SupdocName, inf.Prinvest.ToString(),
                                            inf.EntryDate.ToStr(0), inf.SupdocType, inf.RegNumBranch,
                                            inf.ClientName, inf.SourceSupdoc, inf.PassportNum,
                                            inf.DocDate.ToStr(0), inf.UpdateSdDate.ToStr(0),
                                            // VK_ADDINF_REP
                                            inf.DateLock.ToStr(0), inf.DateRemove.ToStr(0), inf.NumJD,
                                            inf.NumDVK, inf.DateRequest.ToStr(0), inf.DateGetDoc.ToStr(0), inf.CNum,
                                            inf.CBAccount, inf.OperationId.ToString()
                                       };
                data.Add(record);
            }

            return data.ToArray<string[]>();
        }

        string[] GetFilterFilials(string user_id, out bool all_filials)
        {
            if (user_id == null)
            {
                all_filials = true;
                return null;
            }

            if (db.UserClaims.Where(x => x.UserId == user_id && x.ClaimType == "showAllBranches" &&
                x.ClaimValue == CLAIM_VALUE_TRUE).Any())
            {
                all_filials = true;
                return null;
            }

            var filials = (from uf in db.UserrsToFilials
                           join fl in db.Filials on uf.FilialId equals fl.NCFilial
                           where user_id == uf.UserId
                           select new { p = fl.License }).ToList().ConvertAll(x => x.p).ToArray();
            all_filials = false;
            return filials;
        }

        string[] GetFilterClients(string user_id, out bool all_clients)
        {
            if (user_id == null)
            {
                all_clients = true;
                return null;
            }

            if (db.UserClaims.Where(x => x.UserId == user_id && x.ClaimType == "FlAccess" && x.ClaimValue == CLAIM_VALUE_ALL).Any())
            {
                all_clients = true;
                return null;
            }

            var clients = db.UserClaims.Where(x => x.UserId == user_id && x.ClaimType == "FlAccess").Select(p => p.ClaimValue).ToArray();
            all_clients = false;
            return clients;
        }


        string[] GetFilterSupdocs(string user_id, out bool all_supdocs)
        {
            if (user_id == null)
            {
                all_supdocs = true;
                return null;
            }

            if (db.UserClaims.Where(x => x.UserId == user_id && x.ClaimType == "PdAccess" && x.ClaimValue == CLAIM_VALUE_ALL).Any())
            {
                all_supdocs = true;
                return null;
            }

            var clients = db.UserClaims.Where(x => x.UserId == user_id && x.ClaimType == "PdAccess").Select(p => p.ClaimValue).ToArray();
            all_supdocs = false;
            return clients;
        }

        private static string CurrentUserId
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.User == null
                    || HttpContext.Current.User.Identity == null)
                    return null;

                return HttpContext.Current.User.Identity.GetUserId();
            }
        }
    }
    
}
