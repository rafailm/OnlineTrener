namespace OnlineTrener.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class UserRolesContext : DbContext
    {
        public UserRolesContext()
            : base("name=MainDb")
        {
        }

        public virtual DbSet<role_users> role_users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
