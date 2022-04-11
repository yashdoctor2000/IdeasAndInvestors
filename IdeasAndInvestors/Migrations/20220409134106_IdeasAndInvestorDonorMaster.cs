using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeasAndInvestors.Migrations
{
    public partial class IdeasAndInvestorDonorMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iid",
                table: "DonorMasters");

            migrationBuilder.RenameColumn(
                name: "Ddate",
                table: "DonorMasters",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Dcomment",
                table: "DonorMasters",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "Damount",
                table: "DonorMasters",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DonorMasters",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "DonorMasters");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "DonorMasters",
                newName: "Damount");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DonorMasters",
                newName: "Ddate");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "DonorMasters",
                newName: "Dcomment");

            migrationBuilder.AddColumn<int>(
                name: "Iid",
                table: "DonorMasters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
