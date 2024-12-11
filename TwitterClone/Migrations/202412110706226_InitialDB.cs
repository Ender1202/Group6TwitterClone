namespace TwitterClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 10),
                        FollowingId = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FollowingId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FollowingId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 10),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Mobile = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 20),
                        ConfirmPassword = c.String(nullable: false),
                        Bio = c.String(maxLength: 500),
                        Pic = c.String(),
                        Fullname = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Joined = c.DateTime(nullable: false),
                        Active = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        TweetId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 10),
                        Message = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TweetId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Followings", "FollowingId", "dbo.Users");
            DropForeignKey("dbo.Tweets", "UserId", "dbo.Users");
            DropIndex("dbo.Tweets", new[] { "UserId" });
            DropIndex("dbo.Followings", new[] { "FollowingId" });
            DropIndex("dbo.Followings", new[] { "UserId" });
            DropTable("dbo.Tweets");
            DropTable("dbo.Users");
            DropTable("dbo.Followings");
        }
    }
}
