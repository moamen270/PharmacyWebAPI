using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using PharmacyWebAPI.DataAccess.Repository.IRepository;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.ViewModels;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO.Pipelines;
using AutoMapper;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _unitOfWork.Product.GetAsync(id);

            if (obj is null)
                return NotFound(new { success = false, message = "Not Found" });
            ProductDetailDto productDetailDto = _mapper.Map<ProductDetailDto>(obj);
            /* new ProductDetailDto()
        {
            Id = obj.Id,
            ArabicName = obj.ArabicName,
            EnglishName = obj.EnglishName,
            type = obj.type,
            Contraindications = obj.Contraindications,
            Description = obj.Description,
            ExpDate = obj.ExpDate,
            Stock = obj.Stock,
            ListPrice = obj.ListPrice,
            Price = obj.Price,
            ImgURL = obj.ImgURL,
            StorageId = obj.StorageId,
            BrandId = obj.BrandId,
            CategoryId = obj.CategoryId,
        };*/

            return Ok(new { Product = productDetailDto });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Product> Products = await _unitOfWork.Product.GetAllAsync(c => c.Category, z => z.Brand, s => s.Storage, r => r.Rates, com => com.Comments);
            IEnumerable<ProductDetailDto> ProductsDto = _mapper.Map<IEnumerable<ProductDetailDto>>(Products);
            return Ok(new { Products = ProductsDto });
        }

        //POST
        [HttpPost]
        [Route("QuickCreate")]
        public async Task<IActionResult> QuickCreate()
        {
            var model = new Product
            {
                CategoryId = (await _unitOfWork.Category.GetFirstOrDefaultAsync()).Id,
                BrandId = (await _unitOfWork.Brand.GetFirstOrDefaultAsync()).Id
            };
            await _unitOfWork.Product.AddAsync(model);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Product Created Successfully", model });
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ProductFormDto ProductFormDto = new ProductFormDto
            {
                Categories = await _unitOfWork.Category.GetAllAsync(),
                Brands = await _unitOfWork.Brand.GetAllAsync()
            };
            return Ok(new { Product = ProductFormDto });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ProductFormDto obj)
        {
            if (!ModelState.IsValid)
            {
                obj.Categories = await _unitOfWork.Category.GetAllAsync();
                obj.Brands = await _unitOfWork.Brand.GetAllAsync();
                return BadRequest(new { success = false, message = "Validation Error", Product = obj });
            }

            Product product = _mapper.Map<Product>(obj);
            /*new()
        {
            ArabicName = obj.ArabicName,
            EnglishName = obj.EnglishName,
            type = obj.type,
            Contraindications = obj.Contraindications,
            Description = obj.Description,
            ExpDate = obj.ExpDate,
            Stock = obj.Stock,
            ListPrice = obj.ListPrice,
            Price = obj.Price,
            ImgURL = obj.ImgURL,
            StorageId = obj.StorageId,
            BrandId = obj.BrandId,
            CategoryId = obj.CategoryId,
        };*/

            await _unitOfWork.Product.AddAsync(product);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Product Created Successfully", Product = product });
        }

        //POST
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Product.GetAsync(id);
            if (obj == null)
            {
                return NotFound(new { success = false, message = "Not Found" });
            }
            _unitOfWork.Product.Delete(obj);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Product Deleted Successfully" });
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.Product.GetAsync(id);
            if (product == null)
                return NotFound(new { success = false, message = "Not Found" });

            ProductFormDto productDto = _mapper.Map<ProductFormDto>(product);
            productDto.Categories = await _unitOfWork.Category.GetAllAsync();
            productDto.Brands = await _unitOfWork.Brand.GetAllAsync();

            /*new ProductFormDto
        {
            ArabicName = product.ArabicName,
            EnglishName = product.EnglishName,
            type = product.type,
            Contraindications = product.Contraindications,
            Description = product.Description,
            ExpDate = product.ExpDate,
            Stock = product.Stock,
            ListPrice = product.ListPrice,
            Price = product.Price,
            ImgURL = product.ImgURL,
            StorageId = product.StorageId,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            Categories = await _unitOfWork.Category.GetAllAsync(),
            Brands = await _unitOfWork.Brand.GetAllAsync()
        };*/

            return Ok(new { Product = productDto });
        }

        //Put
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(ProductFormDto obj)
        {
            if (!ModelState.IsValid)
            {
                obj.Categories = await _unitOfWork.Category.GetAllAsync();
                obj.Brands = await _unitOfWork.Brand.GetAllAsync();
                return BadRequest(new { success = false, message = "Validation Error", Product = obj }); ;
            }

            Product product = _mapper.Map<Product>(obj);
            /*await _unitOfWork.Product.GetAsync(obj.Id);
        product.ArabicName = obj.ArabicName;
        product.EnglishName = obj.EnglishName;
        product.type = obj.type;
        product.Contraindications = obj.Contraindications;
        product.Description = obj.Description;
        product.ExpDate = obj.ExpDate;
        product.Stock = obj.Stock;
        product.ListPrice = obj.ListPrice;
        product.Price = obj.Price;
        product.ImgURL = obj.ImgURL;
        product.StorageId = obj.StorageId;
        product.BrandId = obj.BrandId;
        product.CategoryId = obj.CategoryId;*/

            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Product Updated Successfully", Product = product });
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
                return NotFound(new { success = false, message = "Not Found" });
            ProductDetailDto obj = _mapper.Map<ProductDetailDto>(product);
            /*new()
        {
            ArabicName = product.ArabicName,
            EnglishName = product.EnglishName,
            type = product.type,
            Contraindications = product.Contraindications,
            Description = product.Description,
            ExpDate = product.ExpDate,
            Stock = product.Stock,
            ListPrice = product.ListPrice,
            Price = product.Price,
            ImgURL = product.ImgURL,
            StorageId = product.StorageId,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
        };*/
            /*  double temp = 0;*/
            /* foreach (var item in RateList)
             {
                 temp += item.Rate;
             }
             viewModel.AvgRate = temp / RateList.Count();*/
            return Ok(new { Product = obj });
        }

        [HttpGet]
        [Route("GetByCategory/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var products = await _unitOfWork.Product.GetAllFilterAsync(x => x.CategoryId == id, c => c.Category, z => z.Brand, s => s.Storage, r => r.Rates, com => com.Comments);
            if (products.Count() == 0)
                return NotFound(new { success = false, message = "Not Found" });
            IEnumerable<ProductDetailDto> ProductsDto = _mapper.Map<IEnumerable<ProductDetailDto>>(products);
            return Ok(new { Products = ProductsDto });
        }

        [HttpGet]
        [Route("GetByBrand/{id}")]
        public async Task<IActionResult> GetByBrand(int id)
        {
            var products = await _unitOfWork.Product.GetAllFilterAsync(x => x.BrandId == id, c => c.Category, z => z.Brand, s => s.Storage, r => r.Rates, com => com.Comments);
            if (products.Count() == 0)
                return NotFound(new { success = false, message = "Not Found" });
            IEnumerable<ProductDetailDto> ProductsDto = _mapper.Map<IEnumerable<ProductDetailDto>>(products);
            return Ok(new { Products = ProductsDto });
        }

        /*[HttpPost]
        public async Task<IActionResult> AddReview(ProductReviewVM viewModel)
        {
            Review review = new Review()
            {
                ProductId = viewModel.Id,
                Rate = viewModel.Rate,
                Comment = viewModel.Comment,
                UserId = (await _userManager.GetUserAsync(User)).Id
            };
            await _unitOfWork.Review.AddAsync(review);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Details), new { id = viewModel.Id }); ;
        }*/
    }
}