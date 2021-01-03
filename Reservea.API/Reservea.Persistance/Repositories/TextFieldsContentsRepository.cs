using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    public class TextFieldsContentsRepository : GenericRepository<TextFieldContent>, ITextFieldsContentsRepository
    {
        public TextFieldsContentsRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
