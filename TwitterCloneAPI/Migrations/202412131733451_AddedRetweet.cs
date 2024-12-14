namespace TwitterClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRetweet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Retweets",
                c => new
                    {
                        RetweetId = c.Int(nullable: false, identity: true),
                        OriginalTweetId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Content = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RetweetId)
                .ForeignKey("dbo.Tweets", t => t.OriginalTweetId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.OriginalTweetId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Retweets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Retweets", "OriginalTweetId", "dbo.Tweets");
            DropIndex("dbo.Retweets", new[] { "UserId" });
            DropIndex("dbo.Retweets", new[] { "OriginalTweetId" });
            DropTable("dbo.Retweets");
        }
    }
}
