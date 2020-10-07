using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiIdentity.Migrations
{
    public partial class swaggerTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Did = table.Column<int>(nullable: false),
                    DName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Did);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Eid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Did = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Eid);
                    table.ForeignKey(
                        name: "FK_Employee_Departments_Did",
                        column: x => x.Did,
                        principalTable: "Departments",
                        principalColumn: "Did",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Did",
                table: "Employee",
                column: "Did");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
