using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastureManagement.Data;
using PastureManagement.Models;
using PastureManagement.Repositories;

namespace PastureManagement.Controllers.v1
{
   [Route("v1/animals")]
   [Authorize]
   public class AnimalController : ControllerBase
   {
      private readonly AnimalRepository _repository;

      public AnimalController(AnimalRepository repository)
      {
         _repository = repository;
      }

      [HttpGet]
      [Route("{id:int}")]
      public ActionResult<Animal> GetById(int id)
      {
         var pasture = _repository.GetById(id);
         return Ok(pasture);
      }

      [HttpGet]
      [Route("")]
      public ActionResult<List<Animal>> Get()
      {
         var animals = _repository.Get();
         return Ok(animals);
      }

      [HttpPost]
      [Route("")]
      public ActionResult Post([FromBody]Animal animal)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         try
         {
            _repository.Save(animal);
            return Ok(animal);
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível salvar o animal." });
         }
      }

      [HttpPut]
      [Route("{id:int}")]
      public ActionResult Put(int id, [FromBody]Animal animal)
      {
         if (id != animal.Id)
            return NotFound(new { message = "Animal não encontrado." });

         try
         {
            _repository.Update(animal);
            return Ok(animal);
         }
         catch (DbUpdateConcurrencyException)
         {
            return BadRequest(new { message = "Este animal já foi atualizado." });
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível atualizar o animal." });
         }
      }

      [HttpDelete]
      [Route("{id:int}")]
      public ActionResult Delete(int id, [FromServices]DataContext context)
      {
         try
         {
            _repository.Delete(id);
            return Ok(new { message = "Animal removido com sucesso." });
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível remover o animal." });
         }
      }
   }
}