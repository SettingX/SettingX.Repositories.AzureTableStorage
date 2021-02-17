using SettingX.Core.Blob;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Blob
{
    public class RepositoryDataRepository : BlobDataRepository, IRepositoryDataRepository
    {
        public RepositoryDataRepository(IBlobStorage blobStorage, string container, string historyContainer) : base(blobStorage, container, historyContainer)
        {

        }
    }
}