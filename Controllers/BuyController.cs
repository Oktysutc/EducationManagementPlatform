﻿using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace EducationManagementPlatform.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class BuyController : Controller
    {
        private readonly IBuyRepository _buyRepository; // Dependency injection
        private readonly ICourseRepository _courseRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public BuyController(ApplicationDbContext context,IBuyRepository buyRepository, ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _buyRepository = buyRepository;
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Buy> objBuyList = _buyRepository.GetAll(includeProps: "Course").ToList();
            IEnumerable<SelectListItem> CourseList = _courseRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.CourseName,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseList = CourseList;
            return View(objBuyList);
        }

        public IActionResult Purchase(int id)
        {
            var course = _context.Courses
                .Where(c => c.Id == id)
                .Select(c => new PurchaseViewModel
                {
                    CourseId = c.Id,
                    CourseName = c.CourseName,
                    CourseImage = c.File,
                   
                })
                .FirstOrDefault();

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public IActionResult ConfirmPurchase(PurchaseViewModel model)
        {
            var purchase = new Purchase
            {
                CourseId = model.CourseId,
                CardName = model.CardName,
                CardNumber = model.CardNumber,
                CVC = model.CVC,
                ExpirationDate = model.ExpirationDate,
                Price = model.CoursePrice,
                PurchaseDate = DateTime.Now,
                UserId = User.Identity.Name // Kullanıcı bilgilerini almak için
            };

            _context.Purchases.Add(purchase);
            _context.SaveChanges();

            return RedirectToAction("Sales");
        }

        public async Task<IActionResult> Sales()
        {
            var sales = await _context.Purchases
                .Include(p => p.Course)  // Include Course related data
                .Select(p => new SalesViewModel
                {
                    
                    CourseImage = p.Course.File,
                    Purchaser = p.UserId
                }).ToListAsync();

            return View(sales);
        }


        [Authorize(Roles = "Admin,User")]
        public IActionResult Detail(int id)
        {
            var buy = _buyRepository.Get(c => c.Id == id, includeProps: "Buy");
            if (buy == null)
            {
                return NotFound();
            }
            return View(buy);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var buy = _buyRepository.Get(b => b.Id == id);
            if (buy == null)
            {
                return NotFound();
            }
            return Json(buy);
        }
        
        [HttpPost]
        public IActionResult AddUpdate(Buy buy)
        {
            if (ModelState.IsValid)
            {
                if (buy.Id == 0)
                {
                    _buyRepository.Add(buy);
                }
                else
                {
                    _buyRepository.Update(buy);
                }
                _buyRepository.Save();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var buy = _buyRepository.Get(b => b.Id == id);
            if (buy == null)
            {
                return NotFound();
            }
            _buyRepository.Delete(buy);
            _buyRepository.Save();
            return Json(new { success = true });
        }
        
    }
}
