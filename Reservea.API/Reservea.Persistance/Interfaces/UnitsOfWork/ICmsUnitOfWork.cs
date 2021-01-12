using Reservea.Persistance.Interfaces.Repositories;

namespace Reservea.Persistance.Interfaces.UnitsOfWork
{
    public interface ICmsUnitOfWork : IBasicUnitOfWork
    {
        ITextFieldsContentsRepository TextFieldsContentsRepository { get; }
        IPhotosRepository PhotosRepository { get;}
        IUserRatesRepository UserRatesRepository { get; }
    }
}
