using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SocialAnalyser.Services
{
  public class DatasetService: IDatasetServicecs
  {
    public DatasetService(
      IDatasetRepository companyRepository
      )
    {
      fCompanyRepository = companyRepository;
      fCompanyNoteRepository = companyNoteRepository;
      fRegionRepository = regionRepository;
    }

    public Task<Unit> CreateDatasetAsync(string dataset, CancellationToken cancellationToken)
    {

    }
  }
}
