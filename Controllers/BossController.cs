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
    public class BossController : ControllerBase
    {
        BossRepository repo = new BossRepository();

        #region POST Add d'un Boss
        /// <summary>
        /// Post API/Boss
        /// </summary>
        /// <param name="Boss">Boss à insérer</param>
        [HttpPost]
        public IActionResult Post([FromBody] BossModel Boss)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Boss == null || Boss.NameEN == null || Boss.NameFR == null) return BadRequest();
                else
                {
                    repo.Create(Boss.MapTo<BossEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les boss
        /// <summary>
        /// Get API/Boss
        /// </summary>
        /// <returns>List de tous les boss</returns>
        [HttpGet]
        public IActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<BossModel> List = repo.GetAll().Select(Boss => Boss?.MapTo<BossModel>());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un Boss by Id
        /// <summary>
        /// Get API/Boss/{id}
        /// </summary>
        /// <param name="id">id du Boss à récupérer</param>
        /// <returns>Boss avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                BossModel Objet = repo.GetOne(id)?.MapTo<BossModel>();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Boss by Id
        /// <summary>
        /// Delete API/Boss/{id}
        /// </summary>
        /// <param name="id">id du Boss à supprimer</param>
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

        #region PUT Update d'un Boss by Id
        /// <summary>
        /// Put API/Boss/{id}
        /// </summary>
        /// <param name="Boss">Boss à insérer</param>
        /// <param name="id">Id du Boss à Updateier</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BossModel Boss)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (Boss == null || Boss.NameEN == null || Boss.NameFR == null) return BadRequest();
                else
                {
                    repo.Update(id, Boss.MapTo<BossEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
