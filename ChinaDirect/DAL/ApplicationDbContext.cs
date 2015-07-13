using ChinaDirect.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChinaDirect.DAL
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
         public DbSet<UserInfo> UserInfo { get; set; }
        //public DbSet<ApplicationUser> Users { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        } 

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}