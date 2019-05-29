using Microsoft.AspNet.Identity;
using CcRep.Models._dc;
using CcRep.Models.adm;
using CcRep.Models.etl;
using CcRep.Models.vk;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CcRep.Managers
{
    public class ReportsManager
    {
        public CcRepContext context { get; set; }

        const string UNKNOWN_AUTHOR = "UNKNOWN";
        const int MAX_USER_NAME_LENGTH = 20;
        static object lock_obj = new object();

        public ReportsManager(CcRepContext db)
        {
            context = db;
        }

        public string LoadDwhOperations(HeaderRep report)
        {
            string message = "";

            if (report == null)
            {
                message = "Отчет с таким номером отсутствует";
                return message;
            }

            lock (lock_obj)
            {
                if (context.DwhLoads.Any<DwhLoad>
                    (b => (DbFunctions.TruncateTime(b.DateLoad) == DbFunctions.TruncateTime(DateTime.Now)) && (b.State == "S")))
                {
                    // В том случае, если запись будет найдена, то выдавать сообщение «Загрузка за сегодняшний день уже была произведена», 
                    // и прекращать работу процедуры. 
                    message = "Загрузка за сегодняшний день уже была произведена";
                }
                else
                {
                    var _Dwhs = context.DWHSs.Where<Dwhs>
                         (b => (DbFunctions.TruncateTime(b.StartLoad) == DbFunctions.TruncateTime(DateTime.Now))).ToArray<Dwhs>();

                    if (_Dwhs != null && _Dwhs.Length > 0)
                    {
                        switch (_Dwhs[0].ParamValue)
                        {
                            case "1":
                                message = "Отправлен запрос в DWH. Ждите";
                                break;
                            case "3":
                                message = "Производится выгрузка операций из DWH. Ждите";
                                break;
                            case "4":
                                message = "Производится загрузка операций в систему. Ждите";
                                break;
                        }
                    }
                    else
                    {
                        Dwhs dwh = new Dwhs()
                        {
                            IdRep = report.Id,
                            ParamValue = "1",
                            ParamName = Dwhs.ParamNames.CCREP_DWH_UNLOAD.ToString(),
                            StartLoad = DateTime.Now,
                            LaunchAuthor = CurrentUserName
                        };

                        context.DWHSs.Add(dwh);
                        context.SaveChanges();

                        message = "Ваш запрос отправлен, ждите.";
                    }
                }
            }

            return message;
        }

        private static string CurrentUserName
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.User == null
                    || HttpContext.Current.User.Identity == null)
                    return UNKNOWN_AUTHOR;

                string user_name = HttpContext.Current.User.Identity.GetUserName();
                if (String.IsNullOrEmpty(user_name))
                    return UNKNOWN_AUTHOR;

                int len = user_name.Length;
                if (len > MAX_USER_NAME_LENGTH)
                    user_name = user_name.Substring(0, MAX_USER_NAME_LENGTH);
                return user_name;
            }
        }
    }
}