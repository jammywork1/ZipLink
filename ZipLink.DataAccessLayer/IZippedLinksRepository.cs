using System.Collections.Generic;
using System.Threading.Tasks;
using ZipLink.DataAccessLayer.Entities;

namespace ZipLink.DataAccessLayer
{
    public interface IZippedLinksRepository
    {
        Task<IEnumerable<ZippedLinkEntity>> GetLinks(int take, int skip);
        Task CreateLink(ZippedLinkEntity entity);
        Task<ZippedLinkEntity> GetByHash(string hash);
        void InitSchema();
        Task IncrementLinkFollowByHash(string hash);
#if DEBUG
        void ReInitSchema();
#endif
    }
}