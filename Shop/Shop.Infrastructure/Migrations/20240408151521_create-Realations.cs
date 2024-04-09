using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class createRealations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE [seller].Inventories
            ADD CONSTRAINT FK_Inventories_products_ProductId
            FOREIGN KEY (ProductId) REFERENCES [product].products(Id);");

            migrationBuilder.Sql(@"
            ALTER TABLE [order].Items
            ADD CONSTRAINT FK_Items_Inventories_InventoryId
            FOREIGN KEY (InventoryId) REFERENCES [seller].Inventories(Id);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
