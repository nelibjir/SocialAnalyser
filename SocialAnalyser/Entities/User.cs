using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialAnalyser.Entities
{
  [Table("users")]
  public partial class User: IBaseEntity
  {
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("user_id")]
    public string UserId { get; set; }

    [InverseProperty("FriendUser")]
    public virtual ICollection<UserFriend> UserFriends { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserFriend> Users { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserDataset> UserDatasets { get; set; }
  }
}
