using Microsoft.EntityFrameworkCore.Migrations;

namespace EventReporting.DataAccessLayer.Migrations
{
    public partial class InitialSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 101, "Zagreb" },
                    { 102, "Velika Gorica" }
                });

            migrationBuilder.InsertData(
                table: "Settlements",
                columns: new[] { "Id", "CityId", "Name", "PostalCode", "TypeOfSettlement" },
                values: new object[,]
                {
                    { 201, 101, "Sloboština", "10010", (byte)2 },
                    { 202, 101, "Maksimir", "10000", (byte)2 },
                    { 204, 101, "Črnomerec", "10000", (byte)2 },
                    { 203, 102, "Velika Gorica centar", "10408", (byte)2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 102);
        }
    }
}
