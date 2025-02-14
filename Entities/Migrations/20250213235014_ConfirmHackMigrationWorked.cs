using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class ConfirmHackMigrationWorked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
             * Confirmation the previous migration: '20250213234805_HackMigrationForPatientAndCareTeam' worked.
             * Entity Framework can no longer detect required changes for the existing tables Patient & CareTeam.
             *
             * This strategy will be required each time we want to add additional tables.
             */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
