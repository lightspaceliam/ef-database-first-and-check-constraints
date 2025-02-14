using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class HackMigrationForPatientAndCareTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* The strategy is to slowly add existing tables into Entity Framework !!!WITHOUT!!!:
             *      1. Including all tables
             *      2. Making changes to the existing table/s we gradually include as this could break other or unknown dependencies such as:
             *          - Stored Procedures
             *          - Views
             *          - Other
             *
             * However, this can add risk if we continue to make changes to the schema, specifically to the included tables, outside of Entity Framework.\
             * An example includes:
             *      1. We have added Patient and CareTeam tables, and we are expecting the same schema
             *      2. Someone makes a change directly on the database and changes:
             *          - a colum name
             *          - the length an NVARCHAR column can take
             *          - data type
             *          - if data type is string and updates the collation
             *
             * We need to add an EF hack migration to get this schema calculation into the {ContextName}ModelSnapshot and then comment out the migration commands
             * but only in the migration. !!!Never update the ModelSnapshot!!!.
             *
             * Ensure the Down migrations commands are disabled as well to prevent destructive results to the existing schema.
             */  
            
            #region Hack Migration to add to ModelSnapshot

            // migrationBuilder.EnsureSchema(
            //     name: "database_first");
            //
            // migrationBuilder.CreateTable(
            //     name: "tbl_CareTeams",
            //     schema: "database_first",
            //     columns: table => new
            //     {
            //         iCareTeamID = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         vchName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_tbl_CareTeams", x => x.iCareTeamID);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "tbl_Patients",
            //     schema: "database_first",
            //     columns: table => new
            //     {
            //         iPatientID = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         vchFirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //         vchLastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //         iCareTeamID = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_tbl_Patients", x => x.iPatientID);
            //         table.ForeignKey(
            //             name: "FK_tbl_Patients_tbl_CareTeams_iCareTeamID",
            //             column: x => x.iCareTeamID,
            //             principalSchema: "database_first",
            //             principalTable: "tbl_CareTeams",
            //             principalColumn: "iCareTeamID",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_tbl_Patients_iCareTeamID",
            //     schema: "database_first",
            //     table: "tbl_Patients",
            //     column: "iCareTeamID");

            #endregion
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            #region Hack Migration to add to ModelSnapshot
            
            // migrationBuilder.DropTable(
            //     name: "tbl_Patients",
            //     schema: "database_first");
            //
            // migrationBuilder.DropTable(
            //     name: "tbl_CareTeams",
            //     schema: "database_first");
            
            #endregion
        }
    }
}
