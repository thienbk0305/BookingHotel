using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Room1102111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status_HRS",
                table: "HotelRoomService",
                newName: "Status");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "HotelRoomService",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "HotelRoomService",
                newName: "Status_HRS");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "HotelRoomService",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
