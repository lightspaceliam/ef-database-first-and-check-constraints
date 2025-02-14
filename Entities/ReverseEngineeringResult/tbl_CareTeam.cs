using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ReverseEngineeringResult;

[Table("tbl_CareTeams", Schema = "database_first")]
public partial class tbl_CareTeam
{
    [Key]
    public int iCareTeamID { get; set; }

    [StringLength(150)]
    public string vchName { get; set; } = null!;

    [InverseProperty("iCareTeam")]
    public virtual ICollection<tbl_Patient> tbl_Patients { get; set; } = new List<tbl_Patient>();
}
