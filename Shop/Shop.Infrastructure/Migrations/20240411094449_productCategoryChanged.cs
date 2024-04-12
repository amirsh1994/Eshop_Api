using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class productCategoryChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstSubCategoryId",
                schema: "product",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                schema: "product",
                table: "Images",
                newName: "Sequence");

            migrationBuilder.AddColumn<long>(
                name: "SecondarySubCategoryId",
                schema: "product",
                table: "products",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondarySubCategoryId",
                schema: "product",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "Sequence",
                schema: "product",
                table: "Images",
                newName: "SortOrder");

            migrationBuilder.AddColumn<long>(
                name: "FirstSubCategoryId",
                schema: "product",
                table: "products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
