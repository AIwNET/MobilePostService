using MobilePostService.Models;
using System.Linq;

namespace MobilePostService.Repositories
{
    interface IParcelRepository : IRepository<Parcel>
    {
        IQueryable<Parcel> GetAllParcels();
    }
}
