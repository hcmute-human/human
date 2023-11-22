using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Human.WebServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    Email = table.Column<string>(type: "character varying(261)", maxLength: 261, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(61)", maxLength: 61, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentPositions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    FirstName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    DateOfBirth = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPasswordResetTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpirationTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswordResetTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_UserPasswordResetTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Permission = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.Permission });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Token = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    ExpiryTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => new { x.UserId, x.Token });
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePositions",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentPositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    StartTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    EmploymentType = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositions", x => new { x.EmployeeId, x.DepartmentPositionId });
                    table.ForeignKey(
                        name: "FK_EmployeePositions_DepartmentPositions_DepartmentPositionId",
                        column: x => x.DepartmentPositionId,
                        principalTable: "DepartmentPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePositions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash" },
                values: new object[] { new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084"), "admin@gmail.com", "$2a$11$64yVH5hVA6oxBKOAWEdE6OV6eYlzdP86UQWNJ7ox/pOnoki.o5vc6" });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Permission", "UserId" },
                values: new object[,]
                {
                    { "create:department", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "create:departmentPosition", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "create:employee", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "create:employeePosition", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "delete:department", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "delete:departmentPosition", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "delete:employee", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "delete:employeePosition", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "read:department", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "read:departmentPosition", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "read:employee", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "read:employeePosition", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "update:department", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "update:departmentPosition", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "update:employee", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") },
                    { "update:employeePosition", new Guid("bb81a9cd-c181-4b32-a1c1-967067e85084") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositions_DepartmentId",
                table: "DepartmentPositions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositions_DepartmentPositionId",
                table: "EmployeePositions",
                column: "DepartmentPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswordResetTokens_UserId",
                table: "UserPasswordResetTokens",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePositions");

            migrationBuilder.DropTable(
                name: "UserPasswordResetTokens");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "DepartmentPositions");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
