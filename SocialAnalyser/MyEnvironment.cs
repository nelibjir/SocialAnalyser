using System;

namespace SocialAnalyser
{
  public class MyEnvironment
  {
    private const EnvironmentVariableTarget _Machine = EnvironmentVariableTarget.Machine;

    public static string DefaultDbConnectionString { get; set; }

    public const string _DbConnectionString = "SHIPVIO_DB_CONNECTION_STRING";

    public static string DbConnectionString {
      get {
        return Environment.GetEnvironmentVariable(_DbConnectionString, _Machine)
        ?? DefaultDbConnectionString;
      }
    }

  }
}
