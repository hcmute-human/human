﻿using System;
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
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Days = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    Email = table.Column<string>(type: "character varying(261)", maxLength: 261, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(61)", maxLength: 61, nullable: false),
                    Avatar_Key = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Avatar_Format = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    Avatar_Version = table.Column<long>(type: "bigint", nullable: true)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Permission = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "LeaveApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    UpdatedTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    IssuerId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ProcessorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Employees_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Employees_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveApplications_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash" },
                values: new object[] { new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f"), "admin@gmail.com", "$2a$11$3pvpjARf7ZlxGNHXja/uqucqrt4xdKJUR44W1T8Lceazax1P6.cj." });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Permission", "UserId" },
                values: new object[,]
                {
                    { "apply:leaveApplication", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "create:department", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "create:departmentPosition", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "create:employee", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "create:employeePosition", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "create:leaveApplication", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "create:leaveType", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "create:user", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "create:userPermission", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "delete:department", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "delete:departmentPosition", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "delete:employee", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "delete:employeePosition", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "delete:leaveApplication", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "delete:leaveType", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "delete:user", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "delete:userPermission", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "process:leaveApplication", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "read:department", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "read:departmentPosition", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "read:employee", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "read:employeePosition", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "read:leaveApplication", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "read:leaveType", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "read:user", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "read:userPermission", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "update:department", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "update:departmentPosition", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "update:employee", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "update:employeePosition", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "update:leaveApplication", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "update:leaveType", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "update:user", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") },
                    { "update:userPermission", new Guid("ce24d891-6c5a-41df-8f6b-8ce993652b5f") }
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
                name: "IX_LeaveApplications_IssuerId",
                table: "LeaveApplications",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_LeaveTypeId",
                table: "LeaveApplications",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_ProcessorId",
                table: "LeaveApplications",
                column: "ProcessorId");

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
                name: "LeaveApplications");

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
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
