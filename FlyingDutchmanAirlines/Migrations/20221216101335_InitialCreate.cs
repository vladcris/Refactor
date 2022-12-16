using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyingDutchmanAirlines.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    IATA = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Origin = table.Column<int>(type: "INTEGER", nullable: false),
                    AirportOriginAirportId = table.Column<int>(type: "INTEGER", nullable: true),
                    Destination = table.Column<int>(type: "INTEGER", nullable: false),
                    AirportDestinationAirportId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightNumber);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_AirportDestinationAirportId",
                        column: x => x.AirportDestinationAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId");
                    table.ForeignKey(
                        name: "FK_Flights_Airports_AirportOriginAirportId",
                        column: x => x.AirportOriginAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId");
                });

            migrationBuilder.CreateTable(
                name: "Bokings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNumberId = table.Column<int>(type: "INTEGER", nullable: false),
                    FlightNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bokings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bokings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bokings_Flights_FlightNumber",
                        column: x => x.FlightNumber,
                        principalTable: "Flights",
                        principalColumn: "FlightNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bokings_CustomerId",
                table: "Bokings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bokings_FlightNumber",
                table: "Bokings",
                column: "FlightNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirportDestinationAirportId",
                table: "Flights",
                column: "AirportDestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirportOriginAirportId",
                table: "Flights",
                column: "AirportOriginAirportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bokings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
