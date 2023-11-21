using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Room1102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Room");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "HotelRoomService",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "Status_HRS",
                table: "HotelRoomService",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "HotelRoomService");

            migrationBuilder.DropColumn(
                name: "Status_HRS",
                table: "HotelRoomService");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Room",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Room",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
