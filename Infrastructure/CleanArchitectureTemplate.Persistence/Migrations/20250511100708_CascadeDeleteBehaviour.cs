using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitectureTemplate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAvatarFiles_FileDetailsId",
                schema: "Files",
                table: "UserAvatarFiles");

            migrationBuilder.DropIndex(
                name: "IX_ReportFiles_FileDetailsId",
                schema: "Files",
                table: "ReportFiles");

            migrationBuilder.DropIndex(
                name: "IX_ProductImageFiles_FileDetailsId",
                schema: "Files",
                table: "ProductImageFiles");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceFiles_FileDetailsId",
                schema: "Files",
                table: "InvoiceFiles");

            migrationBuilder.DropIndex(
                name: "IX_CategoryImageFiles_CategoryId",
                schema: "Files",
                table: "CategoryImageFiles");

            migrationBuilder.DropIndex(
                name: "IX_CategoryImageFiles_FileDetailsId",
                schema: "Files",
                table: "CategoryImageFiles");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                schema: "Shopping",
                table: "BasketItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatarFiles_FileDetailsId",
                schema: "Files",
                table: "UserAvatarFiles",
                column: "FileDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportFiles_FileDetailsId",
                schema: "Files",
                table: "ReportFiles",
                column: "FileDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageFiles_FileDetailsId",
                schema: "Files",
                table: "ProductImageFiles",
                column: "FileDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFiles_FileDetailsId",
                schema: "Files",
                table: "InvoiceFiles",
                column: "FileDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImageFiles_CategoryId",
                schema: "Files",
                table: "CategoryImageFiles",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImageFiles_FileDetailsId",
                schema: "Files",
                table: "CategoryImageFiles",
                column: "FileDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId1",
                schema: "Shopping",
                table: "BasketItems",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Products_ProductId1",
                schema: "Shopping",
                table: "BasketItems",
                column: "ProductId1",
                principalSchema: "Shopping",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Products_ProductId1",
                schema: "Shopping",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_UserAvatarFiles_FileDetailsId",
                schema: "Files",
                table: "UserAvatarFiles");

            migrationBuilder.DropIndex(
                name: "IX_ReportFiles_FileDetailsId",
                schema: "Files",
                table: "ReportFiles");

            migrationBuilder.DropIndex(
                name: "IX_ProductImageFiles_FileDetailsId",
                schema: "Files",
                table: "ProductImageFiles");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceFiles_FileDetailsId",
                schema: "Files",
                table: "InvoiceFiles");

            migrationBuilder.DropIndex(
                name: "IX_CategoryImageFiles_CategoryId",
                schema: "Files",
                table: "CategoryImageFiles");

            migrationBuilder.DropIndex(
                name: "IX_CategoryImageFiles_FileDetailsId",
                schema: "Files",
                table: "CategoryImageFiles");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_ProductId1",
                schema: "Shopping",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                schema: "Shopping",
                table: "BasketItems");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatarFiles_FileDetailsId",
                schema: "Files",
                table: "UserAvatarFiles",
                column: "FileDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFiles_FileDetailsId",
                schema: "Files",
                table: "ReportFiles",
                column: "FileDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageFiles_FileDetailsId",
                schema: "Files",
                table: "ProductImageFiles",
                column: "FileDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFiles_FileDetailsId",
                schema: "Files",
                table: "InvoiceFiles",
                column: "FileDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImageFiles_CategoryId",
                schema: "Files",
                table: "CategoryImageFiles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImageFiles_FileDetailsId",
                schema: "Files",
                table: "CategoryImageFiles",
                column: "FileDetailsId");
        }
    }
}
