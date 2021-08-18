namespace Oversteer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RenameFeedbackRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Raiting",
                table: "Feedbacks",
                newName: "Rating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Feedbacks",
                newName: "Raiting");
        }
    }
}
