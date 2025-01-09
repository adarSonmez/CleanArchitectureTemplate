﻿using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.Domain.Entities.Marketing;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Products.Queries.GetAllProducts;

/// <summary>
/// Handles the <see cref="GetAllProductsQueryRequest"/>.
/// </summary>
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, PagedResponse<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductsQueryHandler(IMapper mapper, IProductReadRepository productReadRepository)
    {
        _mapper = mapper;
        _productReadRepository = productReadRepository;
    }

    public async Task<PagedResponse<ProductDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<ProductDto>();
        try
        {
            var includes = new List<Expression<Func<Product, object>>>();

            if (request.IncludeCategories)
            {
                includes.Add(p => p.Categories);
            }
            if (request.IncludeOrders)
            {
                includes.Add(p => p.Orders);
            }
            if (request.IncludeProductImageFiles)
            {
                includes.Add(p => p.ProductImageFiles);
            }

            var products = await _productReadRepository.GetAllPaginatedAsync(
                pagination: request.Pagination,
                include: includes);

            response.SetData(_mapper.Map<IEnumerable<ProductDto>>(products), request.Pagination?.Page, request.Pagination?.Size);
        }
        catch (Exception ex)
        {
            response.AddError("PRD602774", ex.Message);
        }

        return response;
    }
}