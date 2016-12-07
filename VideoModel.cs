namespace OnlineTrener
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class VideoModel : DbContext
    {
        public VideoModel()
            : base("name=VideoModel1")
        {
        }

        public virtual DbSet<Video> Videos { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Video>()
                .Property(e => e.videoTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Video>()
                .Property(e => e.videoDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Video>()
                .Property(e => e.videoCategory)
                .IsUnicode(false);

            modelBuilder.Entity<Video>()
                .Property(e => e.videoURL)
                .IsUnicode(false);
        }
    }
}
