using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infra.Migrations
{
    public partial class ChangingTaxIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaxId",
                table: "Company",
                type: "VARCHAR(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaxId",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)");
        }
    }
}
