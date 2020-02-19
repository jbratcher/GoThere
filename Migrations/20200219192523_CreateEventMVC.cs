using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoThere.Migrations
{
    public partial class CreateEventMVC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 144, nullable: false),
                    Description = table.Column<string>(maxLength: 3000, nullable: false),
                    Type = table.Column<string>(maxLength: 144, nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    LocationName = table.Column<string>(maxLength: 144, nullable: false),
                    StreetAddress = table.Column<string>(maxLength: 288, nullable: false),
                    City = table.Column<string>(maxLength: 144, nullable: false),
                    State = table.Column<string>(maxLength: 144, nullable: false),
                    Country = table.Column<string>(maxLength: 144, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 144, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
