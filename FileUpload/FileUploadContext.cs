using System;
using System.Collections.Generic;
using FileUploadAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FileUploadAPI;

public partial class FileUploadContext : DbContext
{
    public FileUploadContext()
    {
    }

    public FileUploadContext(DbContextOptions<FileUploadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TransactionData> TransactionData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:FileUpload");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07799659A1");

            entity.Property(e => e.Amount).HasColumnType("decimal(26, 9)");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TransactionId).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
