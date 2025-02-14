using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ReverseEngineeringResult;

[Table("tbl_Patients", Schema = "database_first")]
public partial class tbl_Patient
{
    [Key]
    public int iPatientID { get; set; }

    [StringLength(150)]
    public string vchFirstName { get; set; } = null!;

    [StringLength(150)]
    public string vchLastName { get; set; } = null!;

    public int iCareTeamID { get; set; }

    [ForeignKey("iCareTeamID")]
    [InverseProperty("tbl_Patients")]
    public virtual tbl_CareTeam iCareTeam { get; set; } = null!;
}
