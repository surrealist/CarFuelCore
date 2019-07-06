using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFuel.Services.Migrations
{
    public partial class update03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Users_OwnerId",
                table: "Cars");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Users_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Users_OwnerId",
                table: "Cars");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Users_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
