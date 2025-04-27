using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NewZealand.Migrations
{
    /// <inheritdoc />
    public partial class Seedingmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("30547172-2b6a-4d45-92c6-d4f57a0302db"), "Medium" },
                    { new Guid("981c0b18-c325-41c6-b52d-af49c3abfdae"), "Easy" },
                    { new Guid("d696e25f-643e-4f9a-891e-204170e41a32"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("28bb9c0c-6372-4acf-9247-06d65e3c59f6"), "Harayana", "Gurugram", "https://media.gettyimages.com/id/1224427501/photo/aerial-night-shot-of-buildings-with-homes-and-offices-with-more-skyscrapers-in-the-distance.jpg?s=612x612&w=gi&k=20&c=hJkyRqEHkK9NjoAwuNtKTHJPZ1LvvaRT5A0Ns_fP7JE=" },
                    { new Guid("551f264a-8f7d-47f3-b7d5-a49e33d08c53"), "UP", "Noida", "https://media.gettyimages.com/id/1224427501/photo/aerial-night-shot-of-buildings-with-homes-and-offices-with-more-skyscrapers-in-the-distance.jpg?s=612x612&w=gi&k=20&c=hJkyRqEHkK9NjoAwuNtKTHJPZ1LvvaRT5A0Ns_fP7JE=" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("30547172-2b6a-4d45-92c6-d4f57a0302db"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("981c0b18-c325-41c6-b52d-af49c3abfdae"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d696e25f-643e-4f9a-891e-204170e41a32"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("28bb9c0c-6372-4acf-9247-06d65e3c59f6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("551f264a-8f7d-47f3-b7d5-a49e33d08c53"));
        }
    }
}
