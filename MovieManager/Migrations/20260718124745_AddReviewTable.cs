using Microsoft.EntityFrameworkCore.Migrations;
using MovieManager.Models;

#nullable disable

namespace MovieManager.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                    //الاي دي يزيد تلقائيًا
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(

                        //يوجد جدول اسمه Reviews.
                        //مرتبط بجدول Movies.
                        //باستخدام العمود MovieId.
                       // بدون أن اكتب Fluent API.
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        //الجدول الأساسي
                        principalTable: "Movies",
                        //اربط MovieId مع Movies.Id.
                        principalColumn: "Id",
                        //إذا حذفت فيلمًا، احذف تلقائيًا جميع المراجعات التابعة له.
                        onDelete: ReferentialAction.Cascade);
                });

            //هذا ينشئ Index على MovieId.
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
