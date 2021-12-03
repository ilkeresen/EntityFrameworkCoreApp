# EntityFrameworkCoreApp

Program.cs dosyamıza eklemeyi unutmuyoruz. </br>
`.UseDefaultServiceProvider(options=>options.ValidateScopes=false)` </br>
"ConnectionStrings" oluşturup database bağlantımızı yazıyoruz.

![](https://i.resim.host/jMOZyT.png)

"ConfigureServices" içerisine servislerimizi ekliyoruz UseSqlServer & Repository 

![](https://i.resim.host/JPGsIL.png)

```javascript
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=List}/{id?}");
            });

            SeedData.Seed(app);
        }
```

![](https://i.resim.host/YyNdRT.png)

`Product Modelimizi oluşturuyoruz.`

![](https://i.resim.host/rfTKQy.png)

Seed data ekliyoruz programı çalıştırdığımızda database verileri ekleyecektir.
Fakat veritabanı bağlantısı için dotnet ef database update dememiz de tablolarımızı oluşturmamızda yeterli olacaktır.

![](https://i.resim.host/uf1Qgs.png)

Product IRepository oluşturuyoruz. (Listeleme/Güncelleme/Silme/Ekleme/Id ye göre veri çekme)

![](https://i.resim.host/E3s3x1.png)

EfProductRepository ile veritabanı işlemlerimizi gerçekleştiriyoruz.
```javascript
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.Models
{
    public class EfProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public EfProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public Product GetById(int productid)
        {
            return _context.Products.FirstOrDefault(i => i.ProductId == productid);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}

```

![](https://i.resim.host/J9gAzO.png)

ApplicationDbContext ile veritabanı içinde yer alan verilerimizle alakalı olarak her türlü süreçte iletişimimizi sağlayan bir class oluşturuyoruz.

![](https://i.resim.host/7oRo3s.png)

Listeleme,ekleme,güncelleme,Silme işlerimleri için Actionlarımızı yazdık. 

constructor ile repository çalıştırıyoruz.

İşlemler sonrasında return RedirectToAction("List"); diyerek tekrar listemizin sayfasına gönderiyoruz.

```javascript
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

```

![](https://i.resim.host/kdNUeL.png)

<h3>Migration Nedir?</h3>
<p>
İngilizcede doğrudan kelime anlamı göç, yani şöyle düşünün benim buradaki bilgilerim, kurallarım veritabanına göç ediyor. Böyle hayal edin, tasvir doğru mu oldu bilemiyorum ama uygun gibi. Tam olarak yapılan da bu, uygulamamda dediğim kurallar, model classlar, bağıntıları migration ile anlatıyorum.
<p>
Migration 3 adımlı bir işlemdir.
<p>
-Öncelikle model oluşturur veya modellerimizde değişiklik yaparız. Bu noktada yeni bir model oluşturmuş olabilirsiniz veya var olan modellerinizde değişiklik yapmış olabilirsiniz. Böyle bir işlem yaptığınızda mutlaka migration ile database tarafına değişiklik yansıtılmalıdır.
  <p>
-Migration Ekleme işlemi yaparız. Database tarafına değişikliği yansıtabilmek için migration ekleriz.
    <p>
-Migration uygulama işlemi yaparız. Migration uygulamamıza bir isimle eklenince, henüz işlem database yansıdı demek değildir bunu database’e yansıtabilmek için migration’u database’e push etmemiz gerekir.
<p>
  Migration komutlarında (add,bundle,list,remove,script) bulunmaktadır.</br>
  Oluşturduğumuz migrationsları</br>
  dotnet ef database (update,drop) ile çalıştırmalıyız.</br>
  
![](https://i.resim.host/ha8eXp.png)

![](https://i.resim.host/lN7UYw.png)

Cmd yada Terminal ekranında `dotnet ef database update` dediğimizde tablolarımız oluşmuş olacaktır fakat biz seed datalarımızı eklediğimiz için datalarımız da veritabanımıza eklenmiş oldu.

![](https://i.resim.host/o1k31A.png)

Bootstrap ile view sayfalarımızı daha düzgün görünümlü hale getiriyoruz Product Modelimiz ile Productlarımızı listeliyoruz. </br>

`@model IEnumerable<Product>` ile verilerimizi alıyoruz </br>

`@foreach (var item in Model)` ile verilerimizi listeliyoruz. </br>

```javascript
@model IEnumerable<Product>

    <div class="table-responsive">
        <table class="table table-dark">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Name</th>
                    <th scope="col">Description</th>
                    <th scope="col">Price</th>
                    <th scope="col">Category</th>
                    <th scope="col">Stock</th>
                    <th scope="col">Process</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var item in Model)
                {
                    <tr>
                        <th scope="row">@item.ProductId</th>
                        <td>@item.Name</td>
                        <td>@item.Description</td>
                        <td>@item.Price</td>
                        <td>@item.Category</td>
                        <td>@item.Stock</td>
                        <td>
                            <a class="btn btn-success btn-sm" asp-controller="Product" asp-action="Details" asp-route-id="@item.ProductId">
                                Details
                            </a>
                            |
                            <a class="btn btn-danger btn-sm" asp-controller="Product" asp-action="Delete" asp-route-id="@item.ProductId">
                                Delete
                            </a>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    </div>
    <a class="btn btn-dark btn-sm" asp-controller="Product" asp-action="Create">
        Create New Product
    </a>


```

![](https://i.resim.host/k1YynW.png)

Tasarımları da aşağıda göründüğü gibi.

![](https://i.resim.host/Oe94Ne.png)
![](https://i.resim.host/r24AwY.png)
![](https://i.resim.host/dC7Rw0.png)
