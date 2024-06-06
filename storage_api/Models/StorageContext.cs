using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace storage_api.Models;

public partial class StorageContext : DbContext
{
    private readonly IConfiguration _configuration;

    public StorageContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public StorageContext(DbContextOptions<StorageContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Dataset> Datasets { get; set; }

    public virtual DbSet<DatasetEntry> DatasetEntries { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var address = _configuration["Database:Address"];
        var database = _configuration["Database:DatabaseName"];
        var user = _configuration["Database:User"];
        var password = _configuration["Database:Password"];
        var port = _configuration["Database:Port"];

        optionsBuilder.UseMySQL($"Server={address}; Port={port}; Database={database}; Uid={user}; Pwd={password};");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dataset");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DatasetEntry>(entity =>
        {
            entity.HasKey(e => new { e.DatasetId, e.ImageId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("dataset_entry");

            entity.HasIndex(e => e.ImageId, "image_id");

            entity.Property(e => e.DatasetId)
                .HasColumnType("int(11)")
                .HasColumnName("dataset_id");
            entity.Property(e => e.ImageId)
                .HasColumnType("int(11)")
                .HasColumnName("image_id");
            entity.Property(e => e.SetType)
                .HasColumnType("enum('Training','Test','Validation')")
                .HasColumnName("set_type");

            entity.HasOne(d => d.Dataset).WithMany(p => p.DatasetEntries)
                .HasForeignKey(d => d.DatasetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dataset_entry_ibfk_1");

            entity.HasOne(d => d.Image).WithMany(p => p.DatasetEntries)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dataset_entry_ibfk_2");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("image");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("date_added");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SourcePath)
                .HasMaxLength(255)
                .HasColumnName("source_path");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("label");

            entity.HasIndex(e => e.ImageId, "image_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Class)
                .HasColumnType("enum('Herring')")
                .HasColumnName("class");
            entity.Property(e => e.H).HasColumnName("h");
            entity.Property(e => e.ImageId)
                .HasColumnType("int(11)")
                .HasColumnName("image_id");
            entity.Property(e => e.W).HasColumnName("w");
            entity.Property(e => e.X).HasColumnName("x");
            entity.Property(e => e.Y).HasColumnName("y");

            entity.HasOne(d => d.Image).WithMany(p => p.Labels)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("label_ibfk_1");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("model");

            entity.HasIndex(e => e.StartModelId, "start_model_id");

            entity.HasIndex(e => e.TrainDatasetId, "train_dataset_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SourcePath)
                .HasMaxLength(255)
                .HasColumnName("source_path");
            entity.Property(e => e.StartModelId)
                .HasColumnType("int(11)")
                .HasColumnName("start_model_id");
            entity.Property(e => e.TrainDatasetId)
                .HasColumnType("int(11)")
                .HasColumnName("train_dataset_id");

            entity.HasOne(d => d.StartModel).WithMany(p => p.InverseStartModel)
                .HasForeignKey(d => d.StartModelId)
                .HasConstraintName("model_ibfk_2");

            entity.HasOne(d => d.TrainDataset).WithMany(p => p.Models)
                .HasForeignKey(d => d.TrainDatasetId)
                .HasConstraintName("model_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
