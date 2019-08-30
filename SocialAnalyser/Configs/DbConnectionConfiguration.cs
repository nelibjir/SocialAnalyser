using System;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Configs
{
  public class DbConnectionConfiguration: IDbConnectionConfiguration
  {
    public string ConnectionString => MyEnvironment.DbConnectionString;
  }
}
