using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservea.Persistance.Migrations
{
    public partial class AddIsDeletedFlagToResourceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ResourceTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ResourceTypes");
        }
    }
}
