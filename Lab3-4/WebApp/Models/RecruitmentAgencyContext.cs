using System;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public partial class RecruitmentAgencyContext : DbContext
    {
        public RecruitmentAgencyContext()
        {
        }

        public RecruitmentAgencyContext(DbContextOptions<RecruitmentAgencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<HiringEvent> HiringEvents { get; set; }
        public virtual DbSet<HiringEventStage> HiringEventStages { get; set; }
        public virtual DbSet<JobVacancy> JobVacancies { get; set; }
        public virtual DbSet<Recruiter> Recruiters { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=recruitment_agency.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Database.IsSqlite())
            {
                ConfigureForSQLite(modelBuilder);
            }

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Candidate");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ContactInfo)
                    .HasMaxLength(255)
                    .HasColumnName("contact_info");
                entity.Property(e => e.CurrentStatus)
                    .HasColumnName("current_status");
                entity.Property(e => e.Education)
                    .HasColumnName("education");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(45)
                    .HasColumnName("last_name");
                entity.Property(e => e.Resume)
                    .HasColumnName("resume");
                entity.Property(e => e.WorkExperience)
                    .HasColumnName("work_experience");

                entity.HasMany(d => d.Skills)
                    .WithMany(p => p.Candidates)
                    .UsingEntity<Dictionary<string, object>>(
                        "CandidateSkill",
                        r => r.HasOne<Skill>().WithMany()
                            .HasForeignKey("SkillId")
                            .OnDelete(DeleteBehavior.Cascade),
                        l => l.HasOne<Candidate>().WithMany()
                            .HasForeignKey("CandidateId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("CandidateId", "SkillId");
                            j.ToTable("CandidateSkill");
                            j.HasIndex(new[] { "CandidateId" });
                            j.HasIndex(new[] { "SkillId" });
                            j.Property<int>("CandidateId").HasColumnName("candidate_id");
                            j.Property<int>("SkillId").HasColumnName("skill_id");
                        });
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Company");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CompanySize)
                    .HasColumnName("company_size");
                entity.Property(e => e.Industry)
                    .HasMaxLength(100)
                    .HasColumnName("industry");
                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .HasColumnName("location");
                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
                entity.Property(e => e.Website)
                    .HasMaxLength(255)
                    .HasColumnName("website");
            });

            modelBuilder.Entity<Employer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Employer");

                // Remove unique constraints for SQLite compatibility
                if (Database.IsSqlite())
                {
                    entity.HasIndex(e => e.Email).IsUnique(false);
                    entity.HasIndex(e => e.Phone).IsUnique(false);
                }
                else
                {
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.HasIndex(e => e.Phone).IsUnique();
                }

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(45)
                    .HasColumnName("address");
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(45)
                    .HasColumnName("last_name");
                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employers)
                    .HasForeignKey(d => d.CompanyId);
            });

            modelBuilder.Entity<HiringEvent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("HiringEvent");

                entity.HasIndex(e => e.CandidateId);
                entity.HasIndex(e => e.VacancyId);
                entity.HasIndex(e => e.RecruiterId);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CandidateId).HasColumnName("candidate_id");
                entity.Property(e => e.Feedback).HasColumnName("feedback");
                entity.Property(e => e.RecruiterId).HasColumnName("recruiter_id");
                entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.HiringEvents)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Recruiter)
                    .WithMany(p => p.HiringEvents)
                    .HasForeignKey(d => d.RecruiterId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.HiringEvents)
                    .HasForeignKey(d => d.VacancyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HiringEventStage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("HiringEventStage");

                entity.HasIndex(e => e.HiringEventId);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.HiringEventId).HasColumnName("hiring_event_id");
                entity.Property(e => e.Stage).HasColumnName("stage");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.EndDate).HasColumnName("end_date");
                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.HiringEvent)
                    .WithMany(p => p.HiringEventStages)
                    .HasForeignKey(d => d.HiringEventId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<JobVacancy>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("JobVacancy");

                entity.HasIndex(e => e.RecruiterId);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .HasColumnName("currency");
                entity.Property(e => e.JobDescription).HasColumnName("job_description");
                entity.Property(e => e.JobTitle)
                    .HasMaxLength(255)
                    .HasColumnName("job_title");
                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .HasColumnName("location");
                entity.Property(e => e.RecruiterId).HasColumnName("recruiter_id");
                entity.Property(e => e.Requirements).HasColumnName("requirements");
                
                // Convert decimal to double for SQLite
                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasConversion<double>();

                entity.Property(e => e.VacancyStatus)
                    .HasColumnName("vacancy_status")
                    .HasDefaultValue("Open")
                    .HasConversion<string>();

                entity.HasOne(d => d.Recruiter)
                    .WithMany(p => p.JobVacancies)
                    .HasForeignKey(d => d.RecruiterId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Skills)
                    .WithMany(p => p.JobVacancies)
                    .UsingEntity<Dictionary<string, object>>(
                        "JobVacancySkill",
                        r => r.HasOne<Skill>().WithMany()
                            .HasForeignKey("SkillId")
                            .OnDelete(DeleteBehavior.Cascade),
                        l => l.HasOne<JobVacancy>().WithMany()
                            .HasForeignKey("JobVacancyId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("JobVacancyId", "SkillId");
                            j.ToTable("JobVacancySkill");
                            j.HasIndex(new[] { "JobVacancyId" });
                            j.HasIndex(new[] { "SkillId" });
                            j.Property<int>("JobVacancyId").HasColumnName("job_vacancy_id");
                            j.Property<int>("SkillId").HasColumnName("skill_id");
                        });
            });

            modelBuilder.Entity<Recruiter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Recruiter");

                entity.HasIndex(e => e.CompanyId);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.ContactInfo)
                    .HasMaxLength(255)
                    .HasColumnName("contact_info");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(45)
                    .HasColumnName("last_name");
                entity.Property(e => e.Specialization)
                    .HasMaxLength(100)
                    .HasColumnName("specialization");
                entity.Property(e => e.SuccessfulClosures).HasColumnName("successful_closures");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Recruiters)
                    .HasForeignKey(d => d.CompanyId);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Skill");

                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });
        }

        private void ConfigureForSQLite(ModelBuilder modelBuilder)
        {
            // Configure decimal properties to be stored as double for SQLite compatibility
            modelBuilder.Entity<JobVacancy>(entity =>
            {
                entity.Property(e => e.Salary)
                    .HasConversion<double>()
                    .HasColumnName("salary");
            });

            // Configure enums as strings where SQLite doesn't support enums directly
            modelBuilder.Entity<JobVacancy>(entity =>
            {
                entity.Property(e => e.VacancyStatus)
                    .HasConversion<string>()
                    .HasDefaultValue("Open")
                    .HasColumnName("vacancy_status");
            });

            modelBuilder.Entity<HiringEventStage>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasColumnName("status");

                entity.Property(e => e.Stage)
                    .HasConversion<string>()
                    .HasColumnName("stage");
            });

            // Remove unique constraints on nullable fields to avoid issues with SQLite
            modelBuilder.Entity<Employer>(entity =>
            {
                // Remove unique constraints on Email and Phone for SQLite compatibility
                entity.HasIndex(e => e.Email).IsUnique(false);
                entity.HasIndex(e => e.Phone).IsUnique(false);
            });

            // Set up many-to-many relationships for SQLite without custom table configurations
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Skills)
                .WithMany(s => s.Candidates)
                .UsingEntity<Dictionary<string, object>>(
                    "CandidateSkill",
                    right => right.HasOne<Skill>().WithMany().HasForeignKey("SkillId").OnDelete(DeleteBehavior.Cascade),
                    left => left.HasOne<Candidate>().WithMany().HasForeignKey("CandidateId").OnDelete(DeleteBehavior.Cascade),
                    joinEntity =>
                    {
                        joinEntity.HasKey("CandidateId", "SkillId");
                        joinEntity.ToTable("CandidateSkill");
                        joinEntity.Property<int>("CandidateId").HasColumnName("candidate_id");
                        joinEntity.Property<int>("SkillId").HasColumnName("skill_id");
                    });

            modelBuilder.Entity<JobVacancy>()
                .HasMany(jv => jv.Skills)
                .WithMany(s => s.JobVacancies)
                .UsingEntity<Dictionary<string, object>>(
                    "JobVacancySkill",
                    right => right.HasOne<Skill>().WithMany().HasForeignKey("SkillId").OnDelete(DeleteBehavior.Cascade),
                    left => left.HasOne<JobVacancy>().WithMany().HasForeignKey("JobVacancyId").OnDelete(DeleteBehavior.Cascade),
                    joinEntity =>
                    {
                        joinEntity.HasKey("JobVacancyId", "SkillId");
                        joinEntity.ToTable("JobVacancySkill");
                        joinEntity.Property<int>("JobVacancyId").HasColumnName("job_vacancy_id");
                        joinEntity.Property<int>("SkillId").HasColumnName("skill_id");
                    });
        }

        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}