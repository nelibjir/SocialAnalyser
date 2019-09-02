using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialAnalyser.Entities
{
  [Table("users_friends")]
  public partial class UserFriend: IBaseEntity
  {
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("user_id")]
    public string UserId { get; set; }

    [Required]
    [Column("friend_user_id")]
    public string FriendUserId { get; set; }

    [Required]
    [Column("dataset_id")]
    public int DatasetId { get; set; }

    [ForeignKey("FriendUserId")]
    [InverseProperty("UserFriends")]
    public virtual User FriendUser { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual User User { get; set; }

    [ForeignKey("DatasetId")]
    [InverseProperty("UserFriends")]
    public Dataset Dataset { get; set; }
  }
}
