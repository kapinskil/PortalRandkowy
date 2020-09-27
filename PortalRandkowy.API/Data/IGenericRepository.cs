using System.Threading.Tasks;

namespace PortalRandkowy.API.Data
{
    public interface IGenericRepository
    {
         void Add<t>(t entity) where t: class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
    }
}