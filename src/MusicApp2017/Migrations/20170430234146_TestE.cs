using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp2017.Migrations
{
    public partial class TestE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Ratings");

            migrationBuilder.AddColumn<decimal>(
                name: "RatingValue",
                table: "Ratings",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingValue",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
