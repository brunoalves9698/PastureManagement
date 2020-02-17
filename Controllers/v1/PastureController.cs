using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastureManagement.Data;
using PastureManagement.Models;
using PastureManagement.Repositories;

namespace PastureManagement.Controllers.v1
{
   [Route("v1/pastures")]
   [Authorize]
   public class PastureController : ControllerBase
   {
      private readonly PastureRepository _repository;

      public PastureController(PastureRepository repository)
      {
         _repository = repository;
      }

      [HttpGet]
      [Route("{id:int}")]
      public ActionResult<Pasture> GetById(int id)
      {
         var pasture = _repository.GetById(id);
         return Ok(pasture);
      }

      [HttpGet]
      [Route("")]
      public ActionResult<List<Pasture>> Get()
      {
         var pastures = _repository.Get();
         return Ok(pastures);
      }

      [HttpPost]
      [Route("")]
      public ActionResult Post([FromBody]Pasture pasture)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         try
         {
            _repository.Save(pasture);
            return Ok(pasture);
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível salvar o pasto." });
         }
      }

      [HttpPut]
      [Route("{id:int}")]
      public ActionResult Put(int id, [FromBody]Pasture pasture)
      {
         if (id != pasture.Id)
            return NotFound(new { message = "Pasto não encontrado." });

         try
         {
            _repository.Update(pasture);
            return Ok(pasture);
         }
         catch (DbUpdateConcurrencyException)
         {
            return BadRequest(new { message = "Este pasto já foi atualizado." });
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível atualizar o pasto." });
         }
      }

      [HttpDelete]
      [Route("{id:int}")]
      public ActionResult Delete(int id, [FromServices]DataContext context)
      {
         try
         {
            _repository.Delete(id);
            return Ok(new { message = "Pasto removido com sucesso." });
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível remover o Pasto." });
         }
      }
   }
}