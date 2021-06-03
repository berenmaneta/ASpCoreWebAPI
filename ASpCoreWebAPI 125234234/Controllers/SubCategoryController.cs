using System.Collections.Generic;
using System.Linq;
using AspWebAPI.Data;
using AspWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList;

namespace AspWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ILogger<SubCategoryController> _logger;
        int pageSize = 10;

        public SubCategoryController(ILogger<SubCategoryController> logger)
        {
            _logger = logger;
        }

        // GET METHOD FOR SUBCATEGORY
        // RETURNS ALL SUBCATEGORIES ON DATABASE
        [HttpGet]
        public IEnumerable<SubCategory> Get(int? idCategory, int? page)
        {
            using (DataContext db = new DataContext())
            {
                if(idCategory.HasValue)
                    return db.SubCategories.Where(x => x.IdCategory == idCategory.Value).ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize);
                else
                    return db.SubCategories.ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize);
            }
        }

        // POST METHOD FOR SUBCATEGORY
        // SAVES A NEW SUBCATEGORY ON THE DATABASE
        [HttpPost]
        public void Post(SubCategory SubCategory)
        {
            using (DataContext db = new DataContext())
            {
                db.SubCategories.Add(SubCategory);
                db.SaveChanges();
            }
        }

        // PUT METHOD FOR SUBCATEGORY
        // ALTERS A SUBCATEGORY ALREADY PRESENT ON THE DATABASE
        [HttpPut]
        public void Put(SubCategory SubCategory)
        {
            using (DataContext db = new DataContext())
            {
                var pUpdate = db.SubCategories.Where(p => p.Id == SubCategory.Id).FirstOrDefault();
                if(pUpdate != null)
                {
                    pUpdate.Name = SubCategory.Name;
                    db.SaveChanges();
                } 
            }
        }

        // DELETE METHOD FOR SUBCATEGORY
        // DELETES A SUBCATEGORY FROM THE DATABASE
        [HttpDelete]
        public void Delete(int id)
        {
            using (DataContext db = new DataContext())
            {
                var deleta = db.SubCategories.Where(p => p.Id == id).FirstOrDefault();
                if (deleta != null)
                    db.SubCategories.Remove(deleta);
                db.SaveChanges();
            }
        }
    }
}