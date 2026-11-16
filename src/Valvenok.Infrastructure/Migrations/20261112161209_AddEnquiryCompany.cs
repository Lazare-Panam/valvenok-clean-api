using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Valvenok.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnquiryCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Enquiries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Enquiries");
        }
    }
}
