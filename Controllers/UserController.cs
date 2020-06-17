using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Services;
using Models;
using API.Helper;
using API.Mapper.Attributes;
using DAL.Entities.Entities;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository repo = new UserRepository();

        #region POST Add d'un User
        /// <summary>
        /// Post API/User
        /// </summary>
        /// <param name="E">User à insérer</param>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel User)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (User == null || User.Mail == null || User.Password == null || User.Username == null) return BadRequest();
                else
                {
                    repo.Create(User.MapTo<UserEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Users
        /// <summary>
        /// Get API/User
        /// </summary>
        /// <returns>List de tous les User</returns>
        [HttpGet("")]
        public IActionResult Get()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<UserModel> List = repo.GetAllAdmin().Select(User => User?.MapTo<UserModel>());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un User by Id
        /// <summary>
        /// Get API/User/{id}
        /// </summary>
        /// <param name="id">id du User à récupérer</param>
        /// <returns>User avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                UserModel Objet = repo.GetOne(id)?.MapTo<UserModel>();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un User by Username
        /// <summary>
        /// Get API/User/?Username={Username}
        /// </summary>
        /// <param name="Username">Username de l'User à récupérer</param>
        /// <returns>User avec le Username correspondant</returns>
        [HttpGet("Username={Username}")]
        public IActionResult GetByUsername(string Username)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                UserModel Objet = repo.GetOneByUsername(Username)?.MapTo<UserModel>();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un User by mail
        /// <summary>
        /// Get API/User/?mail={mail}
        /// </summary>
        /// <param name="mail">mail de l'User à récupérer</param>
        /// <returns>User avec le mail correspondant</returns>
        [HttpGet("mail={mail}")]
        public IActionResult GetByMail(string mail)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                UserModel Objet = repo.GetOneByMail(mail)?.MapTo<UserModel>();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un User by Id
        /// <summary>
        /// Delete API/User/{id}
        /// </summary>
        /// <param name="id">id de l'User à supprimer</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else
                {
                    repo.Delete(id);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region PUT Update d'un User by Id
        /// <summary>
        /// Put API/User/{id}
        /// </summary>
        /// <param name="User">User à insérer</param>
        /// <param name="Id">Id du User à Updateier</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] UserModel User)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOneAdmin(id) == null) return NotFound();
                else if (User == null || User.Mail == null || User.Username == null || (User.Role != "User" && User.Role != "Admin")) return BadRequest();
                else
                {
                    repo.Update(id, User.MapTo<UserEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
