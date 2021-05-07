using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STS.Controllers
{
    public class PersonController : ApiBaseController
    {
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return Ok(_repository.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetById(int id)
        {
            return Ok(_repository.GetById(id));
        }

        [HttpPost]
        public ActionResult<Person> Create(Person person)
        {
            _repository.Create(person);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<Person> Update(int id, Person personUpdateInfo)
        {
            Person person = _repository.GetById(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<String> Delete()
        {
            return "dsa";
        }
    }
}
