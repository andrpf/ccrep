using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CcRep.Areas.Reports.Models;
using CcRep.Managers.Transactions;
using CcRep.Models._dc;
using CcRep.Models.vk;
using CcRep.Components.Handlers;
using CcRep.Managers.Export;

namespace CcRep.Models._db
{
    public class TestingApiFilters
    {
        CcRepContext db { get; set; }
        const int MAX_TESTS = 25;

        string[] ErrorTitles = new string[MAX_TESTS];

        public TestingApiFilters(CcRepContext db)
        {
            this.db = db;

            for (int i = 0; i < MAX_TESTS; i++)
            {
                ErrorTitles[i] = String.Format("[Api-filters: Test {0}] ", i + 1);
            }

            this.db.SaveChanges();
        }

        HeaderRep GetHeaderRep()
        {
            CcRepUser repUser = new CcRepUser()
            {
                FullName = "Петр Иванов",
                UserName = "test_user"
            };

            return new HeaderRep()
            {
                BeginDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now.AddMonths(1),
                CreateDate = DateTime.Now,
                UserLastEditedId = repUser.Id,
                UserLastEdited = repUser
            };
        }

        QueryBuildRule[] GetQueryBuildRules(int num_test)
        {
            List<QueryBuildRule> rules = new List<QueryBuildRule>();

            switch (num_test)
            {
                case 1:
                    {
                        var year = DateTime.Now.Year;
                        var month = DateTime.Now.Month;

                        var startDate = new DateTime(year, month, 1).AddMonths(-1);

                        QueryBuildRule rule = new QueryBuildRule
                        {
                            id = "AddInfRep_DateGetDoc",
                            type = "datetime",
                            _operator = "equal",
                            value = startDate.ToString(HandlerOutFormat.FORMAT_DATES[0])
                        };
                        rules.Add(rule);
                    }
                    break;
                case 2:
                    {
                        QueryBuildRule rule = new QueryBuildRule
                        {
                            id = "AddInfRep_DateLock",
                            type = "datetime",
                            _operator = "is_null",
                            value = null
                        };
                        rules.Add(rule);
                    }
                    break;

                case 3:
                    {
                        QueryBuildRule rule = new QueryBuildRule
                        {
                            id = "AddInfRep_Status",
                            type = "string",
                            _operator = "begins_with",
                            value = "INC"
                        };
                        rules.Add(rule);
                    }
                    break;
                case 4:
                    {
                        QueryBuildRule rule = new QueryBuildRule
                        {
                            id = "Rep406_OperKind",
                            type = "integer",
                            _operator = "equal",
                            value = "20"
                        };
                        rules.Add(rule);
                    }
                    break;

                case 5:
                    {
                        QueryBuildRule rule = new QueryBuildRule
                        {
                            id = "Rep406_BicBank",
                            type = "string",
                            _operator = "equal",
                            value = "044525225"
                        };
                        rules.Add(rule);
                    }
                    break;

                case 6:
                    {
                        QueryBuildRule rule = new QueryBuildRule
                        {
                            id = "BasicRep_AmountAll",
                            type = "decimal",
                            _operator = "equal",
                            value = "10.0"
                        };
                        rules.Add(rule);
                    }
                    break;

            }

            return rules.ToArray();
        }

        FindTranDataModel GetTranDataModel(int num_test)
        {
            return new FindTranDataModel()
            {
                Rules = GetQueryBuildRules(num_test),
                ReportId = 1,
                PageLength = 5,
                StartRowNo = 0,
                onlyDuple = false,
                OrderRules = new FindTranOrder[]
                  { new FindTranOrder() { column = 0, dir = Dir.desc } }
            };
        }

        public void Test_1()
        {
            HeaderRep hr = GetHeaderRep();
            FindTranDataModel req = GetTranDataModel(1);

            GridDataHandler handler = new GridDataHandler(db);
            GridDataHandler.ResponseData res = handler.GetData(req, hr, true);
            if (!String.IsNullOrEmpty(handler.ErrorString))
                throw new HttpException(ErrorTitles[0] + handler.ErrorString);

            if (res.data.Length != 0 || res.recordsTotal != 0)
                throw new HttpException(ErrorTitles[0] + "Некорректные выходные данные.");
        }

        int[] data_lengths = new int[] { 0, 2, 2, 2, 1, 2, 1 };

        public void AllTests(HeaderRep hr, int count_records)
        {
            for (int num_test = 1; num_test < data_lengths.Length; num_test++)
            {
                Tests(hr, num_test, count_records);
            }

          //  TestExportToKliko(hr);
        }

        public void TestExportToKliko(HeaderRep hr)
        {
            ExportToKliko export_to_kliko = new ExportToKliko(db);
            string result = export_to_kliko.CreateKlikoFiles(hr, @"c:\temp\", "Все филиалы", "Все");
            if (result != "true")
                throw new HttpException("Некорректен экспорт в Клико.");
        }

        public void Tests(HeaderRep hr, int num_test, int count_records)
        {
            FindTranDataModel req = GetTranDataModel(num_test);

            GridDataHandler handler = new GridDataHandler(db);
            GridDataHandler.ResponseData res = handler.GetData(req, hr, true);
            if (!String.IsNullOrEmpty(handler.ErrorString))
                throw new HttpException(ErrorTitles[num_test] + handler.ErrorString);

            int data_length = data_lengths[num_test];
            if (res.data.Length != data_length || res.recordsTotal != count_records || res.recordsFiltered != data_length)
                throw new HttpException(ErrorTitles[num_test] + "Некорректные выходные данные: " +
                    String.Format("Data length = {0} (!={1}), Total records = {2} (!={3}), Filtered records = {4} (!={5})",
                    res.data.Length, data_length, res.recordsTotal, count_records, res.recordsFiltered, data_length));
        }
    }
}