using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UWSR.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WSREFs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minus = table.Column<int>(type: "int", nullable: false),
                    Plus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WSREFs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WSREFComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WSREFId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WSREFComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WSREFComments_WSREFs_WSREFId",
                        column: x => x.WSREFId,
                        principalTable: "WSREFs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WSREFComments_WSREFId",
                table: "WSREFComments",
                column: "WSREFId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WSREFComments");

            migrationBuilder.DropTable(
                name: "WSREFs");
        }
    }
}
