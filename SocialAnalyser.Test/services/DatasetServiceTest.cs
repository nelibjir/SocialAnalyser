using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moq;
using NUnit.Framework;
using SocialAnalyser.Adapters;
using SocialAnalyser.Dtos;
using SocialAnalyser.Services;

namespace SocialAnalyser.Test.services
{
  [TestFixture]
  public class DatasetServiceTest: GenericTest<UserFriendDto[]>
  {

    [Test]
    public void AddNewUsersTest()
    {
      HashSet<string> uniqueUserIds = new HashSet<string> { "1", "6", "7", "9", "10", "11" };
      CancellationToken cancellationToken = new CancellationToken();

      Mock<IDatasetService> datasetServiceService = new Mock<IDatasetService>(MockBehavior.Strict);
      datasetServiceService.Setup(p => p.InsertNewUsersAsync(uniqueUserIds, cancellationToken).Result).Returns(uniqueUserIds.Count);

      HashSet<string> userIdNull = null;
      datasetServiceService.Setup(p => p.InsertNewUsersAsync(userIdNull, cancellationToken).Result).Returns(0);

      datasetServiceService.VerifyAll();
    }

    [Test]
    public void ParseDatasetTest()
    {
      IStringUtilAdapter stringUtilAdapter = new StringUtilAdapter();

      string dataset = "1 2" + System.Environment.NewLine + "3 4" + System.Environment.NewLine + "5 6" + System.Environment.NewLine;
      int size = stringUtilAdapter.GetUserFriendDtos(dataset).ToArray().Length;
      Assert.AreEqual(3, size);

      dataset = "1 2" + System.Environment.NewLine + "34" + System.Environment.NewLine + "5 6" + System.Environment.NewLine;
      TestBadRequestException(() => stringUtilAdapter.GetUserFriendDtos(dataset).ToArray());

      dataset = null;
      IEnumerable<UserFriendDto> userFreind = stringUtilAdapter.GetUserFriendDtos(dataset);
      Assert.IsNull(userFreind);
    }
  }
}
