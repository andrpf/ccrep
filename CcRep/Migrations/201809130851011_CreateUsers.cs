namespace CcRep.Migrations
{
    using CcRep.MigrationsExtends;

    public partial class CreateUsers : ExtendedMigration
    {
        private string etlUser = "ccrep_etl";
        private string outUser = "dwh4ccrep";

        public override void Up()
        {
            CreateUser(etlUser, "etl4cCr78ep$");
            GrantToSchema(etlUser, "etl", Operation.SELECT, Operation.INSERT, Operation.DELETE, Operation.UPDATE);
            //GrantToTable(etlUser, "dbo.DIC_CALENDAR", Operation.SELECT, Operation.INSERT, Operation.DELETE, Operation.UPDATE);

            CreateUser(outUser, "dwh4cCr75ep$");
            GrantToSchema(outUser, "out", Operation.SELECT);
        }

        public override void Down()
        {
            DropUser(etlUser);
            DropUser(outUser);
        }
    }
}
