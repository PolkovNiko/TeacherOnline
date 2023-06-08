using Microsoft.EntityFrameworkCore;
using System;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.DAL;

public partial class AssistantTeachingContext : DbContext
{
    public AssistantTeachingContext()
    {
    }

    public AssistantTeachingContext(DbContextOptions<AssistantTeachingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Estimate> Estimates { get; set; }

    public virtual DbSet<Entities.File> Files { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupsInSub> GroupsInSubs { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DataBase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.ToTable("Chat");

            entity.HasOne(d => d.IdUser1Navigation).WithMany(p => p.ChatIdUser1Navigations)
                .HasForeignKey(d => d.IdUser1)
                .HasConstraintName("FK_Chat_Profile");

            entity.HasOne(d => d.IdUser2Navigation).WithMany(p => p.ChatIdUser2Navigations)
                .HasForeignKey(d => d.IdUser2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chat_Profile1");
        });

        modelBuilder.Entity<Estimate>(entity =>
        {
            entity.Property(e => e.DateAdd).HasColumnType("smalldatetime");
            entity.Property(e => e.DateUpdate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Estimates)
                .HasForeignKey(d => d.IdSubject)
                .HasConstraintName("FK_Estimates_Subjects");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.EstimateIdTeacherNavigations)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estimates_ToTeacher");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.EstimateIdUserNavigations)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estimates_ToUser");
        });

        modelBuilder.Entity<Entities.File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07E83A90B0");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Files)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Files_ToProfile");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0700DDEC6B");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Specialty).HasMaxLength(50);
        });

        modelBuilder.Entity<GroupsInSub>(entity =>
        {
            entity.ToTable("GroupsInSub");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdGroupsNavigation).WithMany(p => p.GroupsInSubs)
                .HasForeignKey(d => d.IdGroups)
                .HasConstraintName("FK_GroupsInSub_Groups");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.GroupsInSubs)
                .HasForeignKey(d => d.IdSubject)
                .HasConstraintName("FK_GroupsInSub_Subjects");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.GroupsInSubs)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupsInSub_Profile");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Message1).HasColumnName("Message");
            entity.Property(e => e.Time).HasColumnType("smalldatetime");

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Profile");

            entity.HasOne(d => d.IdChatNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdChat)
                .HasConstraintName("FK_Message_Chat");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07787CD6C4");

            entity.ToTable("Profile");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.GroupsNavigation).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.Groups)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Profile_Groups");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Profile)
                .HasForeignKey<Profile>(d => d.Id)
                .HasConstraintName("FK_Profile_Users");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC071EAFF885");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.IdTeacher)
                .HasConstraintName("FK_Subjects_ToTeacher");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC073275A89F");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825BB3B5CA59").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105345420AB15").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(18);
            entity.Property(e => e.Rank).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
