using System.Threading;
using System.Threading.Tasks;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public class DatasetRepository: GenericRepository<Dataset>, IDatasetRepository
  {
    public DatasetRepository(DataContext dataContext)
      : base(dataContext) { }

    public async Task<int> InsertDatasetAsync(string name, CancellationToken cancellationToken)
    {
     return (await DbSet.AddAsync(new Dataset() { Name = name }, cancellationToken)).Entity.Id;
    }
  }
}
