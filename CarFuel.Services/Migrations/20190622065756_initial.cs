using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFuel.Services.Migrations
{
  public partial class initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Cars",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            Make = table.Column<string>(nullable: true),
            Color = table.Column<string>(nullable: true),
            IsStarted = table.Column<bool>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Cars", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "FillUp",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            Odometer = table.Column<int>(nullable: false),
            Liters = table.Column<double>(nullable: false),
            IsFull = table.Column<bool>(nullable: false),
            NextFillUpId = table.Column<int>(nullable: true),
            CarId = table.Column<Guid>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_FillUp", x => x.Id);
            table.ForeignKey(
                      name: "FK_FillUp_Cars_CarId",
                      column: x => x.CarId,
                      principalTable: "Cars",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_FillUp_FillUp_NextFillUpId",
                      column: x => x.NextFillUpId,
                      principalTable: "FillUp",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateIndex(
          name: "IX_FillUp_CarId",
          table: "FillUp",
          column: "CarId");

      migrationBuilder.CreateIndex(
          name: "IX_FillUp_NextFillUpId",
          table: "FillUp",
          column: "NextFillUpId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "FillUp");

      migrationBuilder.DropTable(
          name: "Cars");
    }
  }
}
