using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Repositories;

namespace Reservea.Persistance.UnitsOfWork
{
    public class CmsUnitOfWork : BasicUnitOfWork, ICmsUnitOfWork
    {
        public ITextFieldsContentsRepository TextFieldsContentsRepository
        {
            get
            {
                if (_textFieldsContentsRepository is null)
                {
                    _textFieldsContentsRepository = new TextFieldsContentsRepository(_context, _mapper);
                }

                return _textFieldsContentsRepository;
            }
        }

        private ITextFieldsContentsRepository _textFieldsContentsRepository;

        public IPhotosRepository PhotosRepository
        {
            get
            {
                if (_photosRepository is null)
                {
                    _photosRepository = new PhotosRepository(_context, _mapper);
                }

                return _photosRepository;
            }
        }

        private IPhotosRepository _photosRepository;

        public CmsUnitOfWork(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
