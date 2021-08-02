using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDelivery.Migrations
{
    public partial class storenumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Stores",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "StoreNumber",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreNumber",
                table: "Stores");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Stores",
                newName: "Street");
        }
    }
}
