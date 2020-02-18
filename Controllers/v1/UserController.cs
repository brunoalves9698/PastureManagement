using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastureManagement.Data;
using PastureManagement.Models;
using PastureManagement.Repositories;
using PastureManagement.Services;
using PastureManagement.ViewModels.UserViewModels;

namespace PastureManagement.Controllers.v1
{
   [Route("v1/users")]
   public class UserController : ControllerBase
   {
      private readonly UserRepository _repository;

      public UserController(UserRepository repository)
      {
         _repository = repository;
      }

      [HttpGet]
      [Route("{id:int}")]
      [Authorize]
      public ActionResult<ListUserViewModel> GetById(int id)
      {
         var user = _repository.GetById(id);
         return Ok(user);
      }

      [HttpGet]
      [Route("")]
      [Authorize]
      public ActionResult<List<ListUserViewModel>> Get()
      {
         var users = _repository.Get();
         return Ok(users);
      }

      [HttpPost]
      [Route("login")]
      public ActionResult<dynamic> Login([FromBody]LoginViewModel loginViewModel)
      {
         var user = _repository.Authenticate(loginViewModel);
         if (user == null)
            return NotFound(new { messsage = "Usuário ou senha inválidos." });

         var token = TokenService.GenerateToken(user);
         return new
         {
            user = user.UserName,
            token = token
         };
      }

      [HttpPost]
      [Route("change-password/{id:int}")]
      [Authorize]
      public ActionResult ChangePassword(int id, [FromBody] ChangePasswordViewModel changePasswordViewModel)
      {
         if (id != changePasswordViewModel.Id)
            return NotFound(new { message = "Usuário não encontrado." });

         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var result = _repository.ChangePassword(changePasswordViewModel);
         if (!result)
            return NotFound(new { messsage = "Erro ao alterar senha." });

         return Ok(new { message = "Senha alterada com sucesso." });
      }

      [HttpPost]
      [Route("")]
      public ActionResult<ListUserViewModel> Post([FromBody]User user)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         try
         {
            _repository.Save(user);
            return Ok((ListUserViewModel)user);
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível salvar o usuário." });
         }
      }

      [HttpPut]
      [Route("{id:int}")]
      [Authorize]
      public ActionResult<ListUserViewModel> Put(int id, [FromBody]User user)
      {
         if (id != user.Id)
            return NotFound(new { message = "Usuário não encontrado." });

         ModelState.Remove("Password");
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         try
         {
            _repository.Update(user);
            return Ok((ListUserViewModel)user);
         }
         catch (DbUpdateConcurrencyException)
         {
            return BadRequest(new { message = "Este usuário já foi atualizado." });
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível atualizar o usuário." });
         }
      }

      [HttpDelete]
      [Route("{id:int}")]
      [Authorize]
      public ActionResult Delete(int id, [FromServices]DataContext context)
      {
         try
         {
            _repository.Delete(id);
            return Ok(new { message = "Usuário removido com sucesso." });
         }
         catch
         {
            return BadRequest(new { message = "Não foi possível remover o usuário." });
         }
      }
   }
}