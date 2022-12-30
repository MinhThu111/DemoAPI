using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using API.Demo.Domain.Models;

namespace API.Demo.Infrastructure.Data
{
    public partial class InternshipContext : DbContext
    {
        public InternshipContext()
        {
        }

        public InternshipContext(DbContextOptions<InternshipContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=172.16.23.5;Database=Internship;User Id=thuntm; Password=inter@202210;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.AvatarUrl)
                    .HasMaxLength(250)
                    .HasColumnName("avatar_url");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("first_name");

                entity.Property(e => e.FirstNameSlug)
                    .HasMaxLength(20)
                    .HasColumnName("first_name_slug");

                entity.Property(e => e.FolkId).HasColumnName("folk_id");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("last_name");

                entity.Property(e => e.LastNameSlug)
                    .HasMaxLength(30)
                    .HasColumnName("last_name_slug");

                entity.Property(e => e.NationalityId).HasColumnName("nationality_id");

                entity.Property(e => e.PersonTypeId)
                    .HasColumnName("person_type_id")
                    .HasComment("Dùng để xác định GV hay HS");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");

                entity.Property(e => e.ReligionId).HasColumnName("religion_id");

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .HasColumnName("remark");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Timer)
                    .HasColumnType("datetime")
                    .HasColumnName("timer");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
