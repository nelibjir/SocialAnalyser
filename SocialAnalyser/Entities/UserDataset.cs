using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialAnalyser.Entities
{
  [Table("users_datasets")]
  public class UserDataset: IBaseEntity
  {
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("user_id")]
    public string UserId { get; set; }

    [Required]
    [Column("dataset_id")]
    public int DatasetId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserDatasets")]
    public virtual User User { get; set; }

    [ForeignKey("DatasetId")]
    [InverseProperty("UserDatasets")]
    public Dataset Dataset { get; set; }
  }
}
