using System;
using System.Collections.Generic;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STS.Controllers
{
    public class PersonsController : ApiBaseController
    {
        private readonly IPersonRepository _repository;

        public PersonsController(IPersonRepository repository)
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
            try
            {
                return Ok(_repository.GetById(id));
            }
            catch (Exception)
            {
                return BadRequest("Person not found");
            }

        }

        [HttpPost]
        public ActionResult<Person> Create(Person person)
        {
            _repository.Create(person);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<Person> Update(int id, PersonUpdateInfo personUpdateInfo)
        {
            Person person = _repository.GetById(id);
            person.Name = personUpdateInfo.Name;
            _repository.Update(person);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Person person = _repository.GetById(id);
            _repository.Delete(person);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
