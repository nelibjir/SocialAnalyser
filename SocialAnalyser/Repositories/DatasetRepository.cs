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
      Dataset dataset = new Dataset() { Name = name };
      await DbSet.AddAsync(dataset, cancellationToken);
      await SaveAllAsync(cancellationToken);
      return dataset.Id;
    }
  }
}
