using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.IO;

using CcRep.Models._dc;
using CcRep.Models.dic;
using CcRep.Models._db.csv;
using CcRep.Components.StringExt;
using CcRep.Models.vk;
using LinqToExcel;
using System.Data.OleDb;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;

namespace CcRep.Models._db
{
    public partial class CcRepContextDBInitializer : CreateDatabaseIfNotExists<CcRepContext>
    {
        const string dir_sql = @"Migrations\sql\";
        const string dir_csv = @"Migrations\csv\";
        const string dir_xlsx = @"Migrations\xlsx\";
        string path_to_csv = "";
        string path_to_xlsx = "";
        CultureInfo cult_info = CultureInfo.GetCultureInfo("ru-RU");
        string date_format = "dd.MM.yyyy";

        void SetPathToCsvFiles()
        {
            if (String.IsNullOrEmpty(path_to_csv))
            {
                string base_dir = AppDomain.CurrentDomain.BaseDirectory.CutEnd(@"bin\") + @"\";

                if (!Directory.Exists(base_dir))
                    throw new ApplicationException(String.Format("The base directory '{0}' is not exist.", base_dir));

                path_to_csv = base_dir + dir_csv;
                path_to_xlsx = base_dir + dir_xlsx;
            }

            if (!Directory.Exists(path_to_csv))
                throw new ApplicationException(String.Format("The csv-path '{0}' is not exist.",
                                               path_to_csv));
        }

       
        public class ArchiveRow
        {
            public string FILIAL;
            public string POSTDATE;
            public string CODE_VO;
            public string TYPE_TOOLING;
            public string OPER_TYPE;
            public string DIRECTION_PAY;
            public string COUNT;
            public string SHARE;
            public string CCY;
            public string AMOUNT_ALL;
            public string AMNT_INCOME;
            public string BIC_PARTNER;
            public string COUNTRY_REZ;
            public string SWIFT_405;
            public string SWIFT_FIL_405;
            public string NAME_REZ;
            public string TYPE_REZ;
            public string INN;
            public string NAME_NEREZ;
            public string TYPE_NEREZ;
            public string COUNTRY_NEREZ;
            public string ISSUER_NAME;
            public string SECURITY_CODE;
            public string REG_NUM_ISSUER;
            public string DATE_REG_ISSUER;
            public string REPAY_DATE;
            public string CCY_ISSUER;
            public string ISSUER_CODE;
            public string ISSUER_REESTR;
            public string NOTICE;
            public string NOTICE_ISSUER;
            public string NOTICE_EXCHANGE;
            public string NOTICE_INST;
            public string NOTICE_PROPERTY;
            public string NOTICE_BANK;
            public string BANK_NAME;
            public string COUNTRY_BANK;
            public string PARTNER_NAME;
            public string COUNTRY_PARTNER;
            public string VALUEDATE;
            public string REF_NUM;
            public string CCD_USER;
            public string NARRATIVE;
            public string CITY_CODE;
            public string NOTICE_REPORT;
            public string DEP_ACC;
            public string CONTRACT_NUM;
            public string FORM;
            public string SECTION;
            public string STATUS;
            public string ID_REP;
            public string AUTOR;
            public string SOURCE;
            public string SUPDOC_FL;
            public string SOURCE_NAME;
        }

        string[] fileLoadArchive = new string[]
        {
              "ArchiveAccessForLoad.xlsx:DataAccess",
              "Archive_405_ForLoad.xlsx:раздел_1,раздел_2"
        };



        private void LoadArchiveFromExcel(CcRepContext context)
        {
            for (int i = 0; i < fileLoadArchive.Length; i++ )
            {
                string[] param = fileLoadArchive[i].Split(':');

                string pathToExcelFile = path_to_xlsx + param[0];

                var excelFile = new ExcelQueryFactory(pathToExcelFile);

                string[] sheets = param[1].Split(',');

                List<ArchiveRow> dataArchive;

                //*********
                
                var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);

                var adapter = new OleDbDataAdapter(String.Format("SELECT * FROM [{0}$]", sheets[0]), connectionString);
                var ds = new DataSet();

                adapter.Fill(ds, "ExcelTable");

                DataTable dtable = ds.Tables["ExcelTable"];

                //*************************

                foreach (string sheetName in sheets)
                {
                    try
                    {
                        dataArchive = (from a in excelFile.Worksheet<ArchiveRow>(sheetName) select a).ToList();
                     //   dataArchive = excelFile.Worksheet<ArchiveRow>(sheetName).ToList();
                    }
                    catch
                    {
                        return;
                    }

                    int k = 0;

                    foreach (var a in dataArchive)
                    {
                        k++;

                        try
                        {
                            AddInfRep ai = new AddInfRep();
                            ai.Form = a.FORM;
                            ai.Section = a.SECTION;
                            ai.Status = a.STATUS;
                            ai.IdRep = Convert.ToInt32(a.ID_REP);
                            ai.Author = a.AUTOR;
                            ai.Source = a.SOURCE;
                            ai.SupdocFl = a.SUPDOC_FL;
                            ai.SourceName = a.SOURCE_NAME;
                            ai.PaymentRK = k;
                            ai.IrbPaymentRK = k;

                            context.AddInfReps.Add(ai);

                            context.SaveChanges();

                            AddInfRep last_ai = context.AddInfReps.Last<AddInfRep>();

                            BasicRep br = new BasicRep();
                            br.IdOper = last_ai.IdOper;
                            br.Filial = a.FILIAL.PadLeft(4, '0'); ;
                            br.PostDate = DateTime.ParseExact(a.POSTDATE, date_format, cult_info);
                            br.CodeVO = a.CODE_VO;
                            br.TypeTooling = a.TYPE_TOOLING;
                            br.OperType = Convert.ToByte(a.OPER_TYPE);
                            br.Count = Convert.ToInt64(a.COUNT);
                            br.Share = Convert.ToDecimal(a.SHARE, cult_info);
                            br.Ccy = a.CCY;
                            br.AmountAll = Convert.ToDecimal(a.AMOUNT_ALL, cult_info);
                            br.AmntIncome = Convert.ToDecimal(a.AMNT_INCOME, cult_info);
                            br.Swift = a.SWIFT_405;
                            br.SwiftFil = a.SWIFT_FIL_405;
                            br.ValueDate = DateTime.ParseExact(a.VALUEDATE, date_format, cult_info);
                            br.RefNum = a.REF_NUM;
                            context.BasicReps.Add(br);

                            ClientRep cr = new ClientRep();
                            cr.IdOper = last_ai.IdOper;
                            cr.BicPartner = a.BIC_PARTNER;
                            cr.CountryRez = a.COUNTRY_REZ;
                            cr.NameRez = a.NAME_REZ;
                            cr.TypeRez = a.TYPE_REZ;
                            cr.INN = a.INN;
                            cr.NameNerez = a.NAME_NEREZ;
                            cr.TypeNerez = a.TYPE_NEREZ;
                            cr.CountryNerez = a.COUNTRY_NEREZ;
                            cr.BankName = a.BANK_NAME;
                            cr.CountryBank = a.COUNTRY_BANK;
                            cr.PartnerName = a.PARTNER_NAME;
                            cr.CountryPartner = a.COUNTRY_PARTNER;
                            context.ClientReps.Add(cr);

                            IssuerRep ir = new IssuerRep();
                            ir.IdOper = last_ai.IdOper;
                            ir.IssuerName = a.ISSUER_NAME;
                            ir.SecurityCode = a.SECURITY_CODE;
                            ir.RegNumIssuer = a.REG_NUM_ISSUER;
                            ir.DateRegIssuer = DateTime.ParseExact(a.DATE_REG_ISSUER, date_format, cult_info);
                            ir.RepayDate = DateTime.ParseExact(a.REPAY_DATE, date_format, cult_info);
                            ir.CcyIssuer = a.CCY_ISSUER;
                            ir.IssuerCode = Convert.ToByte(a.ISSUER_CODE);
                            ir.IssuerReestr = a.ISSUER_REESTR;
                            context.IssuerReps.Add(ir);

                            NoticeRep nr = new NoticeRep();
                            nr.IdOper = last_ai.IdOper;
                            nr.Notice = a.NOTICE;
                            nr.NoticeIssuer = a.NOTICE_ISSUER;
                            nr.NoticeExchange = a.NOTICE_EXCHANGE;
                            nr.NoticeInst = a.NOTICE_INST;
                            nr.NoticeProperty = a.NOTICE_PROPERTY;
                            nr.NoticeBank = a.NOTICE_BANK;

                            AddRep ar = new AddRep();
                            ar.IdOper = last_ai.IdOper;
                            ar.CcdUser = a.CCD_USER;
                            ar.Narrative = a.NARRATIVE;
                            ar.CityCode = a.CITY_CODE;
                            ar.NoticeReport = a.NOTICE_REPORT;
                            ar.DepAcc = a.DEP_ACC;
                            ar.ContractNum = a.CONTRACT_NUM;

                            context.SaveChanges();
                        }

                        catch (DbEntityValidationException ex)
                        {
                            throw new Exception("[LoadArchiveFromExcel] " + ex.Message);
                        }
                    }
                }
            }
        }
        

        protected override void Seed(CcRepContext context)
        {
            IList<Section> defaultSections = new List<Section>() {
                    new Section() { _Section = "1.1" },
                    new Section() { _Section = "1.2" },
                    new Section() { _Section = "2.1" },
                    new Section() { _Section = "2.2" }
                };

            context.Sections.AddRange(defaultSections);
            SetPathToCsvFiles();

            CcRepCsvParser csvParser = new CcRepCsvParser(path_to_csv);

            // Asp roles
            var defaultAspRoles = csvParser.CsvParserStart<CcRepRole>(new CsvCcRepRoleMapping(),
                                                "dic_asp_roles.csv");
            ((DbSet<CcRepRole>)context.Roles).AddRange(defaultAspRoles);

            // AcctReport (ok) and ClientType (ok)
            var defaultAcctReports = csvParser.CsvParserStart<AcctReport>(new CsvAcctReportMapping(),
                                                  "dic_acc_report.csv");

            var tempClientTypes = csvParser.CsvParserStart<ClientType>(new CsvClientTypeAcctReportMapping(),
                                                        "dic_acc_report.csv");


            var defaultClientTypes = csvParser.CsvParserStart<ClientType>(new CsvClientTypeMapping(),
                                                        "dic_client_types.csv");

            {   // Fill the table DIC_ACCT_REPORT_TO_CLIENT_TYPE
                for (int i = 0; i < tempClientTypes.Count; i++)
                {
                    if (tempClientTypes[i] == null || defaultAcctReports[i] == null ||
                        String.IsNullOrEmpty(tempClientTypes[i].NameShort))
                        continue;

                    foreach (ClientType ct in defaultClientTypes)
                    {
                        if (ct == null)
                            continue;

                        if (tempClientTypes[i].NameShort.Equals(ct.NameShort))
                        {
                            defaultAcctReports[i].ClientTypes.Add(ct);
                            break;
                        }
                    }
                }
            }

            context.ClientTypes.AddRange(defaultClientTypes);
            context.AcctReports.AddRange(defaultAcctReports);

            //  context.ClientTypes.AddOrUpdate<ClientType>(defaultClientTypes.ToArray<ClientType>());
            //  context.AcctReports.AddOrUpdate<AcctReport>(defaultAcctReports.ToArray<AcctReport>());

            // AddValue10 (ok)
            var defaultAddValue10 = csvParser.CsvParserStart<AddValue10>(new CsvAddValue10Mapping(),
                                                        "dic_add_value_10.csv");
            context.AddValues10.AddRange(defaultAddValue10);
            //  context.AddValues10.AddOrUpdate<AddValue10>(defaultAddValue10.ToArray<AddValue10>());

            // DirectionPay (ok)            
            var defaultDirectionCodes = csvParser.CsvParserStart<DirectionPay>(new CsvDirectionPayMapping(),
                                                        "dic_direction_pay.csv");

            // SettingCodeVO and CodeTooling and SettingCodeVOSection   

            IList<SettingCodeVOSection> sections = new List<SettingCodeVOSection>();

            var defaultSettingCodeVOs = csvParser.CsvParserStart<SettingCodeVO>(new CsvSettingCodeVOsMapping(),
                                                        "dic_setting_code_vo.csv");
            foreach (SettingCodeVO scv in defaultSettingCodeVOs)
            {
                if (String.IsNullOrEmpty(scv.OperationCodeRef))
                    scv.OperationCodeRef = null;
                else
                {
                    DirectionPay dp = Array.Find<DirectionPay>(defaultDirectionCodes.ToArray<DirectionPay>(),
                           x => x.Direction_Pay == scv.OperationCodeRef);
                    scv.OperationCodeRef = null;
                    if (dp != null)
                        scv.OperationCode = dp;
                }

                if (scv.SectionRef.HasValue)
                {
                    SettingCodeVOSection section = Array.Find<SettingCodeVOSection>(sections.ToArray<SettingCodeVOSection>(),
                        x => x.SectionNo == scv.SectionRef.Value);
                    if (section == null)
                    {
                        section = new SettingCodeVOSection() { SectionNo = scv.SectionRef.Value, Desc = "" };
                        sections.Add(section);
                    }

                    section.Desc = "Без описания";
                    scv.Section = section;
                }
            }

            var tempCodeTooling = csvParser.CsvParserStart<CodeTooling>(new CsvCodeToolingSettingCodeVOsMapping(),
                                                        "dic_setting_code_vo.csv");

            var defaultCodeToolings = csvParser.CsvParserStart<CodeTooling>(new CsvCodeToolingMapping(),
                                                        "dic_code_tooling.csv");

            {   // Fill the table DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS
                for (int i = 0; i < tempCodeTooling.Count; i++)
                {
                    CodeTooling temp_ct = tempCodeTooling[i];
                    if (temp_ct == null || String.IsNullOrEmpty(temp_ct.TypeTooling))
                        continue;

                    foreach (CodeTooling ct in defaultCodeToolings)
                    {
                        if (ct == null)
                            continue;

                        if (temp_ct.TypeTooling.Equals(ct.TypeTooling))
                        {
                            if (defaultSettingCodeVOs[i] == null)
                                break;
                            defaultSettingCodeVOs[i].CodeToolings.Add(ct);
                            break;
                        }
                    }
                }
            }

            context.CodeToolings.AddRange(defaultCodeToolings);
            context.SettingCodeVOs.AddRange(defaultSettingCodeVOs);

            //   context.CodeToolings.AddOrUpdate<CodeTooling>(defaultCodeToolings.ToArray<CodeTooling>());
            //   context.SettingCodeVOs.AddOrUpdate<SettingCodeVO>(defaultSettingCodeVOs.ToArray<SettingCodeVO>());


            context.DirectionPays.AddRange(defaultDirectionCodes);
            //  context.DirectionPays.AddOrUpdate<DirectionPay>(defaultDirectionCodes.ToArray<DirectionPay>());

            // CodeVORep (ok)                        
            var defaultCodeVOReps = csvParser.CsvParserStart<CodeVORep>(new CsvCodeVORepMapping(),
                                                        "dic_code_vo_rep.csv");
            context.CodeVOReps.AddRange(defaultCodeVOReps);
            //  context.CodeVOReps.AddOrUpdate<CodeVORep>(defaultCodeVOReps.ToArray<CodeVORep>());


            // Filial (ok)             
            var defaultFilials = csvParser.CsvParserStart<Filial>(new CsvFilialMapping(),
                                                        "dic_filial.csv");
            context.Filials.AddRange(defaultFilials);
            //  context.Filials.AddOrUpdate<Filial>(defaultFilials.ToArray<Filial>());

            // IssuerCode (ok)             
            var defaultIssuerCodes = csvParser.CsvParserStart<IssuerCode>(new CsvIssuerCodeMapping(),
                                                        "dic_issuer_code.csv");
            context.IssuerCodes.AddRange(defaultIssuerCodes);
            //  context.IssuerCodes.AddOrUpdate<IssuerCode>(defaultIssuerCodes.ToArray<IssuerCode>());

            // KeywordsException (ok)         
            var defaultKeywordsExceptions = csvParser.CsvParserStart<KeywordsException>(new CsvKeywordsExceptionMapping(),
                                                        "dic_keywords_exception.csv");
            context.KeywordsExceptions.AddRange(defaultKeywordsExceptions);
            //   context.KeywordsExceptions.AddOrUpdate<KeywordsException>(defaultKeywordsExceptions.ToArray<KeywordsException>());

            // KindContract (ok)           
            var defaultKindContracts = csvParser.CsvParserStart<KindContract>(new CsvKindContractMapping(),
                                                        "dic_kind_contract.csv");
            context.KindContracts.AddRange(defaultKindContracts);
            //   context.KindContracts.AddOrUpdate<KindContract>(defaultKindContracts.ToArray<KindContract>());

            // KindNeRez (ok)           
            var defaultKindNerezs = csvParser.CsvParserStart<KindNeRez>(new CsvKindNerezMapping(),
                                                        "dic_kind_nerez.csv");
            context.KindNerezs.AddRange(defaultKindNerezs);
            // context.KindNerezs.AddOrUpdate<KindNeRez>(defaultKindNerezs.ToArray<KindNeRez>());

            // KindRez (ok)           
            var defaultKindRezs = csvParser.CsvParserStart<KindRez>(new CsvKindRezMapping(),
                                                        "dic_kind_rez.csv");
            context.KindRezs.AddRange(defaultKindRezs);
            //  context.KindRezs.AddOrUpdate<KindRez>(defaultKindRezs.ToArray<KindRez>());

            // OperKind (ok)           
            var defaultOperKinds = csvParser.CsvParserStart<OperKind>(new CsvOperKindMapping(),
                                                        "dic_oper_kind.csv");
            context.OperKinds.AddRange(defaultOperKinds);
            //  context.OperKinds.AddOrUpdate<OperKind>(defaultOperKinds.ToArray<OperKind>());

            // OperType (ok)           
            var defaultOperTypes = csvParser.CsvParserStart<OperType>(new CsvOperTypeMapping(),
                                                        "dic_oper_type.csv");
            context.OperTypes.AddRange(defaultOperTypes);
            // context.OperTypes.AddOrUpdate<OperType>(defaultOperTypes.ToArray<OperType>());

            // PDType (ok)           
            var defaultPDTypes = csvParser.CsvParserStart<PDType>(new CsvPDTypeMapping(),
                                                        "dic_pd_type.csv");
            context.PDTypes.AddRange(defaultPDTypes);
            //  context.PDTypes.AddOrUpdate<PDType>(defaultPDTypes.ToArray<PDType>());

            // StatusOper           
            var defaultStatusOpers = csvParser.CsvParserStart<StatusOper>(new CsvStatusOperMapping(),
                                                        "dic_status_oper.csv");

            foreach (StatusOper so in defaultStatusOpers)
            {
                if (String.IsNullOrEmpty(so.SUPDOC_FL))
                    so.SUPDOC_FL = null;
            }

            context.StatusOpers.AddRange(defaultStatusOpers);
            // context.StatusOpers.AddOrUpdate<StatusOper>(defaultStatusOpers.ToArray<StatusOper>());


            // TypeClient and StatusRez          
            var defaultStatusRezs = csvParser.CsvParserStart<StatusRez>(new CsvStatusRezMapping(),
                                                        "dic_status_rez.csv");

            var tempTypeClients = csvParser.CsvParserStart<TypeClient>(new CsvTypeClientStatusRezMapping(),
                                                       "dic_type_client_status_rez.csv");

            var defaultTypeClients = csvParser.CsvParserStart<TypeClient>(new CsvTypeClientMapping(),
                                                        "dic_type_client.csv");

            {   // Fill the table TypeClientStatusRezs
                foreach (TypeClient temp_tc in tempTypeClients)
                {
                    foreach (StatusRez cr in defaultStatusRezs)
                    {
                        if (temp_tc.Description.Equals(cr.Status))
                        {
                            foreach (TypeClient tc in defaultTypeClients)
                            {
                                if (tc.Type_Client.Equals(temp_tc.Type_Client))
                                {
                                    tc.StatusRezs.Add(cr);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }

            context.StatusRezs.AddRange(defaultStatusRezs);
            context.TypeClients.AddRange(defaultTypeClients);

            // context.StatusRezs.AddOrUpdate<StatusRez>(defaultStatusRezs.ToArray<StatusRez>());
            //context.TypeClients.AddOrUpdate<TypeClient>(defaultTypeClients.ToArray<TypeClient>());

            // TypeContract      
            var defaultTypeContracts = csvParser.CsvParserStart<TypeContract>(new CsvTypeContractMapping(),
                                                        "dic_type_contract.csv");
            context.TypeContracts.AddRange(defaultTypeContracts);
            //  context.TypeContracts.AddOrUpdate<TypeContract>(defaultTypeContracts.ToArray<TypeContract>());

            base.Seed(context);

         //   LoadArchiveFromExcel(context);
        }
       
    }
}