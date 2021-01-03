using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservea.Persistance.Migrations
{
    public partial class addtextfieldcontenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextFieldsContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 4080, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextFieldsContents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextFieldsContents");
        }
    }
}
