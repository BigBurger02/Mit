using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIT_1.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Plans_PlanId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Plans_PlanId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Users_PlanId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PlanId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "Limit",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Limit",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlanId",
                table: "Users",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PlanId",
                table: "Invoices",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Plans_PlanId",
                table: "Invoices",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Plans_PlanId",
                table: "Users",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
