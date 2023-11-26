using Microsoft.EntityFrameworkCore;



namespace DataAccessLayer.EntityDB
{
    public class ChatDbContext : DbContext
    {

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }


        public virtual DbSet<ChatEntity> Chats { get; set; }

        public virtual DbSet<MessageEntity> Messages { get; set; }

        public virtual DbSet<UserEntity> Users { get; set; }
        
        public virtual DbSet<ParticipantsEntity> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ChatEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("int");
                entity.Property(e => e.ChatName).HasColumnType("nvarchar(50)");
                entity.ToTable("Chats");
            });

            modelBuilder.Entity<MessageEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("int");
                entity.Property(e => e.Content).HasColumnType("nvarchar(50)");
                entity.Property(e => e.Timestamp).HasColumnType("DateTime");
                entity.ToTable("Messages");
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("int");
                entity.Property(e => e.UserName).HasColumnType("nvarchar(50)");
                entity.HasIndex(e => e.UserName).IsUnique(); // Создание уникального индекса
                entity.ToTable("Users");
            });

            modelBuilder.Entity<ParticipantsEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("int");
                entity.Property(e => e.UserId).HasColumnType("int");
                entity.Property(e => e.ChatId).HasColumnType("int");
                entity.Property(e => e.IsAdmin).HasColumnType("bit");
                entity.ToTable("Participants");
                entity.HasIndex(p => new { p.UserId, p.ChatId }).IsUnique();
                entity.HasMany(p => p.Messages).WithOne(m => m.Participant);
            });

        }
    }
}
