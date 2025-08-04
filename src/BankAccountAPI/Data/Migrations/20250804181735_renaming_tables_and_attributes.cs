using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccountAPI.Migrations
{
    /// <inheritdoc />
    public partial class renaming_tables_and_attributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankClient_account_cpf",
                table: "BankAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientVerificationCode",
                table: "ClientVerificationCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankClient",
                table: "BankClient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "ClientVerificationCode",
                newName: "client_verification_codes");

            migrationBuilder.RenameTable(
                name: "BankClient",
                newName: "clients");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                newName: "accounts");

            migrationBuilder.RenameColumn(
                name: "client_password",
                table: "clients",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "client_cpf",
                table: "clients",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "client_phone",
                table: "clients",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "client_name",
                table: "clients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "client_email",
                table: "clients",
                newName: "email");

            migrationBuilder.RenameIndex(
                name: "IX_BankClient_client_phone",
                table: "clients",
                newName: "IX_clients_phone");

            migrationBuilder.RenameIndex(
                name: "IX_BankClient_client_email",
                table: "clients",
                newName: "IX_clients_email");

            migrationBuilder.RenameColumn(
                name: "account_cpf",
                table: "accounts",
                newName: "client_cpf");

            migrationBuilder.RenameColumn(
                name: "account_balance",
                table: "accounts",
                newName: "balance");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "accounts",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_account_cpf",
                table: "accounts",
                newName: "IX_accounts_client_cpf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_client_verification_codes",
                table: "client_verification_codes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients",
                table: "clients",
                column: "cpf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accounts",
                table: "accounts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_clients_client_cpf",
                table: "accounts",
                column: "client_cpf",
                principalTable: "clients",
                principalColumn: "cpf",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_clients_client_cpf",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clients",
                table: "clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_client_verification_codes",
                table: "client_verification_codes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accounts",
                table: "accounts");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "BankClient");

            migrationBuilder.RenameTable(
                name: "client_verification_codes",
                newName: "ClientVerificationCode");

            migrationBuilder.RenameTable(
                name: "accounts",
                newName: "BankAccount");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "BankClient",
                newName: "client_password");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "BankClient",
                newName: "client_cpf");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "BankClient",
                newName: "client_phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "BankClient",
                newName: "client_name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "BankClient",
                newName: "client_email");

            migrationBuilder.RenameIndex(
                name: "IX_clients_phone",
                table: "BankClient",
                newName: "IX_BankClient_client_phone");

            migrationBuilder.RenameIndex(
                name: "IX_clients_email",
                table: "BankClient",
                newName: "IX_BankClient_client_email");

            migrationBuilder.RenameColumn(
                name: "client_cpf",
                table: "BankAccount",
                newName: "account_cpf");

            migrationBuilder.RenameColumn(
                name: "balance",
                table: "BankAccount",
                newName: "account_balance");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BankAccount",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_client_cpf",
                table: "BankAccount",
                newName: "IX_BankAccount_account_cpf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankClient",
                table: "BankClient",
                column: "client_cpf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientVerificationCode",
                table: "ClientVerificationCode",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount",
                column: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_BankClient_account_cpf",
                table: "BankAccount",
                column: "account_cpf",
                principalTable: "BankClient",
                principalColumn: "client_cpf",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
