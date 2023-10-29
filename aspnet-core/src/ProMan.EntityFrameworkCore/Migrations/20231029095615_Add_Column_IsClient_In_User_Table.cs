using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProMan.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_IsClient_In_User_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClient",
                table: "AbpUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClient",
                table: "AbpUsers");
        }
    }
}
