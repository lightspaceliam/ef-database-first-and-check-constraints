using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Entities.Abstract;

[DataContract]
public abstract class EntityBase : IEntity
{
	[Key]
	[DataMember]
	public virtual int Id { get; set; }
}