using System;
using System.Collections.Generic;
using System.Linq;
using AspWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList;

namespace AspWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        int pageSize = 10;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        // GET METHOD FOR PRODUCTS
        // RETURNS ALL PRODUCTS ON DATABASE
        // IF RECEIVES AN ID PARAMETER THAT CAN BE SUCCESSFULLY CONVERTETD TO AN INT,
        // RETURNS THE PRODUCT THAT HAS THAT ID
        // IF RECEIVES A STRING ID PARAMETER,
        // RETURNS THE PRODUCTS THAT CONTAIN THAT STRING ON THE NAME
        // IF RECEIVES A SUBCATEGORY PARAMETER,
        // RETURNS THE PRODUCTS THAT BELONG TO THAT SUBCATEGORY
        [HttpGet]
        public IEnumerable<Product> Get(string? id, string? subCategory, int? page)
        {           
            var lista = new List<Product>();
            using (DataContext db = new DataContext())
            {
                if (subCategory == null || subCategory.Equals(string.Empty))
                {
                    if (id == null || id.Equals(string.Empty))
                    {
                        lista = db.Products.ToPagedList(page.HasValue ? page.Value : 1, pageSize).ToList();
                    }
                    else
                    {
                        int idProduto = 0;
                        if (Int32.TryParse(id, out idProduto))
                            lista.AddRange(db.Products.Where(x => x.Id == idProduto).ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize));
                        else
                            lista.AddRange(db.Products.Where(x => x.Name.ToLower().Contains(id.ToLower())).ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize));
                    }
                }
                else
                {
                    int sub = 0;
                    if (Int32.TryParse(subCategory, out sub))
                    {
                        int idProduto = 0;
                        if (Int32.TryParse(id, out idProduto))
                            lista.AddRange(db.Products.Where(x => x.Id == idProduto).ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize));
                        else if(id != null)
                            lista.AddRange(db.Products.Where(x => x.Name.ToLower().Contains(id.ToLower()) && x.IdSubCategory == sub).ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize));
                        else
                            lista.AddRange(db.Products.Where(x => x.IdSubCategory == sub).ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize));
                    }
                }
                
            }

            return lista;
        }

        // POST METHOD FOR PRODUCT
        // SAVES A NEW PRODUCT ON THE DATABASE
        [HttpPost]
        public void Post(Product product)
        {
            using (DataContext db = new DataContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        // PUT METHOD FOR PRODUCT
        // ALTERS A PRODUCT ALREADY PRESENT ON THE DATABASE
        [HttpPut]
        public void Put(Product product)
        {
            using (DataContext db = new DataContext())
            {
                var produto  = db.Products.Where(x => x.Id == product.Id).First();
                produto.Name = product.Name;
                produto.Price = product.Price;
                db.SaveChanges();
            }
        }


        // DELETE METHOD FOR PRODUCT
        // DELETES A PRODUCT FROM THE DATABASE
        [HttpDelete]
        public void Delete(int id)
        {
            using (DataContext db = new DataContext())
            {
                var deleta = db.Products.Where(p => p.Id == id).FirstOrDefault();
                if (deleta != null)
                    db.Products.Remove(deleta);
                db.SaveChanges();
            }
        }

    }
}
