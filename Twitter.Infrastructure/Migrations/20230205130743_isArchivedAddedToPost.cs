using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twitter.Infrastructure.Migrations
{
    public partial class isArchivedAddedToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Posts");
        }
    }
}
