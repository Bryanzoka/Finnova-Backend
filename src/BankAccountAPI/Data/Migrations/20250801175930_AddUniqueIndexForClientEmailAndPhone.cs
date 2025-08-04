using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccountAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexForClientEmailAndPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "client_tel",
                table: "BankClient",
                newName: "client_phone");

            migrationBuilder.CreateIndex(
                name: "IX_BankClient_client_email",
                table: "BankClient",
                column: "client_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankClient_client_phone",
                table: "BankClient",
                column: "client_phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankClient_client_email",
                table: "BankClient");

            migrationBuilder.DropIndex(
                name: "IX_BankClient_client_phone",
                table: "BankClient");

            migrationBuilder.RenameColumn(
                name: "client_phone",
                table: "BankClient",
                newName: "client_tel");
        }
    }
}
