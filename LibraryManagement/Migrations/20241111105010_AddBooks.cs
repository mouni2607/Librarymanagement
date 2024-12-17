using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                column: "Author",
                value: "Rakesh Kumar");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "ISBN", "IsAvailable", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 5, "Robert C. Martin", "978-0132350884", true, new DateTime(2008, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clean Code: A Handbook of Agile Software Craftsmanship" },
                    { 6, "Robert C. Martin", "978-0137081073", true, new DateTime(2011, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Clean Coder: A Code of Conduct for Professional Programmers" },
                    { 7, "Martin Fowler", "978-0201485677", true, new DateTime(1999, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Refactoring: Improving the Design of Existing Code" },
                    { 8, "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides", "978-0201633610", true, new DateTime(1994, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design Patterns: Elements of Reusable Object-Oriented Software" },
                    { 9, "Alan Beaulieu", "978-0596520830", true, new DateTime(2009, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learning SQL" },
                    { 10, "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest, Clifford Stein", "978-0262033848", true, new DateTime(2009, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Introduction to Algorithms" },
                    { 11, "Donald E. Knuth", "978-0321751041", true, new DateTime(2011, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Art of Computer Programming" },
                    { 12, "Herbert Schildt", "978-0071808552", true, new DateTime(2014, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java: The Complete Reference" },
                    { 13, "Jon Skeet", "978-1617291345", true, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# in Depth" },
                    { 14, "Eric Matthes", "978-1593279288", true, new DateTime(2019, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Python Crash Course" },
                    { 15, "Douglas Crockford", "978-0596517748", true, new DateTime(2008, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "JavaScript: The Good Parts" },
                    { 16, "Frederick P. Brooks Jr.", "978-0201835953", true, new DateTime(1995, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Mythical Man-Month" },
                    { 17, "Abraham Silberschatz, Peter B. Galvin, Greg Gagne", "978-1118063330", true, new DateTime(2013, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Operating System Concepts" },
                    { 18, "Steve McConnell", "978-0735619678", true, new DateTime(2004, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Code Complete" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 18);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                column: "Author",
                value: "Rakesh Kumat");
        }
    }
}
