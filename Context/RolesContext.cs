namespace OnlineTrener.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class RolesContext : DbContext
    {
        public RolesContext()
            : base("name=MainDb")
        {
        }

        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .Property(e => e.roleName)
                .IsUnicode(false);

            // Relationships
            //modelBuilder.Entity<Role>().HasMany(t => t.Users)
            //    .WithMany(t => t.Roles)
            //    .Map(m =>
            //    {
            //        m.ToTable("role_users");
            //        m.MapLeftKey("roleId");
            //        m.MapRightKey("userId");
            //    });
        }
    }
}
