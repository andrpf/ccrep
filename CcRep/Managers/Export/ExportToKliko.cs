using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CcRep.Components.Correction;
using CcRep.Models.vk;
using CcRep.Models._dc;
using CcRep.Components.Handlers;
using LinqKit;

namespace CcRep.Managers.Export
{
    public class ExportToKliko
    {
        CcRepContext db;

        const string INCLUDE = "INCLUDE";
        const string ARCHIVE = "ARCHIVE";
        const string PERSON = "ФЛ";
        const string CODE_VO_PERSON = "99999";
        const string FIRST_FILIAL = "0001";
        const string FORM_406 = "406";
        const string FORM_405 = "405";
       
        const string FIRST_LINE_FORM406 = "<F406>";
        const string FIRST_LINE_FORM405 = "<F405 {0}N>";
        const string empty_true_condition = "0 == 0";

        readonly string[] Filials = new string[]
        {
            "Все филиалы",
            "Все филиалы, кроме ГО",
            "Конкретный филиал"
        };

        readonly string[] TypeClients = new string[]
        {
            "Все",
            "ФЛ",
            "Не ФЛ"
        };

        public ExportToKliko(CcRepContext db)
        {
            this.db = db;
        }

        string GetFilialFilter(string filials)
        {
            if (Filials[0] == filials)
                return empty_true_condition;

            if (Filials[1] == filials)
                return String.Format("Filial != \"{0}\"", FIRST_FILIAL);
           
            return String.Format("Filial == \"{0}\"",  filials);
        }

        string GetTypeClientFilter(string type_clients)
        {
            if (TypeClients[0] == type_clients)
                return empty_true_condition;

            if (TypeClients[1] == type_clients)
                return String.Format("TypeRez == \"{0}\"", TypeClients[1]);

            if (TypeClients[2] == type_clients)
                return String.Format("TypeRez != \"{0}\"", TypeClients[1]);

            return string.Empty;
        }

        public string CreateKlikoFiles(HeaderRep headerRep, string export_path, string filials, string type_clients)
        {
            try
            {
                export_path = export_path.TrimEnd(new char[] { '\\', '/' }) + @"\";
                string filterFilial = GetFilialFilter(filials);
                Expression<Func<BasicRep, bool>> CheckFilials = System.Linq.Dynamic.DynamicExpression.ParseLambda<BasicRep, bool>(filterFilial);

                string filterTypeClients = GetTypeClientFilter(type_clients);
                Expression<Func<ClientRep, bool>> CheckTypeClients = System.Linq.Dynamic.DynamicExpression.ParseLambda<ClientRep, bool>(filterTypeClients);

                var Operations = (from ar in db.AddInfReps.AsExpandable()
                                  join br in db.BasicReps.AsExpandable() on ar.IdOper equals br.IdOper
                                  join cr in db.ClientReps.AsExpandable() on ar.IdOper equals cr.IdOper
                                  join ir_left in db.IssuerReps.AsExpandable() on ar.IdOper equals ir_left.IdOper into ir_1
                                  join nr_left in db.NoticeReps.AsExpandable() on ar.IdOper equals nr_left.IdOper into nr_1
                                  join r406_left in db.Rep406s.AsExpandable() on ar.IdOper equals r406_left.IdOper into r406_1
                                  from nr in nr_1.DefaultIfEmpty()
                                  from ir in ir_1.DefaultIfEmpty()
                                  from r406 in r406_1.DefaultIfEmpty()
                                  where headerRep.Id == ar.IdRep && ar.Status == INCLUDE &&
                                  CheckFilials.Invoke(br) && (cr == null || CheckTypeClients.Invoke(cr))
                                  orderby br.Filial, br.PostDate, br.CodeVO
                                  select new KlikoInfo
                                  {
                                      Form = ar.Form,
                                      Section = ar.Section,
                                      // BasicRep
                                      Filial = br.Filial,
                                      PostDate = br.PostDate,
                                      CodeVO = (cr.TypeRez == PERSON ? CODE_VO_PERSON : br.CodeVO),
                                      TypeTooling = br.TypeTooling,
                                      OperType = br.OperType,
                                      DirectionPay = br.DirectionPay,
                                      Count = br.Count,
                                      Share = br.Share,
                                      Ccy = br.Ccy,
                                      AmountAll = br.AmountAll,
                                      AmntIncome = br.AmntIncome,
                                      ValueDate = br.ValueDate,
                                      Swift = br.Swift,
                                      SwiftFil = br.SwiftFil,
                                      // ClientRep
                                      BicPartner = cr.BicPartner,
                                      CountryRez = cr.CountryRez,
                                      NameRez = cr.NameRez,
                                      TypeRez = cr.TypeRez,
                                      INN = cr.INN,
                                      NameNerez = cr.NameNerez,
                                      TypeNerez = cr.TypeNerez,
                                      CountryNerez = cr.CountryNerez,
                                      BankName = cr.BankName,
                                      CountryBank = cr.CountryBank,
                                      PartnerName = cr.PartnerName,
                                      CountryPartner = cr.CountryPartner,
                                      // IssuerRep
                                      IssuerName = (ir == null ? string.Empty : ir.IssuerName),
                                      SecurityCode = (ir == null ? string.Empty : ir.SecurityCode),
                                      RegNumIssuer = (ir == null ? string.Empty : ir.RegNumIssuer),
                                      DateRegIssuer = (ir == null ? null : ir.DateRegIssuer),
                                      RepayDate = (ir == null ? null : ir.RepayDate),
                                      CcyIssuer = (ir == null ? null : ir.CcyIssuer),
                                      IssuerCode = (ir == null ? null : ir.IssuerCode),
                                      IssuerReestr = (ir == null ? null : ir.IssuerReestr),
                                      // NoticeRep
                                      Notice = (nr == null ? string.Empty : nr.Notice),
                                      NoticeIssuer = (nr == null ? string.Empty : nr.NoticeIssuer),
                                      NoticeExchange = (nr == null ? string.Empty : nr.NoticeExchange),
                                      NoticeInst = (nr == null ? string.Empty : nr.NoticeInst),
                                      NoticeProperty = (nr == null ? string.Empty : nr.NoticeProperty),
                                      NoticeBank = (nr == null ? string.Empty : nr.NoticeBank),
                                      // VK_406_REP
                                      OperKind = (r406 == null ? null : r406.OperKind),
                                      AmountTo = (r406 == null ? null : r406.AmountTo),
                                      AmountFrom = (r406 == null ? null : r406.AmountFrom),
                                      BicBank = (r406 == null ? null : r406.BicBank),
                                      CountryBank406 = (r406 == null ? null : r406.CountryBank406),
                                      AddCode10 = (r406 == null ? null : r406.AddCode10),
                                      KindRez = (r406 == null ? null : r406.KindRez),
                                      KindNerez = (r406 == null ? null : r406.KindNerez),
                                      KindContract = (r406 == null ? null : r406.KindContract),
                                      TypeContract = (r406 == null ? null : r406.TypeContract),
                                      Notice15 = (r406 == null ? null : r406.Notice15),
                                      Notice16 = (r406 == null ? null : r406.Notice16),
                                      Notice40 = (r406 == null ? null : r406.Notice40),
                                      NoticeOther = (r406 == null ? null : r406.NoticeOther)
                                  }).ToList();

                // Коррекция свойств
                CorrectProperties.Correction<KlikoInfo>(Operations);

                // Создание файлов
                List<string> sections = GetSections();
                string[] sections_for_names = sections.ConvertAll<string>(x => x = x.Replace(".", "")).ToArray();
                string[] file_names = CreateFileNames(type_clients, headerRep, sections_for_names);
                bool[] first_tags = Enumerable.Repeat(true, file_names.Length).ToArray();

                List<KlikoInfo> Operations406 = Operations.FindAll(x => x.Form == FORM_406);
                if (Operations406.Count > 0)
                {
                    string file406 = export_path + file_names[0];
                    using (StreamWriter file = new StreamWriter(file406, false, Encoding.GetEncoding(866)))
                    {
                        file.WriteLine(FIRST_LINE_FORM406);
                        foreach (KlikoInfo info in Operations406)
                            AddOperationToFile(file, GetAttributesForm406Array(info));
                    }
                }

                for (int i = 0; i < sections.Count; i++)
                {
                    List<KlikoInfo> Operations405 = Operations.FindAll(x => x.Form == FORM_405 && x.Section == sections[i]);
                    if (Operations405.Count > 0)
                    {
                        string file405 = export_path + file_names[i + 1];
                        using (StreamWriter file = new StreamWriter(file405, true, Encoding.GetEncoding(866)))
                        {
                            file.WriteLine(String.Format(FIRST_LINE_FORM405, sections[i][0]));
                            foreach (KlikoInfo info in Operations405)
                                AddOperationToFile(file, GetAttributesForm405Array(info));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ошибка при экспорте в КЛИКО: " + ex.Message;
            }


            return "true";
        }

        List<string> GetSections()
        {
            var sections = (from ar in db.AddInfReps select ar.Section).ToList();
            return sections;
        }

        string GetFlPrefix(string type_clients)
        {
            string fl_prefix = string.Empty;
            if (type_clients == TypeClients[1])
                fl_prefix = "_FL";
            else
            if (type_clients == TypeClients[2])
                fl_prefix = "_UL";

            return fl_prefix;
        }

        string[] CreateFileNames(string type_clients, HeaderRep headerRep, string[] sections)
        {
            string[] name_files = new string[1 + sections.Length];
            string date = headerRep.BeginDate.ToString("MMyy");
            string fl_prefix = GetFlPrefix(type_clients);

            for (int i = 0; i < name_files.Length; i++)
            {
                name_files[i] = String.Format("{0}{1}{2}{3}.txt", i == 0 ? FORM_406 : FORM_405,
                                                               i == 0 ? string.Empty : sections[i - 1],
                                                               date, fl_prefix);
            }

            return name_files;
        }

        string[] GetAttributesForm406Array(KlikoInfo inf)
        {
            var arr = new string[] { inf.Filial, inf.PostDate.ToStr(1), inf.OperKind.ToString(),
                                          inf.Notice40, inf.Ccy,
                                          inf.AmountTo.ToStr(), inf.AmountFrom.ToStr(),
                                          inf.BicBank, inf.Swift, inf.SwiftFil, inf.CountryBank406,
                                          inf.NameRez, inf.AddCode10, inf.INN, inf.KindRez, inf.NameNerez,
                                          inf.CountryNerez, inf.KindNerez, inf.KindContract, inf.TypeContract, inf.Notice15,
                                          inf.Notice16, inf.NoticeOther};
            return arr;
        }



        string[] GetAttributesForm405Array(KlikoInfo inf)
        {
            var list = new List<string> { inf.Filial, inf.PostDate.ToStr(1), inf.CodeVO, inf.TypeTooling,
                                          inf.OperType.ToString(), inf.DirectionPay, inf.Count.ToString(),
                                          inf.Share.ToStr("0"), inf.Ccy, inf.AmountAll.ToStr(),
                                          inf.AmntIncome.ToStr(), inf.BicPartner, inf.CountryRez,
                                          inf.Swift, inf.SwiftFil, inf.NameRez, inf.TypeRez, inf.INN, inf.NameNerez,
                                          inf.TypeNerez, inf.CountryNerez, inf.IssuerName, inf.SecurityCode, inf.RegNumIssuer,
                                          inf.DateRegIssuer.ToStr(1), inf.RepayDate.ToStr(1), inf.CcyIssuer,
                                          inf.IssuerCode.ToString(), inf.IssuerReestr,
                                          inf.Notice, inf.NoticeIssuer, inf.NoticeExchange, inf.NoticeInst, inf.NoticeProperty,
                                          inf.NoticeBank, inf.BankName, inf.CountryBank, inf.CountryPartner };

            if (inf.Section[0] == '1')
                list.Add(inf.ValueDate.ToStr(1));

            return list.ToArray();
        }

        void AddOperationToFile(StreamWriter file, string[] arr)
        {
            StringBuilder sb_format = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                sb_format.Append("\"{");
                sb_format.Append(i);
                sb_format.Append("}\",");
            }
            string format = sb_format.ToString().TrimEnd(',');
            string line = String.Format(format, arr);
            file.WriteLine(line);
        }
    }
}