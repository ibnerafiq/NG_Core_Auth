using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestMakerFree.Migrations
{
    public partial class EROleCLass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole<int>");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "50a07504-555f-4f60-b39b-3b927cad4de8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", "0988a129-4a60-46d5-8b7e-d6bc6f163fc7", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", "abaa2efe-d291-4124-a5ee-7f7561a6aa6a", "Moderator", "MODERATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1", "50a07504-555f-4f60-b39b-3b927cad4de8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2", "0988a129-4a60-46d5-8b7e-d6bc6f163fc7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "3", "abaa2efe-d291-4124-a5ee-7f7561a6aa6a" });

            migrationBuilder.CreateTable(
                name: "IdentityRole<int>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole<int>", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole<int>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "ee53fa43-d16c-4df2-8e8a-4114fa5dc75f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "IdentityRole<int>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 2, "cfb2d4a9-a53a-4d6e-90c5-ded47ac05b82", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "IdentityRole<int>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 3, "21394a9a-76fd-49db-9799-a17c24a9d4ba", "Moderator", "MODERATOR" });
        }
    }
}
