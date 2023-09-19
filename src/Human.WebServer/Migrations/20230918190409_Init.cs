using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Human.Http.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatingTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    Email = table.Column<string>(type: "character varying(261)", maxLength: 261, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(61)", maxLength: 61, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
