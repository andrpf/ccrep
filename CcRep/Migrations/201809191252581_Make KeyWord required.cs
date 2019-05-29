namespace CcRep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeKeyWordrequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DIC_KEYWORDS", "IX_Keyword");
            AlterColumn("dbo.DIC_KEYWORDS", "KEYWORD", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.DIC_KEYWORDS", "KEYWORD", unique: true, name: "IX_Keyword");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DIC_KEYWORDS", "IX_Keyword");
            AlterColumn("dbo.DIC_KEYWORDS", "KEYWORD", c => c.String(maxLength: 100));
            CreateIndex("dbo.DIC_KEYWORDS", "KEYWORD", unique: true, name: "IX_Keyword");
        }
    }
}
