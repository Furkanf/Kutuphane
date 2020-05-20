using Kutuphane.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kutuphane.Context
{
    public class Context:DbContext
    {
        public Context() : base("name=KutuphaneDBConnectionString")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }
   
        public DbSet<AdminUser> adminUsers { get; set; }
        public DbSet<StandardUser> standardUsers { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<UserBookMap> userBookMaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.Entity<AdminUser>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("AdminUser ");
            });

            modelBuilder.Entity<StandardUser>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("StandardUser ");
            });

            modelBuilder.Entity<UserBookMap>()
               .HasKey(c => new { c.Isbn});

            modelBuilder.Entity<UserBookMap>()
                .HasRequired<StandardUser>(u => u.user)
                .WithMany(b => b.userBooks)
                .HasForeignKey<string>(b=>b.userId);
                
               
                
                
        }
     
    }
}