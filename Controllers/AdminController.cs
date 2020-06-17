using System.Collections.Generic;
using System.Linq;
using API.Mapper.Attributes;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        UserRepository repo = new UserRepository();

        #region GET Récupération de tous les User
        /// <summary>
        /// Get API/Admin
        /// </summary>
        /// <returns>List de tous les User (même inActive)</returns>
        [HttpGet]
        public IActionResult Get()
        {
          //  if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
           // {
                IEnumerable<UserModel> List = repo.GetAllAdmin().Select(User => User?.MapTo<UserModel>());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
           // }
           // else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un User by Id
        /// <summary>
        /// Get API/Admin/{id}
        /// </summary>
        /// <param name="id">id du User à récupérer</param>
        /// <returns>User avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           // if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
           // {
                UserModel Objet = repo.GetOneAdmin(id)?.MapTo<UserModel>();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
           // }
           // else return Unauthorized();
        }
        #endregion
    }
}
