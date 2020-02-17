using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastureManagement.Data;
using PastureManagement.Models;
using PastureManagement.Repositories;

namespace PastureManagement.Controllers.v1
{
   [Route("v1/herd-categories")]
   [Authorize]
   public class HerdCategoryController : ControllerBase
   {
      private readonly HerdCategoryRepository _repository;

      public HerdCategoryController(HerdCategoryRepository repository)
      {
         _repository = repository;
      }

      [HttpGet]
      [Route("{id:int}")]
      public ActionResult<HerdCategory> GetById(int id)
      {
         var category = _repository.GetById(id);
         return Ok(category);
      }

      [HttpGet]
      [Route("")]
      public ActionResult<List<HerdCategory>> Get()
      {
         var categories = _repository.Get();
         return Ok(categories);
      }

      [HttpPost]
      [Route("")]
      public ActionResult Post([FromBody]HerdCategory category)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         try
         {
            _repository.Save(category);
            return Ok(category);
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível salvar a categoria." });
         }
      }

      [HttpPut]
      [Route("{id:int}")]
      public ActionResult Put(int id, [FromBody]HerdCategory category)
      {
         if (id != category.Id)
            return NotFound(new { message = "Categoria não encontrada." });

         try
         {
            _repository.Update(category);
            return Ok(category);
         }
         catch (DbUpdateConcurrencyException)
         {
            return BadRequest(new { message = "Esta categoria já foi atualizada." });
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível atualizar a categoria." });
         }
      }

      [HttpDelete]
      [Route("{id:int}")]
      public ActionResult Delete(int id, [FromServices]DataContext context)
      {
         try
         {
            _repository.Delete(id);
            return Ok(new { message = "Categoria removida com sucesso." });
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível remover a categoria." });
         }
      }
   }
}