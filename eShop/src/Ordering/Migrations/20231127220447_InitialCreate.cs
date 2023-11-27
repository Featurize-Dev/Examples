using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order_Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AggregateName = table.Column<string>(type: "text", nullable: false),
                    AggregateId = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    EventName = table.Column<string>(type: "text", nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Events", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Events_AggregateId",
                table: "Order_Events",
                column: "AggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Events_AggregateId_Version",
                table: "Order_Events",
                columns: new[] { "AggregateId", "Version" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Events");
        }
    }
}
