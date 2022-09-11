using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetListBrand;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery : IRequest<GetByIdBrandDto>
    {
        public int Id { get; set; }
        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, GetByIdBrandDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;
            #region Ctor
            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="brandRepository"></param>
            /// <param name="mapper"></param>
            public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }
            #endregion



            public async Task<GetByIdBrandDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
                
                Brand? brand = await _brandRepository.GetAsync(b => b.Id == request.Id);
                await _brandBusinessRules.BrandShouldExistWhenRequested(request.Id);
                GetByIdBrandDto mappedGetByIdBrandDto = _mapper.Map<GetByIdBrandDto>(brand);
                return mappedGetByIdBrandDto;
            }
        }
    }
}
