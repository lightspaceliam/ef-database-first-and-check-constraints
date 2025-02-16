using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Entities.Abstract;

namespace Entities;

[Table("tbl_Patients", Schema = "database_first")]
public class Patient : EntityBase
{
    [Key]
    [Column("iPatientID")]
    public override int Id { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    
    [Column("vchFirstName")]
    [DataMember]
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(150, ErrorMessage = "First name exceeds {1} characters")]
    public string FirstName { get; set; } = null!;

    [Column("vchLastName")]
    [DataMember]
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(150, ErrorMessage = "Last name exceeds {1} characters")]
    public string LastName { get; set; } = null!;
    
    [Column("iCareTeamID")]
    [DataMember]
    [Required(ErrorMessage = "Care team is required")]
    public int CareTeamId { get; set; }
    
    public CareTeam CareTeam { get; set; } = null!;
}
