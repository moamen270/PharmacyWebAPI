using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client.Extensions.Msal;
using PharmacyWebAPI.DataAccess;
using PharmacyWebAPI.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace e_Tickets.Data
{
    public class ApplicationDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            //Category
            if (!context.Categories.Any())
            {
                await context.Categories.AddRangeAsync(new List<Category>()
                {
                    new Category()
                        {
                            Name = "ادوية البرد",
                            ImgURL = "https://dwaprices.com/img/cold.png"
                        },
                        new Category()
                        {
                            Name = "الصداع النصفي",
                            ImgURL = "https://dwaprices.com/img/cold.png"
                        },
                        new Category()
                        {
                            Name = "مضاد جلطات",
                            ImgURL = "https://dwaprices.com/img/clot.png"
                        },
                        new Category()
                        {
                            Name = "فاتح الشهية",
                            ImgURL = "https://dwaprices.com/img/appetizer.png"
                        },
                        new Category()
                        {
                            Name = "الروماتيزم",
                            ImgURL = "https://dwaprices.com/img/rheumatic.png"
                        },
                        new Category()
                        {
                            Name = "ادوية السخونة",
                            ImgURL = "https://dwaprices.com/img/antipyretic.png"
                        },
                        new Category()
                        {
                            Name = "فيتامين-C",
                            ImgURL = "https://dwaprices.com/img/vitaminc.png"
                        },
                        new Category()
                        {
                            Name = "للديدان",
                            ImgURL = "https://dwaprices.com/img/worms.png"
                        },
                        new Category()
                        {
                            Name = "مقويات المناعة",
                            ImgURL = "https://dwaprices.com/img/immunity.png"
                        },
                        new Category()
                        {
                            Name = "ادويةالكحة",
                            ImgURL = "https://dwaprices.com/img/cough.png"
                        }
                });
                context.SaveChanges();
            }
            //Brand
            if (!context.Brands.Any())
            {
                context.Brands.AddRange(new List<Brand>()
                    {
                        new Brand()
                        {
                            Name = "Pfizer",
                            ImgURL = "https://www.rankingthebrands.com/logos/1047_Medium.png?20230408040455",
                        },
                        new Brand()
                        {
                            Name = "Roche",
                            ImgURL = "https://www.rankingthebrands.com/logos/1042_Medium.png?20230408040455",
                        },
                        new Brand()
                        {
                            Name = "Novartis",
                            ImgURL = "https://www.rankingthebrands.com/logos/351_Medium.png?20230408040455",
                        },
                        new Brand()
                        {
                            Name = "Johnson and Johnson",
                            ImgURL = "https://www.rankingthebrands.com/logos/374_Medium.png?20230408040455",
                        },
                        new Brand()
                        {
                            Name = "Sanofi",
                            ImgURL = "https://www.rankingthebrands.com/logos/353_Medium.png?20230408040455",
                        },
                        new Brand()
                        {
                            Name = "Amgen",
                            ImgURL = "https://www.rankingthebrands.com/logos/1056_Medium.png?20230408040455",
                        },
                        new Brand()
                        {
                            Name = "Takeda",
                            ImgURL = "https://www.rankingthebrands.com/logos/1050_Medium.png?20230408040455",
                        },
                        new Brand()
                        {
                            Name = "Bayer",
                            ImgURL = "https://www.rankingthebrands.com/logos/356_Medium.png?20230408040455",
                        },
                        new Brand()
                        {
                            Name = "Jamjoom Pharma",
                            ImgURL = "https://berlitz.sa/wp-content/uploads/2019/06/21.png",
                        },
                        new Brand()
                        {
                            Name = "AbbVie",
                            ImgURL = "https://www.rankingthebrands.com/logos/4591_Medium.png?20230408040455",
                        },
                    });
                context.SaveChanges();
            }
            //Brand
            if (!context.Storages.Any())
            {
                context.Storages.AddRange(new List<PharmacyWebAPI.Models.Storage>()
                    {
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1",Type = StorageType.Machine,},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1",Type = StorageType.Machine,},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1", Type = StorageType.Machine},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1", Type = StorageType.Machine},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1", Type = StorageType.Machine},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1", Type = StorageType.Machine},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1", Type = StorageType.Warehouse},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1", Type = StorageType.Warehouse},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1", Type = StorageType.Warehouse},
                        new PharmacyWebAPI.Models.Storage()
                        {Location = "Location1", Type = StorageType.Warehouse},
                    });
                context.SaveChanges();
            }
            /* //User
             if (!context.User.Any())
             {
                 for (int i = 0; i < 5; i++)
                 {
                     var acc1 = new User()
                     {
                         FirstName = $"PatientFN{i}",
                         LastName = $"PatientLN{i}",
                         Email = $"Patient{i}@ezdrug.com",
                         UserName = $"Patient{i}@ezdrug.com",
                         State = $"State{i}",
                         City = $"City{i}",
                         StreetAddress = $"StreetAddress{i}",
                         ProfilePictureURL = "https://cdn-icons-png.flaticon.com/512/149/149071.png",
                         EmailConfirmed = true
                     };
                     var result = await _userManager.CreateAsync(acc1, "Asdasd1");
                     for (int j = 0; j < 5; j++)
                     {
                         var acc2 = new User()
                         {
                             FirstName = $"DoctorFN{i}",
                             LastName = $"DoctorLN{i}",
                             Email = $"Doctor{i}@ezdrug.com",
                             UserName = $"Doctor{i}@ezdrug.com",
                             State = $"State{i}",
                             City = $"City{i}",
                             StreetAddress = $"StreetAddress{i}",
                             ProfilePictureURL = "https://www.shutterstock.com/image-vector/doctor-icon-260nw-593019092.jpg",
                             EmailConfirmed = true
                         };
                         var result2 = await _userManager.CreateAsync(acc1, "Asdasd1");
                     }
                 };
                 context.SaveChanges();
             }*/
            //Products
            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                                {
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =5,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =5,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =1,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =1,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =1,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =1,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =1,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =1,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =1,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =1,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =1,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =1,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =1,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =1,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }
                                  ,
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =2,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =2,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =2,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =2,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =2,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =2,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }
                                   ,
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =2,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =2,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =2,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =2,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =2,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =2,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }//
                                  ,
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =3,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =3,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }
                                   ,
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =3,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =3,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =3,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }//
                                  //
                                  ,
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =4,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =4,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }
                                   ,
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =4,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =4,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =4,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }//
                                  ,
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =5,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =5,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =5,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =5,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =5,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =5,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }
                                   ,
                                     new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =5,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =5,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =5,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                    new Product()
                                    {
                                        EnglishName = "Product 1",
                                        type = "This is the Bio of the first actor",
                                        ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                        Contraindications = "Product 1",
                                        Description = "Product 1",
                                        Stock = 10,
                                        Price = 10,
                                        BrandId = 3,
                                        CategoryId =5,
                                        ExpDate = DateTime.Now.AddDays(100),
                                        StorageId = 1
                                    },
                                 new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =5,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 },
                                  new Product()
                                 {
                                     EnglishName = "Product 1",
                                     type = "This is the Bio of the first actor",
                                     ImgURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCFPYYIUmUPRlQ8tEvuBAu8k9ua6lzqKfNWgsZCTvepg&s",
                                     Contraindications = "Product 1",
                                     Description = "Product 1",
                                     Stock = 10,
                                     Price = 10,
                                     BrandId = 3,
                                     CategoryId =5,
                                     ExpDate = DateTime.Now.AddDays(100),
                                     StorageId = 1
                                 }
                             });
                context.SaveChanges();
            }
        }
    }
}