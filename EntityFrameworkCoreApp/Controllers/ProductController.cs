using EntityFrameworkCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public IActionResult List()
        {
            return View(repository.Products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            repository.CreateProduct(product);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(repository.GetById(id));
        }

        [HttpPost]
        public IActionResult Details(Product product)
        {
            repository.UpdateProduct(product);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(repository.GetById(id));
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            repository.DeleteProduct(product);
            return RedirectToAction("List");
        }
    }
}
