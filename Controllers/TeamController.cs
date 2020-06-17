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
    public class TeamController : ControllerBase
    {
        TeamRepository repo = new TeamRepository();

        #region POST Add d'une CharactersConfiguration
        /// <summary>
        /// Post API/Team
        /// </summary>
        /// <param name="E">CharactersConfiguration à insérer</param>
        [HttpPost("Post")]
        public IActionResult Post([FromBody] TeamModel Team)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Team == null || Team.CharactersConfiguration.Id == 0 || Team.Zone.Id == 0 || Team.User.Id == 0 || Team.TeamName == null) return BadRequest();
                else
                {
                    repo.Create(Team.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les CharactersConfigurations des Users
        /// <summary>
        /// Get API/Team
        /// </summary>
        /// <returns>List de toutes les CharactersConfigurations des Users</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<TeamModel> List = repo.GetAll().Select(Team => Team?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération des CharactersConfigurations d'un User by UserId
        /// <summary>
        /// Get API/Team/?UserId={UserId}
        /// </summary>
        /// <param name="UserId">id de l'User pour lequel on veut récupérer ses CharactersConfigurations</param>
        /// <returns>List de toutes les CharactersConfiguration de l'User ciblé</returns>
        [HttpGet("UserId={UserId}")]
        public IActionResult GetAll(int UserId)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<TeamModel> List = repo.GetAll(UserId).Select(Team => Team?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'une CharactersConfiguration by Id
        /// <summary>
        /// Get API/Team/{id}
        /// </summary>
        /// <param name="id">id de la CharactersConfiguration à récupérer</param>
        /// <returns>CharactersConfiguration avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                TeamModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'une CharactersConfiguration by Id
        /// <summary>
        /// Delete API/CharactersConfiguration/{id}
        /// </summary>
        /// <param name="id">id de la CharactersConfiguration à supprimer</param>
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

        #region PUT Update d'une CharactersConfiguration by Id
        /// <summary>
        /// Put API/Team/{id}
        /// </summary>
        /// <param name="Team">CharactersConfiguration à insérer</param>
        /// <param name="id">Id de la CharactersConfiguration à Updateier</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] TeamModel Team)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (Team == null || Team.CharactersConfiguration.Id == 0 || Team.Zone.Id == 0 || Team.User.Id == 0 || Team.TeamName == null) return BadRequest();
                else
                {
                    repo.Update(id, Team.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
