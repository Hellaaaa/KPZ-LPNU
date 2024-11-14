using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    contact_info = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    resume = table.Column<string>(type: "TEXT", nullable: true),
                    work_experience = table.Column<string>(type: "TEXT", nullable: true),
                    education = table.Column<string>(type: "TEXT", nullable: true),
                    current_status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    industry = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    company_size = table.Column<string>(type: "TEXT", nullable: true),
                    location = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    website = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employer",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 45, nullable: true),
                    address = table.Column<string>(type: "TEXT", maxLength: 45, nullable: true),
                    company_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employer", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employer_Company_company_id",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Recruiter",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    contact_info = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    specialization = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    successful_closures = table.Column<int>(type: "INTEGER", nullable: false),
                    company_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiter", x => x.id);
                    table.ForeignKey(
                        name: "FK_Recruiter_Company_company_id",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkill",
                columns: table => new
                {
                    candidate_id = table.Column<int>(type: "INTEGER", nullable: false),
                    skill_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkill", x => new { x.candidate_id, x.skill_id });
                    table.ForeignKey(
                        name: "FK_CandidateSkill_Candidate_candidate_id",
                        column: x => x.candidate_id,
                        principalTable: "Candidate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkill_Skill_skill_id",
                        column: x => x.skill_id,
                        principalTable: "Skill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobVacancy",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    job_title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    job_description = table.Column<string>(type: "TEXT", nullable: true),
                    requirements = table.Column<string>(type: "TEXT", nullable: true),
                    salary = table.Column<double>(type: "REAL", nullable: true),
                    currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true),
                    location = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    recruiter_id = table.Column<int>(type: "INTEGER", nullable: true),
                    vacancy_status = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Open")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancy", x => x.id);
                    table.ForeignKey(
                        name: "FK_JobVacancy_Recruiter_recruiter_id",
                        column: x => x.recruiter_id,
                        principalTable: "Recruiter",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HiringEvent",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    vacancy_id = table.Column<int>(type: "INTEGER", nullable: true),
                    candidate_id = table.Column<int>(type: "INTEGER", nullable: true),
                    recruiter_id = table.Column<int>(type: "INTEGER", nullable: true),
                    feedback = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringEvent", x => x.id);
                    table.ForeignKey(
                        name: "FK_HiringEvent_Candidate_candidate_id",
                        column: x => x.candidate_id,
                        principalTable: "Candidate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringEvent_JobVacancy_vacancy_id",
                        column: x => x.vacancy_id,
                        principalTable: "JobVacancy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringEvent_Recruiter_recruiter_id",
                        column: x => x.recruiter_id,
                        principalTable: "Recruiter",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobVacancySkill",
                columns: table => new
                {
                    job_vacancy_id = table.Column<int>(type: "INTEGER", nullable: false),
                    skill_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancySkill", x => new { x.job_vacancy_id, x.skill_id });
                    table.ForeignKey(
                        name: "FK_JobVacancySkill_JobVacancy_job_vacancy_id",
                        column: x => x.job_vacancy_id,
                        principalTable: "JobVacancy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobVacancySkill_Skill_skill_id",
                        column: x => x.skill_id,
                        principalTable: "Skill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HiringEventStage",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    hiring_event_id = table.Column<int>(type: "INTEGER", nullable: false),
                    stage = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: false),
                    start_date = table.Column<DateTime>(type: "TEXT", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    end_date = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringEventStage", x => x.id);
                    table.ForeignKey(
                        name: "FK_HiringEventStage_HiringEvent_hiring_event_id",
                        column: x => x.hiring_event_id,
                        principalTable: "HiringEvent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_candidate_id",
                table: "CandidateSkill",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_skill_id",
                table: "CandidateSkill",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_company_id",
                table: "Employer",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_email",
                table: "Employer",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_phone",
                table: "Employer",
                column: "phone");

            migrationBuilder.CreateIndex(
                name: "IX_HiringEvent_candidate_id",
                table: "HiringEvent",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_HiringEvent_recruiter_id",
                table: "HiringEvent",
                column: "recruiter_id");

            migrationBuilder.CreateIndex(
                name: "IX_HiringEvent_vacancy_id",
                table: "HiringEvent",
                column: "vacancy_id");

            migrationBuilder.CreateIndex(
                name: "IX_HiringEventStage_hiring_event_id",
                table: "HiringEventStage",
                column: "hiring_event_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancy_recruiter_id",
                table: "JobVacancy",
                column: "recruiter_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancySkill_job_vacancy_id",
                table: "JobVacancySkill",
                column: "job_vacancy_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancySkill_skill_id",
                table: "JobVacancySkill",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiter_company_id",
                table: "Recruiter",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_name",
                table: "Skill",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateSkill");

            migrationBuilder.DropTable(
                name: "Employer");

            migrationBuilder.DropTable(
                name: "HiringEventStage");

            migrationBuilder.DropTable(
                name: "JobVacancySkill");

            migrationBuilder.DropTable(
                name: "HiringEvent");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "JobVacancy");

            migrationBuilder.DropTable(
                name: "Recruiter");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
