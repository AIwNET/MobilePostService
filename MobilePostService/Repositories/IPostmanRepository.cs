using MobilePostService.Models;
using System.Linq;

namespace MobilePostService.Repositories
{
    interface IPostmanRepository : IRepository<Postman>
    {
        Postman GetPostmanById(int id);
        Postman GetPostmanByUserId(int id);
        bool IsConfirmed(int id);
    }
}
