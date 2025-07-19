using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccountAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTheNewModelsMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BankClient");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BankClient");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "BankClient");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "BankClient",
                newName: "client_cpf");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "BankAccount",
                newName: "account_cpf");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "BankAccount",
                newName: "account_balance");

            migrationBuilder.RenameColumn(
                name: "AccountType",
                table: "BankAccount",
                newName: "account_type");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "BankAccount",
                newName: "account_id");

            migrationBuilder.AlterColumn<string>(
                name: "client_cpf",
                table: "BankClient",
                type: "VARCHAR(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "client_email",
                table: "BankClient",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "client_name",
                table: "BankClient",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "client_password",
                table: "BankClient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "client_tel",
                table: "BankClient",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "BankClient",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "BankClient",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "BankAccount",
                keyColumn: "account_cpf",
                keyValue: null,
                column: "account_cpf",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "account_cpf",
                table: "BankAccount",
                type: "VARCHAR(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "BankAccount",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_transaction_at",
                table: "BankAccount",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "BankAccount",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_account_cpf",
                table: "BankAccount",
                column: "account_cpf");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_BankClient_account_cpf",
                table: "BankAccount",
                column: "account_cpf",
                principalTable: "BankClient",
                principalColumn: "client_cpf",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankClient_account_cpf",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_account_cpf",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "client_email",
                table: "BankClient");

            migrationBuilder.DropColumn(
                name: "client_name",
                table: "BankClient");

            migrationBuilder.DropColumn(
                name: "client_password",
                table: "BankClient");

            migrationBuilder.DropColumn(
                name: "client_tel",
                table: "BankClient");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "BankClient");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "BankClient");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "last_transaction_at",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "BankAccount");

            migrationBuilder.RenameColumn(
                name: "client_cpf",
                table: "BankClient",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "account_type",
                table: "BankAccount",
                newName: "AccountType");

            migrationBuilder.RenameColumn(
                name: "account_cpf",
                table: "BankAccount",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "account_balance",
                table: "BankAccount",
                newName: "Balance");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "BankAccount",
                newName: "AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "BankClient",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BankClient",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BankClient",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "BankClient",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "BankAccount",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)",
                oldMaxLength: 11)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
