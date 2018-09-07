using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DigiLib.Data.Migrations
{
    public partial class UrlName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Url");

            migrationBuilder.AddColumn<string>(
                name: "UrlDescription",
                table: "Url",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlDescription",
                table: "Url");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Url",
                nullable: true);
        }
    }
}
