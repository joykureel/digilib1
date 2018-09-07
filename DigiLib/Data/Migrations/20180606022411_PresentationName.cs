using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DigiLib.Data.Migrations
{
    public partial class PresentationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoName",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Url",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "Url",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentationName",
                table: "Presentation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoName",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Url");

            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "Url");

            migrationBuilder.DropColumn(
                name: "PresentationName",
                table: "Presentation");
        }
    }
}
