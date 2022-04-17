using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dvdrentalweb.Migrations
{
    public partial class AddTablesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CastMembers",
                table: "CastMembers");

            migrationBuilder.RenameColumn(
                name: "LoanNumberType",
                table: "Loans",
                newName: "LoanTypeNumber");

            migrationBuilder.AlterColumn<int>(
                name: "DVDNumber",
                table: "CastMembers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CastMemberId",
                table: "CastMembers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CastMembers",
                table: "CastMembers",
                column: "CastMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CastMembers",
                table: "CastMembers");

            migrationBuilder.DropColumn(
                name: "CastMemberId",
                table: "CastMembers");

            migrationBuilder.RenameColumn(
                name: "LoanTypeNumber",
                table: "Loans",
                newName: "LoanNumberType");

            migrationBuilder.AlterColumn<int>(
                name: "DVDNumber",
                table: "CastMembers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CastMembers",
                table: "CastMembers",
                column: "DVDNumber");
        }
    }
}
