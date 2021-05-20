using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Models.Requests;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STS.Controllers
{
    [Authorize(Policy = "")]
    public class PersonsController : ApiBaseController
    {
        private readonly IPersonRepository _repository;

        public PersonsController(IPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetById(int id)
        {
            try
            {
                return Ok(await _repository.GetByIdAsync(id));
            }
            catch (Exception)
            {
                return BadRequest("Person not found");
            }

        }

        [HttpPost]
        public async Task<ActionResult<Person>> Create(Person person)
        {
            await _repository.CreateAsync(person);
            await _repository.SaveChangesAsync();
            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> Update(int id, PersonUpdateRequest personUpdateInfo)
        {
            Person person = await _repository.GetByIdAsync(id);
            person.Name = personUpdateInfo.Name;
            _repository.Update(person);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Person person = await _repository.GetByIdAsync(id);
            _repository.Delete(person);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
