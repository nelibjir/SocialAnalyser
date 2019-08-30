using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Middlewares
{
  public class DbTransactionMiddleware
  {
    private readonly RequestDelegate fNext;

    public DbTransactionMiddleware(RequestDelegate next)
    {
      fNext = next;
    }

    public async Task Invoke(HttpContext context, DataContext dataContext)
    {
      IDbContextTransaction transaction = await dataContext.Database.BeginTransactionAsync();
      try
      {
        await fNext(context);
        await dataContext.SaveChangesAsync();
        transaction.Commit();
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }
    }
  }
}
