namespace OnlineTrener.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class UsersContext : DbContext
    {

        public UsersContext()
            : base("name=MainDb")
        {

        }
        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password_hash)
                .IsUnicode(false);

            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany().Map(m =>
                   {
                       m.ToTable("role_users");
                       m.MapLeftKey("user_Id");
                       m.MapRightKey("role_Id");
                   });
        }
    }
}
