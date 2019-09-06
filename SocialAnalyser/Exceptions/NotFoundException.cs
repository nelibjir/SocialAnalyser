namespace SocialAnalyser.Exceptions
{
  public class NotFoundException: ApiException
  {
    public NotFoundException(string name)
      : base(System.Net.HttpStatusCode.NotFound, $"Resource {name} was not found on server.") { }
  }
}
