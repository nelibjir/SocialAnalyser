using System;
using System.Configuration;

namespace SocialAnalyser
{
  public class MyEnvironment
  {
    private const EnvironmentVariableTarget _Machine = EnvironmentVariableTarget.Machine;

    public static string DefaultDbConnectionString { get; set; }

    public const string _DbConnectionString = "SHIPVIO_DB_CONNECTION_STRING";
    //public const string _DbConnectionString = "LOGGER_CONNECTION_STRING";

    public static readonly string DbConnectionString;

    static MyEnvironment()
    {
      DbConnectionString = Environment.GetEnvironmentVariable(_DbConnectionString, _Machine)
        ?? DefaultDbConnectionString;
    }
  }
}
