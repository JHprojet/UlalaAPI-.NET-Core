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
    public class BossesPerZoneController : ControllerBase
    {
        BossesPerZoneRepository repo = new BossesPerZoneRepository();

        #region POST Add d'un BossZone
        /// <summary>
        /// Post API/BossZone
        /// </summary>
        /// <param name="BossZone">BossZone à insérer</param>
        [HttpPost]
        public IActionResult Post(BossesZoneModel BossZone)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (BossZone == null || BossZone.Boss.Id == 0 || BossZone.Zone.Id == 0) return BadRequest();
                else
                {
                    repo.Create(BossZone.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de toutes les BossZone
        /// <summary>
        /// Get API/BossZone
        /// </summary>
        /// <returns>List de toutes les BossZone</returns>
        [HttpGet]
        public IActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<BossesZoneModel> List = repo.GetAll().Select(BossZone => BossZone?.ToModel());
                return Ok(JsonConvert.SerializeObject(List)) ;
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'une BossZone by Id
        /// <summary>
        /// Get API/BossZone/{id}
        /// </summary>
        /// <param name="id">id du BossZone à récupérer</param>
        /// <returns>BossZone avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                BossesZoneModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un BossZone by Id
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

        #region PUT Update d'un BossZone by Id
        /// <summary>
        /// Put API/BossZone/{id}
        /// </summary>
        /// <param name="BossZone">BossZone à insérer</param>
        /// <param name="id">Id du BossZone à Updateier</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BossesZoneModel BossZone)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (BossZone == null || BossZone.Boss.Id == 0 || BossZone.Zone.Id == 0) return BadRequest();
                else
                {
                    repo.Update(id, BossZone.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
