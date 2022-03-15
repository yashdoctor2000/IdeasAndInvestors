using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeasAndInvestors.Migrations
{
    public partial class IandIThird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Catimage",
                table: "CategoryMasters",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catimage",
                table: "CategoryMasters");
        }
    }
}
