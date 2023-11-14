using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class EditRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "RoomMax",
                table: "Room",
                newName: "RoomSize");

            migrationBuilder.AddColumn<byte>(
                name: "RoomHuman",
                table: "Room",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "RoomType",
                table: "Room",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "ServiceCodeByUserId",
                table: "Room",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_ServiceCodeByUserId",
                table: "Room",
                column: "ServiceCodeByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Service_ServiceCodeByUserId",
                table: "Room",
                column: "ServiceCodeByUserId",
                principalTable: "Service",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Service_ServiceCodeByUserId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_ServiceCodeByUserId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomHuman",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "ServiceCodeByUserId",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "RoomSize",
                table: "Room",
                newName: "RoomMax");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Room",
                type: "int",
                nullable: true);
        }
    }
}
