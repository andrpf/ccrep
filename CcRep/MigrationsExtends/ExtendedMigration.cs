using System;
using System.Data.Entity.Migrations;
using System.IO;

namespace CcRep.MigrationsExtends
{
    public enum Operation { SELECT, INSERT, UPDATE, DELETE };

    public abstract class ExtendedMigration : DbMigration
    {
        public string MigrationSqlPath { get; set; } = GetBasePath() + "Migrations/sql/";

        public void CreateUser(string UserName, string UserPassword)
        {
            Sql(
                $@"IF NOT EXISTS (SELECT name FROM master.sys.server_principals WHERE name = '{UserName}') BEGIN CREATE LOGIN {UserName} WITH PASSWORD = '{UserPassword}' END");

            Sql(
                $@"CREATE USER {UserName} FOR LOGIN {UserName};");
        }

        public void GrantToSchema(string UserName, string SchemaName, params Operation[] Operations)
        {
            foreach(Operation op in Operations)
            {
                Sql(
                $"GRANT {op} ON SCHEMA :: {SchemaName} TO {UserName} WITH GRANT OPTION; ");
            }
        }

        public void GrantToTable(string UserName, string TableName, params Operation[] Operations)
        {
            foreach (Operation op in Operations)
            {
                Sql(
                $"GRANT {op} ON {TableName} TO {UserName} WITH GRANT OPTION; ");
            }
        }

        public void DropUser(string UserName)
        {
            Sql(
                $"DROP USER IF EXISTS {UserName}; DROP LOGIN {UserName};");
        }

        public void StopServerAgent()
        {
            Sql(
               $@"EXEC xp_servicecontrol N'STOP',N'SQLServerAGENT'");
        }
        public void StartServerAgent()
        {
            Sql(
               $@"EXEC xp_servicecontrol N'START',N'SQLServerAGENT'");
        }
        
        public void EnableCdcForDb()
        {
            Sql(
               $@"EXEC sys.sp_cdc_enable_db");
        }

        public static string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "/../";
        }

        public static string GetMi()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "/../";
        }
    }
}