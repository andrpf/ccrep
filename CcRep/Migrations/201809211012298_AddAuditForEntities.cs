namespace CcRep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditForEntities : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.DIC_CALENDAR", newSchema: "etl");
            AddColumn("dbo.DIC_ACCOUNT_CLIENT", "CREATE_DATE", c => c.DateTime());
            AddColumn("dbo.DIC_ACCOUNT_CLIENT", "LAST_EDITED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_ACCOUNT_CLIENT", "CREATED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_ACCT_REPORT", "CREATE_DATE", c => c.DateTime());
            AddColumn("dbo.DIC_CLIENT_TYPES", "CREATE_DATE", c => c.DateTime());
            AddColumn("dbo.DIC_CLIENT_TYPES", "LAST_EDITED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_CLIENT_TYPES", "CREATED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_CODE_TOOLING", "CREATE_DATE", c => c.DateTime());
            AddColumn("dbo.DIC_CODE_TOOLING", "LAST_EDITED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_CODE_TOOLING", "CREATED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_KEYWORDS", "CREATE_DATE", c => c.DateTime());
            AddColumn("dbo.DIC_KEYWORDS", "LAST_EDITED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_KEYWORDS", "CREATED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_KEYWORDS_EXCEPTION", "CREATE_DATE", c => c.DateTime());
            AddColumn("dbo.DIC_KEYWORDS_EXCEPTION", "LAST_EDITED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_KEYWORDS_EXCEPTION", "CREATED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_OPER_TYPE", "CREATE_DATE", c => c.DateTime());
            AddColumn("dbo.DIC_OPER_TYPE", "LAST_EDITED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_OPER_TYPE", "CREATED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_PD_TYPE", "CREATE_DATE", c => c.DateTime());
            AddColumn("dbo.DIC_PD_TYPE", "LAST_EDITED_BY", c => c.String(maxLength: 128));
            AddColumn("dbo.DIC_PD_TYPE", "CREATED_BY", c => c.String(maxLength: 128));
            CreateIndex("dbo.DIC_ACCOUNT_CLIENT", "LAST_EDITED_BY");
            CreateIndex("dbo.DIC_ACCOUNT_CLIENT", "CREATED_BY");
            CreateIndex("dbo.DIC_CLIENT_TYPES", "LAST_EDITED_BY");
            CreateIndex("dbo.DIC_CLIENT_TYPES", "CREATED_BY");
            CreateIndex("dbo.DIC_CODE_TOOLING", "LAST_EDITED_BY");
            CreateIndex("dbo.DIC_CODE_TOOLING", "CREATED_BY");
            CreateIndex("dbo.DIC_KEYWORDS", "LAST_EDITED_BY");
            CreateIndex("dbo.DIC_KEYWORDS", "CREATED_BY");
            CreateIndex("dbo.DIC_KEYWORDS_EXCEPTION", "LAST_EDITED_BY");
            CreateIndex("dbo.DIC_KEYWORDS_EXCEPTION", "CREATED_BY");
            CreateIndex("dbo.DIC_OPER_TYPE", "LAST_EDITED_BY");
            CreateIndex("dbo.DIC_OPER_TYPE", "CREATED_BY");
            CreateIndex("dbo.DIC_PD_TYPE", "LAST_EDITED_BY");
            CreateIndex("dbo.DIC_PD_TYPE", "CREATED_BY");
            AddForeignKey("dbo.DIC_ACCOUNT_CLIENT", "CREATED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_ACCOUNT_CLIENT", "LAST_EDITED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_CLIENT_TYPES", "CREATED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_CLIENT_TYPES", "LAST_EDITED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_CODE_TOOLING", "CREATED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_CODE_TOOLING", "LAST_EDITED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_KEYWORDS", "CREATED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_KEYWORDS", "LAST_EDITED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_KEYWORDS_EXCEPTION", "CREATED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_KEYWORDS_EXCEPTION", "LAST_EDITED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_OPER_TYPE", "CREATED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_OPER_TYPE", "LAST_EDITED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_PD_TYPE", "CREATED_BY", "dbo.ASP_USERS", "Id");
            AddForeignKey("dbo.DIC_PD_TYPE", "LAST_EDITED_BY", "dbo.ASP_USERS", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DIC_PD_TYPE", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_PD_TYPE", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_OPER_TYPE", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_OPER_TYPE", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_KEYWORDS_EXCEPTION", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_KEYWORDS_EXCEPTION", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_KEYWORDS", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_KEYWORDS", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_CODE_TOOLING", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_CODE_TOOLING", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_CLIENT_TYPES", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_CLIENT_TYPES", "CREATED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_ACCOUNT_CLIENT", "LAST_EDITED_BY", "dbo.ASP_USERS");
            DropForeignKey("dbo.DIC_ACCOUNT_CLIENT", "CREATED_BY", "dbo.ASP_USERS");
            DropIndex("dbo.DIC_PD_TYPE", new[] { "CREATED_BY" });
            DropIndex("dbo.DIC_PD_TYPE", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.DIC_OPER_TYPE", new[] { "CREATED_BY" });
            DropIndex("dbo.DIC_OPER_TYPE", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.DIC_KEYWORDS_EXCEPTION", new[] { "CREATED_BY" });
            DropIndex("dbo.DIC_KEYWORDS_EXCEPTION", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.DIC_KEYWORDS", new[] { "CREATED_BY" });
            DropIndex("dbo.DIC_KEYWORDS", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.DIC_CODE_TOOLING", new[] { "CREATED_BY" });
            DropIndex("dbo.DIC_CODE_TOOLING", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.DIC_CLIENT_TYPES", new[] { "CREATED_BY" });
            DropIndex("dbo.DIC_CLIENT_TYPES", new[] { "LAST_EDITED_BY" });
            DropIndex("dbo.DIC_ACCOUNT_CLIENT", new[] { "CREATED_BY" });
            DropIndex("dbo.DIC_ACCOUNT_CLIENT", new[] { "LAST_EDITED_BY" });
            DropColumn("dbo.DIC_PD_TYPE", "CREATED_BY");
            DropColumn("dbo.DIC_PD_TYPE", "LAST_EDITED_BY");
            DropColumn("dbo.DIC_PD_TYPE", "CREATE_DATE");
            DropColumn("dbo.DIC_OPER_TYPE", "CREATED_BY");
            DropColumn("dbo.DIC_OPER_TYPE", "LAST_EDITED_BY");
            DropColumn("dbo.DIC_OPER_TYPE", "CREATE_DATE");
            DropColumn("dbo.DIC_KEYWORDS_EXCEPTION", "CREATED_BY");
            DropColumn("dbo.DIC_KEYWORDS_EXCEPTION", "LAST_EDITED_BY");
            DropColumn("dbo.DIC_KEYWORDS_EXCEPTION", "CREATE_DATE");
            DropColumn("dbo.DIC_KEYWORDS", "CREATED_BY");
            DropColumn("dbo.DIC_KEYWORDS", "LAST_EDITED_BY");
            DropColumn("dbo.DIC_KEYWORDS", "CREATE_DATE");
            DropColumn("dbo.DIC_CODE_TOOLING", "CREATED_BY");
            DropColumn("dbo.DIC_CODE_TOOLING", "LAST_EDITED_BY");
            DropColumn("dbo.DIC_CODE_TOOLING", "CREATE_DATE");
            DropColumn("dbo.DIC_CLIENT_TYPES", "CREATED_BY");
            DropColumn("dbo.DIC_CLIENT_TYPES", "LAST_EDITED_BY");
            DropColumn("dbo.DIC_CLIENT_TYPES", "CREATE_DATE");
            DropColumn("dbo.DIC_ACCT_REPORT", "CREATE_DATE");
            DropColumn("dbo.DIC_ACCOUNT_CLIENT", "CREATED_BY");
            DropColumn("dbo.DIC_ACCOUNT_CLIENT", "LAST_EDITED_BY");
            DropColumn("dbo.DIC_ACCOUNT_CLIENT", "CREATE_DATE");
            MoveTable(name: "etl.DIC_CALENDAR", newSchema: "dbo");
        }
    }
}
