using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pozicija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pozicija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sponzor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Iznos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponzor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ETeam",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaxIgraci = table.Column<int>(type: "int", nullable: false),
                    SpozorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETeam", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ETeam_Sponzor_SpozorID",
                        column: x => x.SpozorID,
                        principalTable: "Sponzor",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Igrac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nadimak = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    GodineStaza = table.Column<int>(type: "int", nullable: false),
                    PozicijaID = table.Column<int>(type: "int", nullable: false),
                    TimID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igrac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Igrac_ETeam_TimID",
                        column: x => x.TimID,
                        principalTable: "ETeam",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Igrac_Pozicija_PozicijaID",
                        column: x => x.PozicijaID,
                        principalTable: "Pozicija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trener",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    GodineStaza = table.Column<int>(type: "int", nullable: false),
                    TimID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trener", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trener_ETeam_TimID",
                        column: x => x.TimID,
                        principalTable: "ETeam",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ETeam_SpozorID",
                table: "ETeam",
                column: "SpozorID");

            migrationBuilder.CreateIndex(
                name: "IX_Igrac_PozicijaID",
                table: "Igrac",
                column: "PozicijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Igrac_TimID",
                table: "Igrac",
                column: "TimID");

            migrationBuilder.CreateIndex(
                name: "IX_Trener_TimID",
                table: "Trener",
                column: "TimID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igrac");

            migrationBuilder.DropTable(
                name: "Trener");

            migrationBuilder.DropTable(
                name: "Pozicija");

            migrationBuilder.DropTable(
                name: "ETeam");

            migrationBuilder.DropTable(
                name: "Sponzor");
        }
    }
}
