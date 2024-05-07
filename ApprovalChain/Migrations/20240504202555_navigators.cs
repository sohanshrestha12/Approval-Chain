using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApprovalChain.Migrations
{
    /// <inheritdoc />
    public partial class navigators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_documentApprovals_DocumentId",
                table: "documentApprovals");

            migrationBuilder.CreateIndex(
                name: "IX_documentApprovals_DocumentId",
                table: "documentApprovals",
                column: "DocumentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_documentApprovals_DocumentId",
                table: "documentApprovals");

            migrationBuilder.CreateIndex(
                name: "IX_documentApprovals_DocumentId",
                table: "documentApprovals",
                column: "DocumentId");
        }
    }
}
