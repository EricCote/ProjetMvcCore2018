using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AfiProjet.Models;
using AfiProjet.ViewModel;
using Microsoft.AspNetCore.Http;

namespace AfiProjet.Controllers
{
    public class ProductController : Controller
    {
        private readonly AWContext _db;


        public ProductController(AWContext context)
        {
            _db = context;
        }

        public ActionResult Search(string id)
        {
            return View("Search", id);
        }


        public ActionResult Writer()
        {
            return View();
        }

        [HttpPost()]
        public ActionResult Writer(MyProfile myProfile)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = new DateTimeOffset(DateTime.Now.AddYears(1));
            Response.Cookies.Append("langue", myProfile.Langue, option);

            HttpContext.Session.SetString("nom", myProfile.Nom);


            return View(myProfile);
        }


        public ActionResult Reader()
        {
            var myProfile = new MyProfile();
            myProfile.Langue = Request.Cookies["langue"];
            myProfile.Nom = HttpContext.Session.GetString("nom");
            return View(myProfile);
        }



        public async Task<PartialViewResult> TableResult(string filter="")
        {
        
            var requete1 = _db.Products.
                             Include(p => p.ProductCategory).
                             Include(p => p.ProductModel)
                            .Where(p => p.Name.Contains(filter) && 
                                        !string.IsNullOrEmpty(filter)  )
                            .OrderBy(p => p.Name);
             
            return PartialView("_ProductGrid", await requete1.ToListAsync());
        }





        [Route("Category/{categoryName}/{id=0}")]
        public async Task<IActionResult> ProductByCategory(string categoryName, int id)
        {
            int noPage = id;
            var requete1 = _db.Products
                           .Include(p => p.ProductCategory)
                           .Include(p => p.ProductModel)
                           .Where(p => p.ProductCategory.Name == categoryName)
                           .OrderBy(p => p.Name)
                           .Skip(10 * noPage)
                           .Take(10);

            int nbProduits = _db.Products
                             .Count(p => p.ProductCategory.Name == categoryName);
            int nbPages = ((nbProduits - 1) / 10) + 1;
            ViewBag.nbPages = nbPages;
            ViewBag.noPage = noPage;
            ViewBag.listeCategories = new SelectList(_db.ProductCategories.
                                               Where(c=>c.ProductCategoryId>4),
                                               "Name", 
                                               "Name", 
                                               categoryName);

            return View( await requete1.ToListAsync());

             

        }



        // GET: Product
        public async Task<IActionResult> Index(int id = 0)
        {
            int noPage = id;
            var requete1 = _db.Products.
                             Include(p => p.ProductCategory).
                             Include(p => p.ProductModel)
                            .OrderBy(p => p.Name)
                            .Skip(10 * noPage)
                            .Take(10);

            int nbProduits = _db.Products.Count();
            int nbPages = ((nbProduits - 1) / 10) + 1;
            ViewBag.nbPages = nbPages;
            ViewBag.noPage = noPage;
            ViewBag.listeCategories = new SelectList(_db.ProductCategories.
                                               Where(c => c.ProductCategoryId > 4),
                                               "Name",
                                               "Name"
                                               );

            return View( await requete1.ToListAsync());
        }

        [Route("Product/{id}.gif")]
        public FileResult GetPicture(int id)
        {
            ProductImage product = _db.ProductImages.Find(id);
            if (product?.ThumbNailPhoto == null)
            {
                return File("/images/error.gif", "image/gif");
            }
            return File(product.ThumbNailPhoto, "image/gif");
        }


        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_db.ProductCategories, "ProductCategoryId", "Name");
            ViewData["ProductModelId"] = new SelectList(_db.ProductModels, "ProductModelId", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,Rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductImage = new ProductImage();
                _db.Entry(product.ProductImage).State = EntityState.Added;
                _db.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["ProductModelId"] = new SelectList(_db.ProductModels, "ProductModelId", "Name", product.ProductModelId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["ProductModelId"] = new SelectList(_db.ProductModels, "ProductModelId", "Name", product.ProductModelId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,Rowguid,ModifiedDate")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(product);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["ProductModelId"] = new SelectList(_db.ProductModels, "ProductModelId", "Name", product.ProductModelId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _db.Products.FindAsync(id);

            product.ProductImage = new ProductImage();
            _db.Entry(product.ProductImage).State = EntityState.Deleted;

            _db.Products.Remove(product);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _db.Products.Any(e => e.ProductId == id);
        }
    }
}
