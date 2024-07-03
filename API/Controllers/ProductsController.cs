using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ProductsController(StoreContext context, IMapper mapper, ImageService imageService, ILogger<ProductsController> logger) : BaseApiController
{
    private readonly StoreContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ImageService _imageService = imageService;
    private readonly ILogger<ProductsController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult<PageList<Product>>> GetProducts([FromQuery] ProductParams productParams)
    {
        var query = _context.Products
            .Sort(productParams.OrderBy)
            .Search(productParams.SearchTerm)
            .Filter(productParams.Brands, productParams.Types)
            .AsQueryable();

        var products =
            await PageList<Product>.ToPageList(query, productParams.PageNumber, productParams.PageSize);

        Response.AddPaginationHeader(products.MetaData);

        return products;
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpGet("filters")]
    public async Task<IActionResult> GetFilters()
    {
        var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
        var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();

        return Ok(new { brands, types });
    }

    // [Authorize(Roles = "Admin")]
    // [HttpPost]
    // public async Task<ActionResult<Product>> CreateProduct([FromForm] CreateProductDto productDto)
    // {
    //     var product = _mapper.Map<Product>(productDto);

    //     if (productDto.File != null)
    //     {
    //         var imageResult = await _imageService.AddImageAsync(productDto.File);

    //         if (imageResult.Error != null) return BadRequest(new ProblemDetails
    //         {
    //             Title = imageResult.Error.Message
    //         });

    //         product.PictureUrl = imageResult.SecureUrl.ToString();
    //         product.PublicId = imageResult.PublicId;
    //     }

    //     _context.Products.Add(product);

    //     var result = await _context.SaveChangesAsync() > 0;

    //     if (result) return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);

    //     return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
    // }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromForm] CreateProductDto productDto)
    {
        try
        {
            // Log incoming request details
            _logger.LogInformation("CreateProduct called with Name: {Name}, Description: {Description}", productDto.Name, productDto.Description);

            var product = _mapper.Map<Product>(productDto);

            if (productDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(productDto.File);

                if (imageResult.Error != null)
                {
                    _logger.LogError("Image upload error: {Error}", imageResult.Error.Message);
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });
                }

                product.PictureUrl = imageResult.SecureUrl.ToString();
                product.PublicId = imageResult.PublicId;
            }

            _context.Products.Add(product);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                _logger.LogInformation("Product created successfully with ID: {Id}", product.Id);
                return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);
            }

            _logger.LogWarning("Problem creating new product");
            return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in CreateProduct: {Exception}", ex.Message);
            return BadRequest(new ProblemDetails { Title = "An error occurred while creating the product" });
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProduct([FromForm] UpdateProductDto productDto)
    {
        var product = await _context.Products.FindAsync(productDto.Id);

        if (product == null) return NotFound();

        _mapper.Map(productDto, product);

        _mapper.Map(productDto, product);

        if (productDto.File != null)
        {
            var imageUploadResult = await _imageService.AddImageAsync(productDto.File);

            if (imageUploadResult.Error != null)
                return BadRequest(new ProblemDetails { Title = imageUploadResult.Error.Message });

            if (!string.IsNullOrEmpty(product.PublicId))
                await _imageService.DeleteImageAsync(product.PublicId);

            product.PictureUrl = imageUploadResult.SecureUrl.ToString();
            product.PublicId = imageUploadResult.PublicId;
        }

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok(product);

        return BadRequest(new ProblemDetails { Title = "Problem updating product" });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return NotFound();

        if (!string.IsNullOrEmpty(product.PublicId))
            await _imageService.DeleteImageAsync(product.PublicId);

        _context.Products.Remove(product);

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok();

        return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
    }
}