using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp2017.Migrations
{
    public partial class TestD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AverageRating",
                table: "Ratings",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Ratings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Ratings",
                newName: "AverageRating");
        }
    }
}
