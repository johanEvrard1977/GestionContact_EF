using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionContact.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AdresseId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdresseId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_CP",
                table: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_Ville",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "AdresseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NombreDeSeancesAuthorisee",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "AdresseId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId1",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NombreDeSeancesAuthorisee",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AdresseId",
                table: "Contacts",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ImageId1",
                table: "Contacts",
                column: "ImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Adresses_AdresseId",
                table: "Contacts",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Images_ImageId1",
                table: "Contacts",
                column: "ImageId1",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Adresses_AdresseId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Images_ImageId1",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_AdresseId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ImageId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "AdresseId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ImageId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "NombreDeSeancesAuthorisee",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "AdresseId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NombreDeSeancesAuthorisee",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdresseId",
                table: "AspNetUsers",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_CP",
                table: "Adresses",
                column: "CP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_Ville",
                table: "Adresses",
                column: "Ville",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AdresseId",
                table: "AspNetUsers",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
