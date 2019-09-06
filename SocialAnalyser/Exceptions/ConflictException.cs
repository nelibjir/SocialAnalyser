
namespace SocialAnalyser.Exceptions
{
  public class ConflictException: ApiException
  {
    public ConflictException(string attribute)
      : base(System.Net.HttpStatusCode.Conflict, $"Conflict or SQL error. Attribue {attribute} has to be unique.") { }
  }
}
