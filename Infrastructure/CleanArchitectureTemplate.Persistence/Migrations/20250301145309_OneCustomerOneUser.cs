using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitectureTemplate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OneCustomerOneUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                schema: "Membership",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Users_UserId",
                schema: "Membership",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_UserId",
                schema: "Membership",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                schema: "Membership",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Membership",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Membership",
                table: "Customers");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_Id",
                schema: "Membership",
                table: "Customers",
                column: "Id",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Users_Id",
                schema: "Membership",
                table: "Stores",
                column: "Id",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_Id",
                schema: "Membership",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Users_Id",
                schema: "Membership",
                table: "Stores");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Membership",
                table: "Stores",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Membership",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UserId",
                schema: "Membership",
                table: "Stores",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                schema: "Membership",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                schema: "Membership",
                table: "Customers",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Users_UserId",
                schema: "Membership",
                table: "Stores",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
