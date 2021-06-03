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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        int pageSize = 10;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        // GET METHOD FOR USERS
        // RETURNS ALL USERS ON DATABASE
        [HttpGet]
        public IEnumerable<User> Get(int? page)
        {         
            using (DataContext db = new DataContext())
            {
                return db.Users.ToList().ToPagedList(page.HasValue ? page.Value : 1, pageSize);
            }
        }

        // POST METHOD FOR USERS
        // SAVES A NEW USER ON THE DATABASE
        [HttpPost]
        public void Post(User user)
        {
            using (DataContext db = new DataContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }


        // PUT METHOD FOR USERS
        // ALTERS A USER ALREADY PRESENT ON THE DATABASE
        [HttpPut]
        public void Put(User user)
        {
            using (DataContext db = new DataContext())
            {
                var pUpdate = db.Users.Where(p => p.IdUsuario == user.IdUsuario).FirstOrDefault();
                if(pUpdate != null)
                {
                    pUpdate.Name = user.Name;
                    pUpdate.Password = user.Password;
                    db.SaveChanges();
                } 
            }
        }

        // DELETE METHOD FOR USERS
        // DELETES A USER FROM THE DATABASE
        [HttpDelete]
        public void Delete(int id)
        {
            using (DataContext db = new DataContext())
            {
                var deleta = db.Users.Where(p => p.IdUsuario == id).FirstOrDefault();
                if (deleta != null)
                    db.Users.Remove(deleta);
                db.SaveChanges();
            }
        }
    }
}