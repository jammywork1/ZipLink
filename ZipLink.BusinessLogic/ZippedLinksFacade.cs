using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack;
using ZipLink.DataAccessLayer;
using ZipLink.DataAccessLayer.Entities;

namespace ZipLink.BusinessLogic
{
    public class ZippedLinkFacade
    {
        private readonly IZippedLinksRepository _zippedLinksRepository;
        private readonly IUrlGenerator _urlGenerator;

        public ZippedLinkFacade(IZippedLinksRepository zippedLinksRepository, IUrlGenerator urlGenerator)
        {
            if (zippedLinksRepository == null) throw new ArgumentNullException(nameof(zippedLinksRepository));
            if (urlGenerator == null) throw new ArgumentNullException(nameof(urlGenerator));
            _zippedLinksRepository = zippedLinksRepository;
            _urlGenerator = urlGenerator;
        }

        public async Task<ZippedLinkModel> ZipLink(ZipLinkDTO dto)
        {
            new ZipLinkValidator(dto).Validate();
            var newEntity = new ZippedLinkEntity()
            {
                Created = DateTime.Now,
                Followed = 0,
                OriginalLink = dto.Link,
                Hash = _urlGenerator.Generate()
            };
            await _zippedLinksRepository.CreateLink(newEntity);
            return newEntity.ConvertTo<ZippedLinkModel>();
        }

        public async Task<IEnumerable<ZippedLinkModel>> GetAllZippedLinks()
        {
            var entities = await _zippedLinksRepository.GetLinks(1000, 0);
            return entities.Select(i => i.ConvertTo<ZippedLinkModel>());
        }

        public async Task<ZippedLinkModel> GetByHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(hash));
            var entity = await _zippedLinksRepository.GetByHash(hash);
            return entity.ConvertTo<ZippedLinkModel>();
        }

        public async Task IncrementLinkFollowByHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(hash));
            await _zippedLinksRepository.IncrementLinkFollowByHash(hash);
        }
    }

    public interface IUrlGenerator
    {
        string Generate();
    }

    public class GuidUrlGenerator : IUrlGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString("D").Replace("-", "");
        }
    }

    public class ZipLinkDTO
    {
        public string Link { get; set; }
    }

    public class ZipLinkValidator 
    {
        private ZipLinkDTO _dto;

        public ZipLinkValidator(ZipLinkDTO dto)
        {
            DTO = dto;
        }

        public ZipLinkDTO DTO
        {
            get { return _dto; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _dto = value;
            }
        }

        public void Validate()
        {
            if (!Uri.IsWellFormedUriString(DTO.Link, UriKind.Absolute)) 
                throw new ValidationException("This is not a link!");
        }
    }

    public class ZippedLinkModel
    {
        public string Hash { get;set; }
        public string OriginalLink { get;set; }
        public DateTime Created { get;set; }
        public int Followed { get;set; }
    }
}