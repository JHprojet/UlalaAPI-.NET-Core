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
    public class StrategyController : ControllerBase
    {
        StrategyRepository repo = new StrategyRepository();

        #region POST Add d'un Strategy
        /// <summary>
        /// Post API/Strategy
        /// </summary>
        /// <param name="Strategy">Strategy à insérer</param>
        [HttpPost]
        public IActionResult Post([FromBody] StrategyModel Strategy)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Strategy == null || Strategy.ImagePath1 == null || Strategy.ImagePath2 == null || Strategy.ImagePath3 == null || Strategy.ImagePath4 == null || Strategy.CharactersConfiguration.Id == 0 || Strategy.User.Id == 0 || Strategy.BossZone.Id == 0) return BadRequest();
                else
                {
                    repo.Create(Strategy.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Strategys
        /// <summary>
        /// Get API/Strategy
        /// </summary>
        /// <returns>List de toutes les Strategys</returns>
        [HttpGet]
        public IActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<StrategyModel> List = repo.GetAll().Select(Strategy => Strategy?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un Strategy by Id
        /// <summary>
        /// Get API/Strategy/{id}
        /// </summary>
        /// <param name="id">id de l'Strategy à récupérer</param>
        /// <returns>Strategy avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                StrategyModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Strategy by Id
        /// <summary>
        /// Delete API/Strategy/{id}
        /// </summary>
        /// <param name="id">id de l'Strategy à supprimer</param>
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

        #region PUT Update d'un Strategy by Id
        /// <summary>
        /// Put API/Strategy/{id}
        /// </summary>
        /// <param name="Strategy">Strategy à insérer</param>
        /// <param name="id">Id de l'Strategy à Updateier</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StrategyModel Strategy)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (Strategy == null || Strategy.ImagePath1 == null || Strategy.ImagePath2 == null || Strategy.ImagePath3 == null || Strategy.ImagePath4 == null || Strategy.CharactersConfiguration.Id == 0 || Strategy.User.Id == 0 || Strategy.BossZone.Id == 0) return BadRequest();
                else
                {
                    repo.Update(id, Strategy.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Strategys by BossZone, User et Classes
        /// <summary>
        /// Get API/Strategy/?U={U}&BZ={BZ}&C1={C1}2&C2={C2}&C3={C3}&C4={C4}
        /// </summary>
        /// <returns>List de toutes les Strategys correspondant à la recherche</returns>
        [HttpGet("Custom")]
        public IActionResult Get(int? U, int? BZ, int? C1, int? C2, int? C3, int? C4)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<StrategyModel> List = repo.GetAllByInfos(U, BZ, C1, C2, C3, C4).Select(Strategy => Strategy.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion
    }
}
