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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        int pageSize = 10;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        // GET METHOD FOR CATEGORY
        // RETURNS ALL CATEGORIES ON DATABASE
        [HttpGet]
        public IEnumerable<Category> Get(int? page)
        {          
            using (DataContext db = new DataContext())
            {
                return db.Categories.ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize);
            }
        }

        // POST METHOD FOR CATEGORY
        // SAVES A NEW CATEGORY ON THE DATABASE
        [HttpPost]
        public void Post(Category category)
        {
            using (DataContext db = new DataContext())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        // PUT METHOD FOR CATEGORY
        // ALTERS A CATEGORY ALREADY PRESENT ON THE DATABASE
        [HttpPut]
        public void Put(Category category)
        {
            using (DataContext db = new DataContext())
            {
                var pUpdate = db.Categories.Where(p => p.Id == category.Id).FirstOrDefault();
                if(pUpdate != null)
                {
                    pUpdate.Name = category.Name;
                    db.SaveChanges();
                } 
            }
        }

        // DELETE METHOD FOR CATEGORY
        // DELETES A CATEGORY FROM THE DATABASE
        [HttpDelete]
        public void Delete(int id)
        {
            using (DataContext db = new DataContext())
            {
                var deleta = db.Categories.Where(p => p.Id == id).FirstOrDefault();
                if (deleta != null)
                    db.Categories.Remove(deleta);
                db.SaveChanges();
            }
        }
    }
}