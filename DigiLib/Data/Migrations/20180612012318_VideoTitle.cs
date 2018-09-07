using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DigiLib.Data.Migrations
{
    public partial class VideoTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoName",
                table: "Video");

            migrationBuilder.AddColumn<string>(
                name: "VideoDescription",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoTitle",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoDescription",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "VideoTitle",
                table: "Video");

            migrationBuilder.AddColumn<string>(
                name: "VideoName",
                table: "Video",
                nullable: true);
        }
    }
}
