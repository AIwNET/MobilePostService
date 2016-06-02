using System.Data.Entity;
using System.Linq;

namespace MobilePostService.Models
{
    public class MobilePostServiceContext : DbContext
    {
        public MobilePostServiceContext()
            : base("MobilePostService.mdf")
        {
        }

        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Postman> Postmen { get; set; }
    }
}