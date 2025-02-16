using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Entities.Abstract;

namespace Entities;

[Table("tbl_CareTeams", Schema = "database_first")]
public class CareTeam : EntityBase
{
    [Key]
    [Column("iCareTeamID")]
    [DataMember]
    public override int Id { get; set; }
    
    [Column("vchName")]
    [DataMember]
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(150, ErrorMessage = "Name exceeds {1} characters")]
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
