using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TwitterClone.Models;

namespace TwitterClone.DataAccess
{
    public class TwitterCloneDbContext : DbContext
    {

        public DbSet<User> User { get; set; }
        public DbSet<Tweet> Tweet { get; set; }

        public DbSet<Following> Following { get; set; }
        public TwitterCloneDbContext() : base("X")
        {

        }

        // Unable to generate entity type for table 'dbo.Following'. Please see the warning messages.

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=.\;Database=TwitterClone;Trusted_Connection=True;");
        //            }
        //        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
         

        //    modelBuilder.Entity<User>().HasKey(e => e.UserId);

        //    modelBuilder.Entity<User>().Property(e => e.UserId)
        //     .HasColumnName("user_id")
        //     .HasMaxLength(25)
        //     .IsUnicode(false);


        //    modelBuilder.Entity<User>().Property(e => e.Active).HasColumnName("active");

        //    modelBuilder.Entity<User>().Property(e => e.Email)
        //     .IsRequired()
        //     .HasColumnName("email")
        //     .HasMaxLength(50)
        //     .IsUnicode(false);

        //    modelBuilder.Entity<User>().Property(e => e.Fullname)
        //     .IsRequired()
        //     .HasColumnName("fullname")
        //     .HasMaxLength(30)
        //     .IsUnicode(false);

        //    //modelBuilder.Entity<User>().Property(e => e.DateJoined)
        //    // .IsRequired()
        //    // .HasColumnName("joined")
        //    // .HasColumnType("datetime");

        //    modelBuilder.Entity<User>().Property(e => e.Password)
        //     .IsRequired()
        //     .HasColumnName("password")
        //     .HasMaxLength(50)
        //     .IsUnicode(false);



        //    modelBuilder.Entity<Tweet>().Property(e => e.TweetId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("tweet_id");

        //    modelBuilder.Entity<Tweet>().Property(e => e.Created)
        //        .HasColumnName("created")
        //        .HasColumnType("datetime");

        //    modelBuilder.Entity<Tweet>().Property(e => e.Message)
        //        .IsRequired()
        //        .HasColumnName("message")
        //        .HasMaxLength(140)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Tweet>().Property(e => e.UserId)
        //        .IsRequired()
        //        .HasColumnName("user_id")
        //        .HasMaxLength(25)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Tweet>().HasRequired(d => d.User)
        //        .WithMany(p => p.Tweet)
        //        .HasForeignKey(d => d.UserId)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Following>().HasKey(e => new { e.UserId, e.FollowingId });
        //}
    }
}