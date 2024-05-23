using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationManagementPlatform.Migrations
{
    /// <inheritdoc />
    public partial class migbuy2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buys_CourseCategories_CourseCategoryId",
                table: "Buys");

            migrationBuilder.DropIndex(
                name: "IX_Buys_CourseCategoryId",
                table: "Buys");

            migrationBuilder.DropColumn(
                name: "CourseCategoryId",
                table: "Buys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseCategoryId",
                table: "Buys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Buys_CourseCategoryId",
                table: "Buys",
                column: "CourseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buys_CourseCategories_CourseCategoryId",
                table: "Buys",
                column: "CourseCategoryId",
                principalTable: "CourseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
