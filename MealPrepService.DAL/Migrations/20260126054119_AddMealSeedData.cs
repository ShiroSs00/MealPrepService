using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPrepService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMealSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9556));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9560));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9564));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9567));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9570));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9573));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9576));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9579));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9582));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9586));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9589));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9607));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9611));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9613));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9616));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9619));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9622));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9625));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9627));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 41, 18, 831, DateTimeKind.Utc).AddTicks(9630));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1686));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1691));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1695));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1698));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1702));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1709));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1712));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1716));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1719));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1723));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1728));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1731));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1742));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1745));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1748));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1751));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1754));
        }
    }
}
