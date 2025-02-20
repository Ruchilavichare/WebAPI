using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToyAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AffiliateLink",
                table: "Toys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Toys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice",
                table: "Toys",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDealOfTheDay",
                table: "Toys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffiliateLink",
                table: "Toys");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Toys");

            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "Toys");

            migrationBuilder.DropColumn(
                name: "IsDealOfTheDay",
                table: "Toys");
        }
    }
}
