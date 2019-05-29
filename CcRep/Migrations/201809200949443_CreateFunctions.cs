namespace CcRep.Migrations
{
    using CcRep.MigrationsExtends;
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class CreateFunctions : ExtendedMigration
    {
        public override void Up()
        {
            var filePath = Path.Combine(MigrationSqlPath, "functions/FunctionsCreate.sql");
            SqlFile(filePath);
        }
        
        public override void Down()
        {
            var filePath = Path.Combine(MigrationSqlPath, "functions/FunctionsDrop.sql");
            SqlFile(filePath);
        }
    }
}
