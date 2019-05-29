namespace CcRep.Migrations
{
    using CcRep.MigrationsExtends;
    using CcRep.Models._dc;
    using CcRep.Models.dic;
    using CcRep.Models.vk;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public partial class TurnDataCapture : ExtendedMigration
    {
        public CcRepContext db { get; set; } = new CcRepContext();

        public List<Table> GetTableNames()
        {
            return new List<Table>
            {
                db.GetTableName<AcctReport>(),
                db.GetTableName<ClientType>(),
                db.GetTableName<AccountClient>(),
                db.GetTableName<Keyword>(),
                db.GetTableName<KeywordsException>(),
                db.GetTableName<CodeTooling>(),
                db.GetTableName<OperType>(),
                db.GetTableName<PDType>(),
                db.GetTableName<SettingCodeVO>(),
                db.GetTableName<Rep406>(),
                db.GetTableName<AddRep>(),
                db.GetTableName<AddInfRep>(),
                db.GetTableName<ClientRep>(),
                db.GetTableName<IssuerRep>(),
                db.GetTableName<NoticeRep>(),
                db.GetTableName<SupdocRep>()
            };
        }

        private bool isNotExpress()
        {
            bool isNotExpress = db.Database.SqlQuery<bool>($"SELECT dbo.CheckNotExpress()")
                            .FirstOrDefault();

            return isNotExpress;
        }

        public override void Up()
        {
            if (this.isNotExpress())
            {
                EnableCdcForDb();

                var tables = GetTableNames();

                foreach (Table t in tables)
                {
                    Sql($@"EXEC dbo.AddTableToCdc @tableName = N'{t.Name}', @schema = N'{t.Schema}'");
                }

                //Изменить время устаревания логов до 180 дней
                Sql("sp_cdc_change_job @job_type='cleanup', @retention=259200");
            }


        }

        public override void Down()
        {
            if (this.isNotExpress())
            {
                var tables = GetTableNames();

                foreach (Table t in tables)
                {
                    Sql($@"EXEC sys.sp_cdc_disable_table @source_schema = '{t.Schema}', @source_name = '{t.Name}', @capture_instance = 'all'");
                }
            }
        }
    }
}
