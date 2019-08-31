using System.ComponentModel.DataAnnotations;

namespace SocialAnalyser.Api.Models
{
  public class NewDataset
  {
    /// <summary>
    /// Set of data about friendship 
    /// </summary>
    [Required]
    [StringLength(int.MaxValue, MinimumLength = 1)]
    public string Dataset { get; set; }

    /// <summary>
    /// Name of the dataset
    /// </summary>
    [Required]
    public string Name { get; set; }
  }
}
