using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Areas.Admin.ViewModels.AdminAuthorVM;
using PustokMVC.Areas.Admin.ViewModels.AdminBlogVM;
using PustokMVC.Areas.Admin.ViewModels.AdminHomeVM;
using PustokMVC.Areas.Admin.ViewModels.AdminProductImageVM;
using PustokMVC.Areas.Admin.ViewModels.AdminProductVM;
using PustokMVC.Areas.Admin.ViewModels.AdminTagVM;
using PustokMVC.Contexts;
using PustokMVC.Helpers;
using PustokMVC.Models;
using PustokMVC.ViewModels.AuthorVM;
using PustokMVC.ViewModels.CategoryVM;
using PustokMVC.ViewModels.ProductVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    { 
        PustokDbContext _context { get; }

        public AdminHomeController(PustokDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var models = new AdminHomeListVM();
            models.Sliders = await _context.Sliders.Select(slider => new SliderListVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Description = slider.Description,
                Price = slider.Price,
                ImgUrl = slider.ImgUrl,
                IsRight = slider.IsRight
            }).ToListAsync();
            models.Products = await _context.Products.Select(product => new AdminProductListVM
            {
                Id = product.Id,
                Name = product.Name,
                About = product.About,
                ExTax = product.ExTax,
                Brand = product.Brand,
                RewardPoints = product.RewardPoints,
                IsAvailable = product.IsAvailable,
                ActualPrice = product.ActualPrice,
                SellPrice = product.SellPrice,
                Discount = product.Discount,
                Quantity = product.Quantity,
                Images = product.Images,
                Category = product.Category,
                IsDeleted = product.IsDeleted,
            }).ToListAsync();
            models.Categories = await _context.Categories.Select(category => new CategoryListVM
            {
                Id=category.Id,
                Name = category.Name,
                IsDeleted = category.IsDeleted,
                ParentCategoryId = category.ParentCategoryId,
            }).ToListAsync();
            models.Authors = await _context.Authors.Select(author => new AdminAuthorListVM
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
            }).ToListAsync();
            models.Blogs = await  _context.Blogs.Select(blog => new AdminBlogListVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Author = blog.Author,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
                Tags = blog.BlogTags.Select(bt => bt.Tag.Title).ToList(),
                IsDeleted = blog.IsDeleted,
            }).ToListAsync();
            models.ProductImages = await _context.ProductImages.Select(image => new AdminProductImageListVM
            {
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsActive = image.IsActive,
                Product = image.Product,
            }).ToListAsync();
            models.Tags = await _context.Tags.Select(tag => new AdminTagListVM
            {
                Id = tag.Id,
                Title = tag.Title,
            }).ToListAsync();
            return View(models);
        }
        //Slider actions
        public async Task<IActionResult> CreateSlider()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSlider(SliderCreateVM sliderVM)
        {
            TempData["Response"] = "temp";
            if (!ModelState.IsValid)
            {
                return View(sliderVM);
            }
            Slider slider = new Slider
            {
                Title = sliderVM.Title,
                Description = sliderVM.Description,
                ImgUrl = sliderVM.ImgUrl,
                Price = sliderVM.Price,
                IsRight = sliderVM.Position
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            TempData["Response"] = "created";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteSlider(int? id)
        {
            TempData["Response"] = "temp";
            if (id == null) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            _context.Sliders.Remove(data);
            await _context.SaveChangesAsync();
            TempData["Response"] = "deleted";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateSlider(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            return View(new SliderUpdateVM
            {
                Title = data.Title,
                Description = data.Description,
                ImgUrl = data.ImgUrl,
                Price = data.Price,
                Position = data.IsRight
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSlider(int? id, SliderUpdateVM sliderVM)
        {
            TempData["Response"] = "temp";
            if (!ModelState.IsValid)
            {
                return View(sliderVM);
            }
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.Title = sliderVM.Title;
            data.Description = sliderVM.Description;
            data.ImgUrl = sliderVM.ImgUrl;
            data.Price = sliderVM.Price;
            data.IsRight = sliderVM.Position;
            await _context.SaveChangesAsync();
            TempData["Response"] = "updated";
            return RedirectToAction(nameof(Index));
        }
        //Category Actions
        public async Task<IActionResult> CreateCategory()
        {
            ViewBag.Categories = _context.Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateVM categoryVM)
        {
            TempData["Response"] = "temp";
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories;
                return View(categoryVM);
            }
            Category category = new Category
            {
                Name = categoryVM.Name,
                ParentCategoryId = categoryVM.ParentCategoryId,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            TempData["Response"] = "created";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            TempData["Response"] = "temp";
            if (id == null) return BadRequest();
            var data = await _context.Categories.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _context.SaveChangesAsync();
            TempData["Response"] = "deleted";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateCategory(int? id)
        {
            ViewBag.Categories = _context.Categories;
            if (id == null || id <= 0) return BadRequest();
            var data = await _context.Categories.FindAsync(id);
            if (data == null) return NotFound();
            return View(new CategoryUpdateVM
            {
                Name = data.Name,
                ParentCategoryId = data.ParentCategoryId,
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int? id, CategoryUpdateVM categoryVM)
        {
            TempData["Response"] = "temp";
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories;
                return View(categoryVM);
            }
            var data = await _context.Categories.FindAsync(id);
            if (data == null) return NotFound();
            data.Name = categoryVM.Name;
            data.ParentCategoryId = categoryVM.ParentCategoryId;
            await _context.SaveChangesAsync();
            TempData["Response"] = "updated";
            return RedirectToAction(nameof(Index));
        }
        //Product Actions
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = _context.Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateVM productVM)
        {
            TempData["Response"] = "temp";
            if (productVM.ActualPrice > productVM.SellPrice)
            {
                ModelState.AddModelError("ActualPrice", "Sell price must be bigger than cost price");
                ViewBag.Categories = _context.Categories;
                return View(productVM);
            }
            if (!await _context.Categories.AnyAsync(c => c.Id == productVM.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category doesnt exist");
                ViewBag.Categories = _context.Categories;
                return View(productVM);
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories;
                return View(productVM);
            }
            Product product = new Product
            {
                Name = productVM.Name,
                ExTax = productVM.ExTax,
                Brand = productVM.Brand,
                RewardPoints = productVM.RewardPoints,
                ActualPrice = productVM.ActualPrice,
                SellPrice = productVM.SellPrice,
                Discount = productVM.Discount,
                About = productVM.About,
                Description = productVM.Description,
                Quantity = productVM.Quantity,
                IsAvailable = productVM.IsAvailable,
                CategoryId = productVM.CategoryId,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            TempData["Response"] = "created";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            TempData["Response"] = "temp";
            if (id == null) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _context.SaveChangesAsync();
            TempData["Response"] = "deleted";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateProduct(int? id)
        {
            ViewBag.Categories = _context.Categories;
            if (id == null || id <= 0) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data == null) return NotFound();
            return View(new ProductUpdateVM
            {
                Name = data.Name,
                ExTax = data.ExTax,
                Brand = data.Brand,
                RewardPoints = data.RewardPoints,
                IsAvailable = data.IsAvailable,
                ActualPrice = data.ActualPrice,
                SellPrice = data.SellPrice,
                Discount = data.Discount,
                About = data.About,
                Description = data.Description,
                Quantity = data.Quantity,
                CategoryId = data.CategoryId,
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int? id, ProductUpdateVM productVM)
        {
            TempData["Response"] = "temp";
            if (!ModelState.IsValid)
            {
                return View(productVM);
            }
            if (productVM.ActualPrice > productVM.SellPrice)
            {
                ModelState.AddModelError("ActualPrice", "Sell price must be bigger than cost price");
            }
            if (!await _context.Categories.AnyAsync(c => c.Id == productVM.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category doesnt exist");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories;
                return View(productVM);
            }
            var data = await _context.Products.FindAsync(id);
            if (data == null) return NotFound();
            data.Name = productVM.Name;
            data.ExTax = productVM.ExTax;
            data.Brand = productVM.Brand;
            data.RewardPoints = productVM.RewardPoints;
            data.IsAvailable = productVM.IsAvailable;
            data.ActualPrice = productVM.ActualPrice;
            data.SellPrice = productVM.SellPrice;
            data.Discount = productVM.Discount;
            data.About = productVM.About;
            data.Description = productVM.Description;
            data.Quantity = productVM.Quantity;
            data.CategoryId = productVM.CategoryId;
            await _context.SaveChangesAsync();
            TempData["Response"] = "updated";
            return RedirectToAction(nameof(Index));
        }
        //author actions
        public async Task<IActionResult> CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AdminAuthorCreateVM authorVM)
        {
            TempData["Response"] = "temp";
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }
            Author author = new Author
            {
                Name = authorVM.Name,
                Surname = authorVM.Surname,
            };
            await _context.AddAsync(author);
            await _context.SaveChangesAsync();
            TempData["Response"] = "created";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteAuthor(int? id)
        {
            TempData["Response"] = "temp";
            if (id == null) return BadRequest();
            var data = await _context.Authors.FindAsync(id);
            if (data == null) return NotFound();
            _context.Remove(data);
            await _context.SaveChangesAsync();
            TempData["Response"] = "temp";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateAuthor(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Authors.FindAsync(id);
            if (data == null) return NotFound();
            return View(new AdminAuthorUpdateVM
            {
                Name = data.Name,
                Surname = data.Surname,
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAuthor(int? id, AdminAuthorUpdateVM authorVM)
        {
            TempData["Response"] = "temp";
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }
            var data = await _context.Authors.FindAsync(id);
            if (data == null) return NotFound();
            data.Name = authorVM.Name;
            data.Surname = authorVM.Surname;
            await _context.SaveChangesAsync();
            TempData["Response"] = "updated";
            return RedirectToAction(nameof(Index));
        }
        //blog actions
        public async Task<IActionResult> CreateBlog()
        {
            ViewBag.Authors = _context.Authors;
            ViewBag.Tags = _context.Tags;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlog(AdminBlogCreateVM blogVM)
        {
            TempData["Response"] = "temp";
            if (!await _context.Authors.AnyAsync(author=> author.Id == blogVM.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author does not exist!");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors;
                ViewBag.Tags = _context.Tags;
                return View(blogVM);
            }
            Blog blog = new Blog
            {
                Title = blogVM.Title,
                Description = blogVM.Description,
                AuthorId = blogVM.AuthorId,
                BlogTags = blogVM.TagIds?.Select(id=> new BlogTag
                {
                    TagId = id,
                }).ToList(),
            };
            await _context.AddAsync(blog);
            await _context.SaveChangesAsync();
            TempData["Response"] = "created";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteBlog(int? id)
        {
            TempData["Response"] = "temp";
            if (id == null) return BadRequest();
            var data = await _context.Blogs.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _context.SaveChangesAsync();
            TempData["Response"] = "deleted";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateBlog(int? id)
        {
            ViewBag.Authors = _context.Authors;
            ViewBag.Tags = _context.Tags;
            if (id == null) return BadRequest();
            var data = await _context.Blogs.FindAsync(id);
            if (data == null) return NotFound();
            return View( new AdminBlogUpdateVM
            {
                Title = data.Title,
                Description = data.Description,
                AuthorId = data.AuthorId,
                //TagIds = data.BlogTags?.Select(b => b.TagId).ToList(),
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBlog(int? id, AdminBlogUpdateVM blogVM)
        {
            TempData["Response"] = "temp";
            var data = await _context.Blogs.FindAsync(id);
            if (data == null) return NotFound();
            if (!await _context.Authors.AnyAsync(author => author.Id == blogVM.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author does not exist!");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors;
                return View(blogVM);
            }
            data.Title = blogVM.Title;
            data.Description = blogVM.Description;
            data.AuthorId = blogVM.AuthorId;
            data.BlogTags = blogVM.TagIds?.Select(id => new BlogTag
            {
                TagId = id,
            }).ToList();
            data.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            TempData["Response"] = "updated";
            return RedirectToAction(nameof(Index));
        }
        //product image actions
        public async Task<IActionResult> CreateProductImage()
        {
            ViewBag.Products = _context.Products;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductImage(AdminProductImageCreateVM imageVM)
        {
            TempData["Response"] = "temp";
            if (!await _context.Products.AnyAsync(product => product.Id == imageVM.ProductId))
            {
                ModelState.AddModelError("ProductId", "Product doesnt exist!");
            }
            if (!imageVM.ImageFile.IsImageType())
            {
                ModelState.AddModelError("ImageFile", "File must be an image!");
            }
            if (!imageVM.ImageFile.IsValidSize(1000))
            {
                ModelState.AddModelError("ImageFile", "File is too big!");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Products = _context.Products;
                return View(imageVM);
            }
            ProductImage image = new ProductImage
            {
                ImagePath = await imageVM.ImageFile.SaveAsync(PathConstants.ProductImage),
                ProductId = imageVM.ProductId,
                IsActive = imageVM.IsActive,
            };
            await _context.ProductImages.AddAsync(image);
            await _context.SaveChangesAsync();
            TempData["Response"] = "created";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteProductImage(int? id)
        {
            TempData["Response"] = "temp";
            if (id == null) return BadRequest();
            var data = await _context.ProductImages.FindAsync(id);
            if (data == null) return NotFound();
            _context.ProductImages.Remove(data);
            await _context.SaveChangesAsync();
            TempData["Response"] = "deleted";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateProductImage(int? id)
        {
            ViewBag.Products = _context.Products;
            if (id == null) return BadRequest();
            var data = await _context.ProductImages.FindAsync(id);
            if (data == null) return NotFound();
            return View(new AdminProductImageUpdateVM
            {
                ProductId = data.ProductId,
                IsActive = data.IsActive,
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(int? id, AdminProductImageUpdateVM imageVM)
        {
            TempData["Response"] = "temp";
            if (!await _context.Products.AnyAsync(product => product.Id == imageVM.ProductId))
            {
                ModelState.AddModelError("ProductId", "Product doesnt exist!");
            }
            if (imageVM.ImageFile != null)
            {
                if (!imageVM.ImageFile.IsImageType())
                {
                    ModelState.AddModelError("ImageFile", "File must be an image!");
                }
                if (!imageVM.ImageFile.IsValidSize(1000))
                {
                    ModelState.AddModelError("ImageFile", "File is too big!");
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Products = _context.Products;
                return View(imageVM);
            }
            var data = await _context.ProductImages.FindAsync(id);
            if (data == null) return NotFound();
            data.ImagePath = imageVM.ImageFile == null ? data.ImagePath : await imageVM.ImageFile.SaveAsync(PathConstants.ProductImage);
            data.ProductId = imageVM.ProductId;
            data.IsActive = imageVM.IsActive;
            await _context.SaveChangesAsync();
            TempData["Response"] = "updated";
            return RedirectToAction(nameof(Index));
        }
        //tag actions
        public async Task<IActionResult> CreateTag()
        {
            ViewBag.Products = _context.Products;
            return View();
        }
    }
}
