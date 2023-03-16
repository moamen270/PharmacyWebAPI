using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWebAPI.DataAccess.Repository.IRepository;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.ViewModels;
using System.Diagnostics;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public ProductController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _unitOfWork.Product.GetAsync(id);
            if (obj is null)
                return BadRequest("Not Found");
            return Ok(obj);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Product> Products = await _unitOfWork.Product.GetAllAsync();
            return Ok(Products);
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
            ProductFormDto viewModel = new ProductFormDto
            {
                Categories = await _unitOfWork.Category.GetAllAsync(),
                Brands = await _unitOfWork.Brand.GetAllAsync()
            };
            return Ok(viewModel);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ProductFormDto viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = await _unitOfWork.Category.GetAllAsync();
                viewModel.Brands = await _unitOfWork.Brand.GetAllAsync();
                return BadRequest(viewModel);
            }

            Product product = new()
            {
                EnglishName = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ListPrice = viewModel.ListPrice,
                BrandId = viewModel.BrandId,
                CategoryId = viewModel.CategoryId,
                ImgURL = viewModel.ImgURL
            };

            await _unitOfWork.Product.AddAsync(product);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Product Created Successfully", product });
        }

        //POST
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Product.GetAsync(id);
            if (obj == null)
            {
                return BadRequest(new { success = true, message = "Faild" });
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
                return NotFound();

            ProductFormDto viewModel = new ProductFormDto
            {
                Id = product.Id,
                Name = product.EnglishName,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ListPrice = product.ListPrice,
                Description = product.Description,
                ImgURL = product.ImgURL,
                Categories = await _unitOfWork.Category.GetAllAsync(),
                Brands = await _unitOfWork.Brand.GetAllAsync()
            };

            return Ok(viewModel);
        }

        //POST
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(ProductFormDto viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = await _unitOfWork.Category.GetAllAsync();
                viewModel.Brands = await _unitOfWork.Brand.GetAllAsync();
                return BadRequest(viewModel); ;
            }

            Product product = await _unitOfWork.Product.GetAsync(viewModel.Id);
            product.Price = viewModel.Price;
            product.ListPrice = viewModel.ListPrice;
            product.EnglishName = viewModel.Name;
            product.BrandId = viewModel.BrandId;
            product.CategoryId = viewModel.CategoryId;
            product.Description = viewModel.Description;
            product.ImgURL = viewModel.ImgURL;
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            return Ok(product);
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
                return BadRequest("Not Found");
            ProductDetailDto viewModel = new()
            {
                Id = product.Id,
                Name = product.EnglishName,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ListPrice = product.ListPrice,
                Description = product.Description,
                ImgURL = product.ImgURL,
                Category = product.Category,
                Brand = product.Brand,
            };
            /*  double temp = 0;*/
            /* foreach (var item in RateList)
             {
                 temp += item.Rate;
             }
             viewModel.AvgRate = temp / RateList.Count();*/
            return Ok(viewModel);
        }

        [HttpGet]
        [Route("GetByCategory/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var products = await _unitOfWork.Product.GetAllFilterAsync(x => x.CategoryId == id, c => c.Category, z => z.Brand, s => s.Storage, r => r.Rates, com => com.Comments);
            if (products.Count() == 0)
                return BadRequest("Not Found");
            return Ok(products);
        }

        [HttpGet]
        [Route("GetByBrand/{id}")]
        public async Task<IActionResult> GetByBrand(int id)
        {
            var products = await _unitOfWork.Product.GetAllFilterAsync(x => x.BrandId == id, c => c.Category, z => z.Brand, s => s.Storage, r => r.Rates, com => com.Comments);
            if (products.Count() == 0)
                return BadRequest("Not Found");
            return Ok(products);
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