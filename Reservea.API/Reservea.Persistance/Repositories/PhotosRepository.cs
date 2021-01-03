using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    public class PhotosRepository : GenericRepository<Photo>, IPhotosRepository
    {
        public PhotosRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
