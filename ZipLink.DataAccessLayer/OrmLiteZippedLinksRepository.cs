using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ZipLink.DataAccessLayer.Entities;

namespace ZipLink.DataAccessLayer
{
    public class OrmLiteZippedLinksRepository : IZippedLinksRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public OrmLiteZippedLinksRepository(IDbConnectionFactory dbFactory) {
            this._dbFactory = dbFactory;
        }
        
        public async Task<IEnumerable<ZippedLinkEntity>> GetLinks(int take, int skip)
        {
            if (take <= 0) throw new ArgumentOutOfRangeException(nameof(take));
            if (skip < 0) throw new ArgumentOutOfRangeException(nameof(skip));
            using (var db = _dbFactory.Open())
            {
                var sql = db.From<ZippedLinkEntity>().Select().Take(take).Skip(skip);
                var result = await db.SqlListAsync<ZippedLinkEntity>(sql);
                return result;
//                var result = await db.SelectAsync<ZippedLinkEntity>();
//                return result.Take(take).Skip(skip);
            }
        }

        public async Task CreateLink(ZippedLinkEntity entity)
        {
            using (var db = _dbFactory.Open())
            {
                await db.InsertAsync(entity);
            }           
        }

        public async Task<ZippedLinkEntity> GetByHash(string hash)
        {
            if (hash == null) throw new ArgumentNullException(nameof(hash));
            using (var db = _dbFactory.Open())
            {
                return await db.SingleAsync<ZippedLinkEntity>(i => i.Hash == hash);
            }
        }

        public void InitSchema()
        {
            using (var db = _dbFactory.Open())
            {
                db.CreateTableIfNotExists<ZippedLinkEntity>();
            }
        }

        public async Task IncrementLinkFollowByHash(string hash)
        {
            if (hash == null) throw new ArgumentNullException(nameof(hash));
            using (var db = _dbFactory.Open())
            {
                using (IDbTransaction dbTrans = db.OpenTransaction(IsolationLevel.Serializable))
                {
                    var entity = await db.SingleAsync<ZippedLinkEntity>(i => i.Hash == hash);
                    if (entity == null)
                        throw new EntityNotFoundException(); 
                    entity.Followed++;
                    await db.UpdateAsync(entity);
                    dbTrans.Commit();
                }
            }
        }

#if DEBUG
        public void ReInitSchema()
        {
            using (var db = _dbFactory.Open())
            {
                db.DropAndCreateTable<ZippedLinkEntity>();
            }
        }
#endif
    }

    public class EntityNotFoundException : Exception
    {
    }
}