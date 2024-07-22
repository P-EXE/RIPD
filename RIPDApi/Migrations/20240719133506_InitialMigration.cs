using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RIPDApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diaries",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diaries", x => x.OwnerId);
                    table.ForeignKey(
                        name: "FK_Diaries_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContributerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Energy = table.Column<float>(type: "real", nullable: true),
                    Water = table.Column<float>(type: "real", nullable: true),
                    Protein = table.Column<float>(type: "real", nullable: true),
                    Fat = table.Column<float>(type: "real", nullable: true),
                    Carbohydrates = table.Column<float>(type: "real", nullable: true),
                    Fiber = table.Column<float>(type: "real", nullable: true),
                    Sugar = table.Column<float>(type: "real", nullable: true),
                    Calcium = table.Column<float>(type: "real", nullable: true),
                    Iron = table.Column<float>(type: "real", nullable: true),
                    Magnesium = table.Column<float>(type: "real", nullable: true),
                    Phosphorus = table.Column<float>(type: "real", nullable: true),
                    Potassium = table.Column<float>(type: "real", nullable: true),
                    Sodium = table.Column<float>(type: "real", nullable: true),
                    Zinc = table.Column<float>(type: "real", nullable: true),
                    Copper = table.Column<float>(type: "real", nullable: true),
                    Manganese = table.Column<float>(type: "real", nullable: true),
                    Selenium = table.Column<float>(type: "real", nullable: true),
                    VitaminC = table.Column<float>(type: "real", nullable: true),
                    VitaminB1 = table.Column<float>(type: "real", nullable: true),
                    VitaminB2 = table.Column<float>(type: "real", nullable: true),
                    VitaminB3 = table.Column<float>(type: "real", nullable: true),
                    VitaminB5 = table.Column<float>(type: "real", nullable: true),
                    VitaminB6 = table.Column<float>(type: "real", nullable: true),
                    VitaminB9 = table.Column<float>(type: "real", nullable: true),
                    VitaminB12 = table.Column<float>(type: "real", nullable: true),
                    VitaminA1 = table.Column<float>(type: "real", nullable: true),
                    Retinol = table.Column<float>(type: "real", nullable: true),
                    AlphaCarotin = table.Column<float>(type: "real", nullable: true),
                    BetaCarotin = table.Column<float>(type: "real", nullable: true),
                    BetaCryptoxanthin = table.Column<float>(type: "real", nullable: true),
                    Lycopene = table.Column<float>(type: "real", nullable: true),
                    LutZea = table.Column<float>(type: "real", nullable: true),
                    VitaminE = table.Column<float>(type: "real", nullable: true),
                    VitaminD = table.Column<float>(type: "real", nullable: true),
                    VitaminK = table.Column<float>(type: "real", nullable: true),
                    Choline = table.Column<float>(type: "real", nullable: true),
                    Alcohol = table.Column<float>(type: "real", nullable: true),
                    FattyAcidsSaturated = table.Column<float>(type: "real", nullable: true),
                    FattyAcidsMono = table.Column<float>(type: "real", nullable: true),
                    FattyAcidsPoly = table.Column<float>(type: "real", nullable: true),
                    Cholesterol = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_AspNetUsers_ContributerId",
                        column: x => x.ContributerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Foods_AspNetUsers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContributerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Energy = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_AspNetUsers_ContributerId",
                        column: x => x.ContributerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BodyMetrics",
                columns: table => new
                {
                    DiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Acted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMetrics", x => new { x.DiaryId, x.EntryNr });
                    table.ForeignKey(
                        name: "FK_BodyMetrics_Diaries_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "Diaries",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaryRuns",
                columns: table => new
                {
                    DiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MongoDBId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryRuns", x => new { x.DiaryId, x.EntryNr });
                    table.ForeignKey(
                        name: "FK_DiaryRuns_Diaries_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "Diaries",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaryFoods",
                columns: table => new
                {
                    DiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Acted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryFoods", x => new { x.DiaryId, x.EntryNr });
                    table.ForeignKey(
                        name: "FK_DiaryFoods_Diaries_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "Diaries",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaryFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiaryWorkouts",
                columns: table => new
                {
                    DiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Acted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryWorkouts", x => new { x.DiaryId, x.EntryNr });
                    table.ForeignKey(
                        name: "FK_DiaryWorkouts_Diaries_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "Diaries",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaryWorkouts_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FitnessTargets",
                columns: table => new
                {
                    DiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryNr = table.Column<int>(type: "int", nullable: false),
                    BodyMetricUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartBodyMetricEntryNr = table.Column<int>(type: "int", nullable: false),
                    GoalBodyMetricEntryNr = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessTargets", x => new { x.DiaryId, x.EntryNr });
                    table.ForeignKey(
                        name: "FK_FitnessTargets_BodyMetrics_BodyMetricUser_GoalBodyMetricEntryNr",
                        columns: x => new { x.BodyMetricUser, x.GoalBodyMetricEntryNr },
                        principalTable: "BodyMetrics",
                        principalColumns: new[] { "DiaryId", "EntryNr" });
                    table.ForeignKey(
                        name: "FK_FitnessTargets_BodyMetrics_BodyMetricUser_StartBodyMetricEntryNr",
                        columns: x => new { x.BodyMetricUser, x.StartBodyMetricEntryNr },
                        principalTable: "BodyMetrics",
                        principalColumns: new[] { "DiaryId", "EntryNr" });
                    table.ForeignKey(
                        name: "FK_FitnessTargets_Diaries_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "Diaries",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryFoods_FoodId",
                table: "DiaryFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryWorkouts_WorkoutId",
                table: "DiaryWorkouts",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessTargets_BodyMetricUser_GoalBodyMetricEntryNr",
                table: "FitnessTargets",
                columns: new[] { "BodyMetricUser", "GoalBodyMetricEntryNr" });

            migrationBuilder.CreateIndex(
                name: "IX_FitnessTargets_BodyMetricUser_StartBodyMetricEntryNr",
                table: "FitnessTargets",
                columns: new[] { "BodyMetricUser", "StartBodyMetricEntryNr" });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ContributerId",
                table: "Foods",
                column: "ContributerId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ManufacturerId",
                table: "Foods",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ContributerId",
                table: "Workouts",
                column: "ContributerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DiaryFoods");

            migrationBuilder.DropTable(
                name: "DiaryRuns");

            migrationBuilder.DropTable(
                name: "DiaryWorkouts");

            migrationBuilder.DropTable(
                name: "FitnessTargets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "BodyMetrics");

            migrationBuilder.DropTable(
                name: "Diaries");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
