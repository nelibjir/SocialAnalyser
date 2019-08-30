using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialAnalyser.Entities
{
  public partial class Dataset: IBaseEntity
  {
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }

    [InverseProperty("Dataset")]
    public virtual ICollection<Dataset> Datasets { get; set; }
  }
}
