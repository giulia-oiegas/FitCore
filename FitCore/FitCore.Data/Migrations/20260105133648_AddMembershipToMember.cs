using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipToMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipStartDate",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipTypeId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MembershipTypeId",
                table: "Members",
                column: "MembershipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipTypes_MembershipTypeId",
                table: "Members",
                column: "MembershipTypeId",
                principalTable: "MembershipTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipTypes_MembershipTypeId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MembershipTypeId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipStartDate",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipTypeId",
                table: "Members");
        }
    }
}
