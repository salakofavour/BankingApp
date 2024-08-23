using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.POCO;

public partial class BankingAppContext : DbContext
{
    public BankingAppContext()
    {
    }

    public BankingAppContext(DbContextOptions<BankingAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<NewRequest> NewRequests { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<UsersandAccountInfo> UsersandAccountInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-KFTFNI92\\FAVOURSQLEXPRESS;database=BankingApp;integrated security = true; TrustServerCertificate = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("pk_adminId");

            entity.Property(e => e.AdminId).HasColumnName("adminID");
            entity.Property(e => e.AdminName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("adminName");
            entity.Property(e => e.AdminPassword)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("adminPassword");
        });

        modelBuilder.Entity<NewRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("pk_RequestID");

            entity.ToTable("NewRequest");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.RequestDescription)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.AccNoNavigation).WithMany(p => p.NewRequests)
                .HasForeignKey(d => d.AccNo)
                .HasConstraintName("fk_AccNo");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("pk_TransactionID");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.TransactionDetails)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TransactionTime)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Transaction_Time");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.AccNoNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccNo)
                .HasConstraintName("fk_TransactionAccNo");
        });

        modelBuilder.Entity<UsersandAccountInfo>(entity =>
        {
            entity.HasKey(e => e.AccNo).HasName("pk_accNo");

            entity.ToTable("UsersandAccountInfo");

            entity.HasIndex(e => e.UserName, "unk_userName").IsUnique();

            entity.Property(e => e.AccNo)
                .ValueGeneratedNever()
                .HasColumnName("accNo");
            entity.Property(e => e.AccBalance).HasColumnName("accBalance");
            entity.Property(e => e.AccName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("accName");
            entity.Property(e => e.AccType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("accType");
            entity.Property(e => e.IsAccActive).HasColumnName("isAccActive");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userPassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
