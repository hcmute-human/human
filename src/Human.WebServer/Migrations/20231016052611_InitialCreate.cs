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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash" },
                values: new object[] { new Guid("3eec9cbc-602e-4261-a87a-66fd7d1c2628"), "admin@gmail.com", "$2a$11$ZH1RTH8MeU.9PKOMjDmdouNuzjatQ6XWHFnx.wL8Ra4cNSZkGHFlO" });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Permission", "UserId" },
                values: new object[,]
                {
                    { "create:department", new Guid("3eec9cbc-602e-4261-a87a-66fd7d1c2628") },
                    { "delete:department", new Guid("3eec9cbc-602e-4261-a87a-66fd7d1c2628") },
                    { "read:department", new Guid("3eec9cbc-602e-4261-a87a-66fd7d1c2628") },
                    { "update:department", new Guid("3eec9cbc-602e-4261-a87a-66fd7d1c2628") }
                });

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
                name: "Departments");

            migrationBuilder.DropTable(
                name: "UserPasswordResetTokens");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
