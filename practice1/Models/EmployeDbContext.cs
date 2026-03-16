using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace practice1.Models;

public partial class EmployeDbContext : DbContext
{
    public EmployeDbContext()
    {
    }

    public EmployeDbContext(DbContextOptions<EmployeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__262359ABCD0DCE37");

            entity.Property(e => e.EmpId)
                .ValueGeneratedNever()
                .HasColumnName("Emp_Id");
            entity.Property(e => e.Department)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
