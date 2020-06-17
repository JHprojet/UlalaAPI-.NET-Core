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
    public class ToyController : ControllerBase
    {
        ToyRepository repo = new ToyRepository();

        #region POST Add d'un Toy
        /// <summary>
        /// Post API/Toy
        /// </summary>
        /// <param name="E">Toy à insérer</param>
        [HttpPost]
        public IActionResult Post(ToyModel Toy)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Toy == null || Toy.ImagePath == null || Toy.NameFR == null || Toy.NameEN == null) return BadRequest();
                else
                {
                    repo.Create(Toy.MapTo<ToyEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Toys
        /// <summary>
        /// Get API/Toy
        /// </summary>
        /// <returns>List de tous les Toys</returns>
        [HttpGet]
        public IActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<ToyModel> List = repo.GetAll().Select(Toy => Toy?.MapTo<ToyModel>());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un Toy by Id
        /// <summary>
        /// Get API/Toy/{id}
        /// </summary>
        /// <param name="id">id du Toy à récupérer</param>
        /// <returns>Toy avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                ToyModel Objet = repo.GetOne(id)?.MapTo<ToyModel>();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Toy by Id
        /// <summary>
        /// Delete API/Toy/{id}
        /// </summary>
        /// <param name="id">id du Toy à supprimer</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
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

        #region PUT Update d'un Toy by Id
        /// <summary>
        /// Put API/Toy/{id}
        /// </summary>
        /// <param name="Toy">Toy à insérer</param>
        /// <param name="Id">Id du Toy à Updateier</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ToyModel Toy)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) != null) return NotFound();
                else if (Toy == null || Toy.ImagePath == null || Toy.NameFR == null || Toy.NameEN == null) return BadRequest();
                else
                {
                    repo.Update(id, Toy.MapTo<ToyEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
