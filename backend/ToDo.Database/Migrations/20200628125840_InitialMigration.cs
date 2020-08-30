using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Database.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Estimation = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Assignee = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Assignee",
                table: "Todos",
                column: "Assignee");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Author",
                table: "Todos",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TableId",
                table: "Todos",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
