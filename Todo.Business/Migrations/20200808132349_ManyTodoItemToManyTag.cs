using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Business.Migrations
{
    public partial class ManyTodoItemToManyTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TodoItemTag",
                columns: table => new
                {
                    TodoItemID = table.Column<int>(nullable: false),
                    TagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItemTag", x => new { x.TodoItemID, x.TagID });
                    table.ForeignKey(
                        name: "FK_TodoItemTag_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoItemTag_TodoItems_TodoItemID",
                        column: x => x.TodoItemID,
                        principalTable: "TodoItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItemTag_TagID",
                table: "TodoItemTag",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItemTag");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
