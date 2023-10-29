using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProMan.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ticketes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tasks");

            migrationBuilder.AddColumn<long>(
                name: "ColumnStatusId",
                table: "Ticketes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ColumnStatusId",
                table: "Tasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ColumnStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<long>(type: "bigint", nullable: true),
                    TaskId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnStatuses_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ColumnStatuses_Ticketes_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticketes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticketes_ColumnStatusId",
                table: "Ticketes",
                column: "ColumnStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ColumnStatusId",
                table: "Tasks",
                column: "ColumnStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnStatuses_TaskId",
                table: "ColumnStatuses",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnStatuses_TicketId",
                table: "ColumnStatuses",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ColumnStatuses_ColumnStatusId",
                table: "Tasks",
                column: "ColumnStatusId",
                principalTable: "ColumnStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticketes_ColumnStatuses_ColumnStatusId",
                table: "Ticketes",
                column: "ColumnStatusId",
                principalTable: "ColumnStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ColumnStatuses_ColumnStatusId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticketes_ColumnStatuses_ColumnStatusId",
                table: "Ticketes");

            migrationBuilder.DropTable(
                name: "ColumnStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Ticketes_ColumnStatusId",
                table: "Ticketes");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ColumnStatusId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ColumnStatusId",
                table: "Ticketes");

            migrationBuilder.DropColumn(
                name: "ColumnStatusId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Ticketes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
