﻿using ETicaretApi.Application.Abstractions.Storage;
using ETicaretApi.Application.Repositories.FileRepositories;
using ETicaretApi.Application.Repositories.InvoceFileRepositories;
using ETicaretApi.Application.Repositories.ProductImageFileRepositories;
using ETicaretApi.Application.Repositories.ProductRepositories;
using ETicaretApi.Application.RequestParameters;
using ETicaretApi.Application.ViewModels.Product;
using ETicaretApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_TicaretApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        private readonly IStorageService _storageService;
        
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IStorageService storageService, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _storageService = storageService;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(
                p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdateDate,
                }).ToList();

            return Ok(new
            {
                products,
                totalCount
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VM_Create_Product productModel)
        {

            await _productWriteRepository.AddAsync(new()
            {
                Name = productModel.Name,
                Price = productModel.Price,
                Stock = productModel.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var datas = await _storageService.UploadAsync("resource\\product-images", Request.Form.Files);
            await _productImageFileWriteRepository
                .AddRangeAsync(
                    datas.Select(d => new ProductImageFile()
                    {
                        FileName = d.fileName,
                        Path = d.path,
                        Storage = _storageService.StorageName
                    }
                 ).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product productModel)
        {
            var product = await _productReadRepository.GetByIdAsync(productModel.Id);
            product.Name = productModel.Name;
            product.Price = productModel.Price;
            product.Stock = productModel.Stock;
            await _productWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isSucces = await _productWriteRepository.Remove(id);

            if (!isSucces)
                return StatusCode((int)HttpStatusCode.NotFound);

            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}