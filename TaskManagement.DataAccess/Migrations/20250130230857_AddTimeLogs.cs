using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timelog_AspNetUsers_UserId",
                table: "Timelog");

            migrationBuilder.DropForeignKey(
                name: "FK_Timelog_Tasks_TaskId",
                table: "Timelog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Timelog",
                table: "Timelog");

            migrationBuilder.RenameTable(
                name: "Timelog",
                newName: "Timelogs");

            migrationBuilder.RenameIndex(
                name: "IX_Timelog_UserId",
                table: "Timelogs",
                newName: "IX_Timelogs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Timelog_TaskId",
                table: "Timelogs",
                newName: "IX_Timelogs_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timelogs",
                table: "Timelogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timelogs_AspNetUsers_UserId",
                table: "Timelogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timelogs_Tasks_TaskId",
                table: "Timelogs",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timelogs_AspNetUsers_UserId",
                table: "Timelogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Timelogs_Tasks_TaskId",
                table: "Timelogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Timelogs",
                table: "Timelogs");

            migrationBuilder.RenameTable(
                name: "Timelogs",
                newName: "Timelog");

            migrationBuilder.RenameIndex(
                name: "IX_Timelogs_UserId",
                table: "Timelog",
                newName: "IX_Timelog_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Timelogs_TaskId",
                table: "Timelog",
                newName: "IX_Timelog_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timelog",
                table: "Timelog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timelog_AspNetUsers_UserId",
                table: "Timelog",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timelog_Tasks_TaskId",
                table: "Timelog",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
