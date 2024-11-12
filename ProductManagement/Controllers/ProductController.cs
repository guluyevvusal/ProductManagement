using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using System.Linq;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>();

        // Məhsulların siyahısını göstərmək üçün GET metodu
        public IActionResult Index()
        {
            return View(products);
        }

        // Yeni məhsul əlavə etmək üçün GET metodu
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Product());
        }

        // Yeni məhsul əlavə etmək üçün POST metodu
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = products.Count + 1;
                products.Add(product);
                return RedirectToAction("Index"); // Məhsul əlavə olunduqdan sonra ana səhifəyə yönləndir
            }
            return View(product); // Əgər məlumatlar səhvdirsə, istifadəçiyə formu yenidən göstər
        }

        // Məhsul redaktə etmək üçün GET metodu
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Əgər məhsul tapılmırsa, səhv mesajı
            }
            return View(product);
        }

        // Məhsul redaktə etmək üçün POST metodu
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return NotFound(); // Əgər məhsul tapılmırsa, səhv mesajı
            }

            // Redaktə edilmiş məhsulu yeniləyirik
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            return RedirectToAction("Index"); // Yenilənmiş məhsul siyahısını göstərmək üçün ana səhifəyə yönləndir
        }

        // Məhsul silmək üçün metod
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
            return RedirectToAction("Index"); // Məhsul silindikdən sonra ana səhifəyə yönləndir
        }
    }
}
