using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheBookshelf.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAndCategoryAndSeedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Action" },
                    { 2, 2, "Sci-Fi" },
                    { 3, 3, "History" },
                    { 4, 4, "Fantasy" },
                    { 5, 5, "Horror" },
                    { 6, 6, "Romance" },
                    { 7, 7, "Mystery" },
                    { 8, 8, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Stephen King", 5, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis faucibus accumsan nulla, non ultrices sem. Donec at felis ut turpis condimentum elementum.", "9788556511928", "", 30.0, 27.0, 21.0, 24.0, "Holly" },
                    { 2, "Robert Jordan", 4, "Suspendisse eu magna vitae ex consequat placerat. Morbi vel pretium nunc, in imperdiet dui...", "0812511816", "", 8.0, 7.0, 5.5, 6.0, "The Eye of the World" },
                    { 3, "George R. R. Martin", 4, "Donec eu dapibus arcu. Praesent egestas, sem sed auctor malesuada, sapien ligula dignissim urna, sed feugiat ante massa id nisl.", "0553808044", "", 35.0, 32.0, 25.0, 28.0, "A Game of Thrones: The Illustrated Edition: A Song of Ice and Fire: Book One" },
                    { 4, "Stephen King", 4, "Fusce accumsan orci diam, sed commodo massa gravida id. Nunc ut ante cursus, scelerisque nisl sit amet, ornare eros.", "9781668002179", "", 17.0, 15.0, 12.0, 14.0, "Fairy Tale" },
                    { 5, "Hannah Nicole Maehrer", 6, "Ut mattis placerat odio, et lobortis ipsum volutpat ut. Nullam laoreet metus eu urna pellentesque auctor. Pellentesque et diam nec nisl dictum tincidunt.", "9788556511928", "", 17.0, 16.0, 11.0, 14.0, "Assistant to the Villian" },
                    { 6, "Sarah J. Maas", 4, "Sed maximus purus eget libero placerat, accumsan viverra nisl pulvinar. Ut arcu nulla, feugiat at metus vel, volutpat iaculis quam.", "9781639732869", "", 29.0, 27.0, 22.0, 25.0, "House of Flame and Shadow" },
                    { 7, "William Kent Krueger", 7, "Donec ipsum mauris, rutrum ut arcu in, imperdiet euismod orci. Fusce et odio quis tellus feugiat luctus.", "9781668047903", "", 21.0, 20.0, 17.0, 18.0, "The River We Remember" },
                    { 8, "Harper Lee", 8, "Curabitur sodales sodales lectus, vel feugiat arcu aliquam eget. Pellentesque eu turpis eu nunc aliquam dignissim.", "0446310786", "", 13.0, 11.0, 9.0, 10.0, "To Kill a Mockingbird" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
