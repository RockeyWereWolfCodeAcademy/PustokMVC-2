using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Areas.Admin.ViewModels.AdminBlogVM;
using PustokMVC.Areas.Admin.ViewModels.AdminHomeVM;
using PustokMVC.Areas.Admin.ViewModels.AdminProductImageVM;
using PustokMVC.Areas.Admin.ViewModels.AdminProductVM;
using PustokMVC.Contexts;
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
                IsDeleted = blog.IsDeleted,
            }).ToListAsync();
            models.ProductImages = await _context.ProductImages.Select(image => new AdminProductImageListVM
            {
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsActive = image.IsActive,
                Product = image.Product,
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
            }
            if (!ModelState.IsValid)
            {
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
                return View(productVM);
            }
            Product product = new Product
            {
                Name = productVM.Name,
                ExTax = productVM.ExTax,
                Brand = productVM.Brand,
                RewardPoints = productVM.RewardPoints,
                IsAvailable = productVM.IsAvailable,
                ActualPrice = productVM.ActualPrice,
                SellPrice = productVM.SellPrice,
                Discount = productVM.Discount,
                About = productVM.About,
                Description = productVM.Description,
                Quantity = productVM.Quantity,
                Images = productVM.Images,
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
                Images = data.Images,
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
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories;
                return View(productVM);
            }
            if (!await _context.Categories.AnyAsync(c => c.Id == productVM.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category doesnt exist");
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
            data.Images = productVM.Images;
            data.CategoryId = productVM.CategoryId;
            await _context.SaveChangesAsync();
            TempData["Response"] = "updated";
            return RedirectToAction(nameof(Index));
        }
    }
}
