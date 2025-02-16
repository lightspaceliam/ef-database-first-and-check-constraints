using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Entities.Abstract;

namespace Entities;

public enum ObservationStatus
{
    Registered,
    Preliminary,
    Final,
    Amended
}

[Table("Observations", Schema = "code_first")]
public class Observation : EntityBase
{
    /// <summary>
    /// Constrained to: registered | preliminary | final | amended
    /// </summary>
    [DataMember]
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(150, ErrorMessage = "Name exceeds {1} characters")]
    public ObservationStatus Status { get; set; }
    
    [DataMember]
    [Required(ErrorMessage = "Value is required.")]
    public decimal Value { get; set; }
    
    [DataMember]
    [Required(ErrorMessage = "Unit is required.")]
    [StringLength(20, ErrorMessage = "Unit exceeds {1} characters")]
    public string Unit { get; set; } = null!;
    
    
    [DataMember]
    [Required(ErrorMessage = "Patient is required")]
    public int PatientId { get; set; }   
}
