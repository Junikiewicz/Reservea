using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Dtos.Responses;
using Reservea.Microservices.CMS.Interfaces.Services;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly ICmsUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public PhotosService(ICmsUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            var acc = new Account
            {
                Cloud = configuration["Cloudinary:CloudName"],
                ApiKey = configuration["Cloudinary:ApiKey"],
                ApiSecret = configuration["Cloudinary:ApiSecret"]
            };
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<IEnumerable<PhotoResponse>> GetPhotos(CancellationToken cancellationToken)
        {
            var photos = await _unitOfWork.PhotosRepository.GetAllAsync<PhotoResponse>(cancellationToken);

            return photos;
        }

        public async Task<PhotoResponse> AddPhoto(AddPhotoRequest request, CancellationToken cancellationToken)
        {
            var file = request.File;
            var uploudResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploudParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(700).Height(400).Crop("fill")
                    };
                    uploudResult = await _cloudinary.UploadAsync(uploudParams);
                }
            }
            var photo = _mapper.Map<Photo>(request);
            photo.Url = uploudResult.SecureUrl.ToString();
            photo.PublicId = uploudResult.PublicId;

            _unitOfWork.PhotosRepository.Add(photo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PhotoResponse>(photo);
        }

        public async Task<PhotoResponse> GetPhoto(int id, CancellationToken cancellationToken)
        {
            var photo = await _unitOfWork.PhotosRepository.GetSingleAsync(x => x.Id == id, cancellationToken);

            return _mapper.Map<PhotoResponse>(photo);
        }

        public async Task DeletePhoto(int id, CancellationToken cancellationToken)
        {
            var photo = await _unitOfWork.PhotosRepository.GetSingleAsync(x => x.Id == id, cancellationToken);

            await _cloudinary.DestroyAsync(new DeletionParams(photo.PublicId));

            _unitOfWork.PhotosRepository.Remove(photo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
