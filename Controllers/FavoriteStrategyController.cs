using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Services;
using Models;
using API.Helper;
using API.Mapper;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteStrategyController : ControllerBase
    {
        FavoriteStrategyRepository repo = new FavoriteStrategyRepository();

        #region POST Add d'un favori
        /// <summary>
        /// Post API/Favoris
        /// </summary>
        /// <param name="Favori">Favori à insérer</param>
        [HttpPost]
        public IActionResult Post(FavoriteStrategyModel Favori)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Favori == null || Favori.User.Id == 0 || Favori.Strategy.Id == 0) return BadRequest();
                else
                {
                    repo.Create(Favori.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de toutes les Favoris
        /// <summary>
        /// Get API/Favori
        /// </summary>
        /// <returns>List de tous les Favoris</returns>
        [HttpGet]
        public IActionResult Get()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<FavoriteStrategyModel> List = repo.GetAll().Select(Favori => Favori?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'une Favoris by Id
        /// <summary>
        /// Get API/Favori/{id}
        /// </summary>
        /// <param name="id">id du Favori à récupérer</param>
        /// <returns>Favori avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                FavoriteStrategyModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Favoris by UserId
        /// <summary>
        /// Get API/Favoris/?idUser={idUser}
        /// </summary>
        /// <param name="idUser">id de l'User pour lequel on veut la List des favoris</param>
        /// <returns>List de tous les Favoris</returns>
        [HttpGet("UserId={UserId}")]
        public IActionResult GetByUserId(int UserId)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<FavoriteStrategyModel> List = repo.GetAllByUserId(UserId).Select(Favori => Favori?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Favori by Id
        /// <summary>
        /// Delete API/Favori/{id}
        /// </summary>
        /// <param name="id">id du Favori à supprimer</param>
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

        #region PUT Update d'un Favori by Id
        /// <summary>
        /// Put API/Favori/{id}
        /// </summary>
        /// <param name="Favori">Favori à insérer</param>
        /// <param name="Id">Id du Favori à Updateier</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FavoriteStrategyModel Favori)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (Favori == null || Favori.User.Id == 0 || Favori.Strategy.Id == 0) return BadRequest();
                else
                {
                    repo.Update(id, Favori.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
