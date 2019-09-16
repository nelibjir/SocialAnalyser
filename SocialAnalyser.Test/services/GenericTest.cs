using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SocialAnalyser.Exceptions;

namespace SocialAnalyser.Test.services
{
  public class GenericTest<T> where T : class
  {
    public void TestException(Func<T> func)
    {
      try
      {
        T dtos = func();
        Assert.Fail("No exception on wrong format of dataset was thrown");
      }
      catch (BadRequestException) { }
      catch (AssertionException) { }
      catch (Exception ex)
      {
        Assert.Fail("Bad exception was thrown for bad dataset format input", ex);
      }

    }
  }
}
