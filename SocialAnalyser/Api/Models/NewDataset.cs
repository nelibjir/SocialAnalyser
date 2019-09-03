using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SocialAnalyser.Api.Models
{
  public class NewDataset
  {
    /// <summary>
    /// Name of the dataset
    /// </summary>
    [Required]
    [Range(1, 450)]
    public string Name { get; set; }
  }
}
