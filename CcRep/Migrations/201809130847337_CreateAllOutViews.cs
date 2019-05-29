namespace CcRep.Migrations
{
    using CcRep.Components.StringExt;
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class CreateAllOutViews : DbMigration
    {
        public int MyProperty { get; set; }

        const string dir_sql = @"Migrations\sql\";
        const string dir_xlsx = @"Migrations\xlsx\";
        string base_dir;

        string path_to_views { get { return base_dir + dir_sql + @"views\"; } }
        string path_to_triggers { get { return base_dir + dir_sql + @"triggers\"; } }
        string path_to_archive { get { return base_dir + dir_sql + @"archive\"; } }
        string path_to_xlsx { get { return base_dir + dir_xlsx; } }

        string[] fileViews = new string[]
        {
            "out_ccrep_client_account.sql",
            "out_ccrep_customer_accounts.sql",
            "out_ccrep_customer_code_405_settings.sql",
            "out_ccrep_keywords.sql",
            "out_ccrep_keywords_exception.sql",
            "out_ccrep_log.sql"
        };

        string[] fileDropViews = new string[]
        {
            "drop_all_out_views.sql"
        };

        string[] fileTriggers = new string[]
        {
            "create_tgrTransferFromETL.sql"
        };

        string[] fileDropTriggers = new string[]
        {
            "drop_tgrTransferFromETL.sql"
        };

        string[] fileArchive = new string[]
        {
            "InsertHeaderReports.sql",
          //  "LoadArchiveFromAccess.sql",
          //  "LoadArchiveFor405.sql"
        };

        string[] fileDropArchive = new string[]
        {
            "DeleteHeaderReports.sql"
        };

        string[] fileLoadArchive = new string[]
        {
            null,
        //    "ArchiveAccessForLoad.xlsx",
         //   "Archive_405_ForLoad.xlsx"
        };


        public CreateAllOutViews()
        {
            SetPathToSqlFiles();

            for (int i = 0; i < fileLoadArchive.Length; i++)
            {
                if (fileLoadArchive[i] != null)
                    fileLoadArchive[i] = path_to_xlsx + fileLoadArchive[i];
            }
        }


        void SetPathToSqlFiles()
        {
            base_dir = AppDomain.CurrentDomain.BaseDirectory.CutEnd(@"bin\") + @"\";

            if (!Directory.Exists(base_dir))
                throw new ApplicationException(String.Format("The base directory '{0}' is not exist.", base_dir));

            if (!Directory.Exists(path_to_views))
                throw new ApplicationException(String.Format("The sql-path '{0}' is not exist.",
                                               path_to_views));

            if (!Directory.Exists(path_to_triggers))
                throw new ApplicationException(String.Format("The sql-path '{0}' is not exist.",
                                               path_to_triggers));
        }

        private void CreateSqlRequestFromFiles(string[] filePaths, string root_path, object[] args = null)
        {
            int len = filePaths.Length - 1;
            string sql_str = "USE ccrep\r\nGO\r\n";

            try
            {
                for (int i = 0; i <= len; i++)
                {
                    string file = root_path + filePaths[i];
                    if (!File.Exists(file))
                        throw new ApplicationException(String.Format("This file '{0}' does not exist:", file));

                    StreamReader f = File.OpenText(file);
                    string _sql = f.ReadToEnd();
                    if (args != null)
                        _sql = String.Format(_sql, args[i]);

                    sql_str += _sql + "\r\nGO\r\n";
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("[CreateSqlRequestFromFiles]: " + ex.Message);
            }

            Sql(sql_str);
        }

        public override void Up()
        {
            CreateSqlRequestFromFiles(fileArchive, path_to_archive, fileLoadArchive);
            CreateSqlRequestFromFiles(fileViews, path_to_views);
            CreateSqlRequestFromFiles(fileTriggers, path_to_triggers);

        }

        public override void Down()
        {
            CreateSqlRequestFromFiles(fileDropTriggers, path_to_triggers);
            CreateSqlRequestFromFiles(fileDropViews, path_to_views);
            CreateSqlRequestFromFiles(fileDropArchive, path_to_archive);
        }
    }
}

