using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeasAndInvestors.Migrations
{
    public partial class IdeasAndInvestorsfirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryMasters",
                columns: table => new
                {
                    Catid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Catname = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMasters", x => x.Catid);
                });

            migrationBuilder.CreateTable(
                name: "ComplainMasters",
                columns: table => new
                {
                    Compid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Cdetails = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainMasters", x => x.Compid);
                });

            migrationBuilder.CreateTable(
                name: "DonorMasters",
                columns: table => new
                {
                    Did = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iid = table.Column<int>(type: "int", nullable: false),
                    Ddate = table.Column<string>(type: "varchar(50)", nullable: false),
                    Damount = table.Column<string>(type: "varchar(20)", nullable: false),
                    Dcomment = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorMasters", x => x.Did);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackMasters",
                columns: table => new
                {
                    Fid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Fdetails = table.Column<string>(type: "varchar(200)", nullable: false),
                    Experiencerate = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackMasters", x => x.Fid);
                });

            migrationBuilder.CreateTable(
                name: "IdeaMasters",
                columns: table => new
                {
                    Iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ititle = table.Column<string>(type: "varchar(50)", nullable: false),
                    Idescription = table.Column<string>(type: "varchar(200)", nullable: false),
                    IinvestmentNeeded = table.Column<string>(type: "varchar(50)", nullable: false),
                    IinvestmentDuration = table.Column<string>(type: "varchar(50)", nullable: false),
                    Iimage = table.Column<string>(type: "varchar(200)", nullable: false),
                    Idate = table.Column<string>(type: "varchar(50)", nullable: false),
                    Ividurl = table.Column<string>(type: "varchar(200)", nullable: false),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Catid = table.Column<int>(type: "int", nullable: false),
                    IRFLT10Pnt = table.Column<string>(type: "varchar(50)", nullable: false),
                    IRFLT20Pnt = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaMasters", x => x.Iid);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentMasters",
                columns: table => new
                {
                    Insid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iid = table.Column<int>(type: "int", nullable: false),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Insdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Instime = table.Column<string>(type: "varchar(50)", nullable: false),
                    Insamount = table.Column<int>(type: "int", nullable: false),
                    Instype = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentMasters", x => x.Insid);
                });

            migrationBuilder.CreateTable(
                name: "PersonMasters",
                columns: table => new
                {
                    Pid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Paddress = table.Column<string>(type: "varchar(200)", nullable: false),
                    Pdob = table.Column<string>(type: "varchar(50)", nullable: false),
                    Pgender = table.Column<string>(type: "varchar(10)", nullable: false),
                    Pphone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Pqualification = table.Column<string>(type: "varchar(50)", nullable: false),
                    Pemail = table.Column<string>(type: "varchar(20)", nullable: false),
                    Ppassword = table.Column<string>(type: "varchar(20)", nullable: false),
                    Pimage = table.Column<string>(type: "varchar(200)", nullable: false),
                    Pqid = table.Column<int>(type: "int", nullable: false),
                    Panswer = table.Column<string>(type: "varchar(100)", nullable: false),
                    Prollid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMasters", x => x.Pid);
                });

            migrationBuilder.CreateTable(
                name: "QuestionMasters",
                columns: table => new
                {
                    Qid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Questiontext = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionMasters", x => x.Qid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMasters");

            migrationBuilder.DropTable(
                name: "ComplainMasters");

            migrationBuilder.DropTable(
                name: "DonorMasters");

            migrationBuilder.DropTable(
                name: "FeedbackMasters");

            migrationBuilder.DropTable(
                name: "IdeaMasters");

            migrationBuilder.DropTable(
                name: "InvestmentMasters");

            migrationBuilder.DropTable(
                name: "PersonMasters");

            migrationBuilder.DropTable(
                name: "QuestionMasters");
        }
    }
}
