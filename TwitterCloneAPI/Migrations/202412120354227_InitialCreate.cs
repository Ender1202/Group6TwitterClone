namespace TwitterClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        FollowingId = c.String(maxLength: 128),
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
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Mobile = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        Bio = c.String(),
                        Pic = c.String(),
                        Fullname = c.String(),
                        Email = c.String(),
                        JoiningDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        TweetId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Message = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TweetId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tweets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Followings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Followings", "FollowingId", "dbo.Users");
            DropIndex("dbo.Tweets", new[] { "UserId" });
            DropIndex("dbo.Followings", new[] { "FollowingId" });
            DropIndex("dbo.Followings", new[] { "UserId" });
            DropTable("dbo.Tweets");
            DropTable("dbo.Users");
            DropTable("dbo.Followings");
        }
    }
}
