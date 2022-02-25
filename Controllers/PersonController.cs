using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPI.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Pagination;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public readonly string listUrl = @"http://testlodtask20172.azurewebsites.net/task";
        private DataContext db;
        PersonFilter pf;

        public PersonController(DataContext context)
        {
            db = context;
            pf = new PersonFilter();
        }


        [Route("")]
        public async Task<ActionResult> AddDataToDB()
        {
            var detailedPeople = pf.GetPeople(listUrl);
            foreach (var item in detailedPeople)
            {
                if (!db.People.Contains(item))
                {
                    db.People.Add(item);
                    await db.SaveChanges();
                }
            }
            return Ok(db.People);
        }

        
        //https://localhost:44371/api/person/filter/?&AgeY=50&AgeX=10&filter=female&pageNumber=2&pageSize=2
        [Route("filter/")]
        public async Task<ActionResult> GetFromDbByFilter([FromQuery] PagingParameters param, [FromQuery] int? AgeX, [FromQuery] int? AgeY, [FromQuery] string filter)
        {
            var people = await db.People.ToListAsync<Person>();
            var peopleFilteredByAge = pf.FilterByAge(people, AgeX, AgeY);
            var peopleFilteredByGender = pf.FilterByGender(peopleFilteredByAge, filter);
            var pagedPeople = pf.Pagination(peopleFilteredByGender, param);

            return Ok(pagedPeople);
        }
        
        [Route("id/{id}")]
        public async Task<ActionResult> GetPersonById(string id="")
        {
            var person = await db.People.FirstOrDefaultAsync<Person>(x => x.Id == id);
            return Ok(person);
        }

        
    }
}
