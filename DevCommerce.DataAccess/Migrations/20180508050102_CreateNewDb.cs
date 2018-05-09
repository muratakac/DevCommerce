using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DevCommerce.DataAccess.Migrations
{
    public partial class CreateNewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
