namespace CcRep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeKeywordExceptionRequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DIC_KEYWORDS_EXCEPTION", "IX_KeywordException_Keyword");
            AlterColumn("dbo.DIC_KEYWORDS_EXCEPTION", "KEYWORD", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.DIC_KEYWORDS_EXCEPTION", "KEYWORD", unique: true, name: "IX_KeywordException_Keyword");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DIC_KEYWORDS_EXCEPTION", "IX_KeywordException_Keyword");
            AlterColumn("dbo.DIC_KEYWORDS_EXCEPTION", "KEYWORD", c => c.String(maxLength: 255));
            CreateIndex("dbo.DIC_KEYWORDS_EXCEPTION", "KEYWORD", unique: true, name: "IX_KeywordException_Keyword");
        }
    }
}
