using AppCleanArchitecture.Application.DTOs;
using AppCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppCleanArchitecture.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _catergoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductService productService,
                                ICategoryService categoryService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _catergoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Produtos-Disponiveis")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = 
                new SelectList(await _catergoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                await _productService.Add(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var productDTO = await _productService.GetById(id);

            if (productDTO == null) return NotFound();

            var categories = await _catergoryService.GetCategories();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDTO.CategoryId);

            return View(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                    await _productService.Update(productDTO);
                    return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productDTO = await _productService.GetById(id);

            if (productDTO == null) return NotFound();

            return View(productDTO);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var productDTO = await _productService.GetById(id);

            if (productDTO == null) return NotFound();

            var wwwroot = _webHostEnvironment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDTO.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDTO);
        }
    }
}
