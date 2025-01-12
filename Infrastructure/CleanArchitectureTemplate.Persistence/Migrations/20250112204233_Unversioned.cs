using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitectureTemplate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Unversioned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BaseEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BaseEntity",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
