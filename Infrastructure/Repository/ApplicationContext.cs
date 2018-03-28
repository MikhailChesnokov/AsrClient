namespace Infrastructure.Repository
{
    using Domain.Entities.ResultText;
    using Domain.Entities.Speaker;
    using Domain.Entities.Speech;
    using Microsoft.EntityFrameworkCore;

    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<Speech> Audios { get; set; }

        public DbSet<ResultText> RecognitionResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=AsrClient;User=root;Password=12345678;SslMode=none");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Speaker>().HasKey(x => x.Id);
            builder.Entity<Speaker>().Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Entity<Speaker>().HasAlternateKey(x => new {x.FirstName, x.Surname, x.Patronymic});
            builder.Entity<Speaker>().Property(x => x.FirstName).IsRequired().HasMaxLength(64);
            builder.Entity<Speaker>().Property(x => x.Surname).IsRequired().HasMaxLength(64);
            builder.Entity<Speaker>().Property(x => x.Patronymic).IsRequired().HasMaxLength(64);
            builder.Entity<Speaker>().Property(x => x.University).HasMaxLength(128);
            builder.Entity<Speaker>().Property(x => x.Faculty).HasMaxLength(128);
            builder.Entity<Speaker>().Property(x => x.Group).HasMaxLength(16);
            builder.Entity<Speaker>().HasIndex(x => new {x.Id, x.FirstName, x.Surname, x.Patronymic});
            builder.Entity<Speaker>().HasMany(x => x.Audios).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Speech>().HasKey(x => x.Id);
            builder.Entity<Speech>().Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Entity<Speech>().HasAlternateKey(x => x.Name);
            builder.Entity<Speech>().Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Entity<Speech>().Property(x => x.AudioFile).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(500000);
            builder.Entity<Speech>().Property(x => x.AudioFormat).IsRequired();
            builder.Entity<Speech>().Property(x => x.BitDepth);
            builder.Entity<Speech>().Property(x => x.BitRate);
            builder.Entity<Speech>().Property(x => x.Duration);
            builder.Entity<Speech>().Property(x => x.SamplingRate);
            builder.Entity<Speech>().Property(x => x.RecordingDate);
            builder.Entity<Speech>().Property(x => x.Size);
            builder.Entity<Speech>().Property(x => x.TotalChannels);
            builder.Entity<Speech>().HasIndex(x => new { x.Id, x.Name });
            builder.Entity<Speech>().HasMany(x => x.RecognitionResults).WithOne(x => x.Speech).HasForeignKey(x => x.AudioId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ResultText>().HasKey(x => x.Id);
            builder.Entity<ResultText>().Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Entity<ResultText>().Property(x => x.Text).IsRequired().IsUnicode();
            builder.Entity<ResultText>().Property(x => x.Confidence);
            builder.Entity<ResultText>().Property(x => x.RecognitionDate);
            builder.Entity<ResultText>().HasIndex(x => x.AudioId);

            base.OnModelCreating(builder);
        }
    }
}