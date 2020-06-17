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
    public class SkillController : ControllerBase
    {
        SkillRepository repo = new SkillRepository();

        #region POST Add d'un Skill
        /// <summary>
        /// Post API/Skill
        /// </summary>
        /// <param name="E">Skill à insérer</param>
        [HttpPost]
        public IActionResult Post(SkillModel Skill)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Skill == null || Skill.NameEN == null || Skill.NameFR == null || Skill.Classe.Id == 0) return BadRequest();
                else
                {
                    repo.Create(Skill.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Skills
        /// <summary>
        /// Get API/Skill
        /// </summary>
        /// <returns>List de tous les Skill</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<SkillModel> List = repo.GetAll().Select(Skill => Skill?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Skills by classeId
        /// <summary>
        /// Get API/Skill/?ClasseId={ClasseId}
        /// </summary>
        /// <param name="ClasseId"></param>
        /// <returns>List de tous les Skill</returns>
        [HttpGet("ClasseId={ClasseId}")]
        public IActionResult GetAll( int ClasseId)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<SkillModel> List = repo.GetAll(ClasseId).Select(Skill => Skill?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Ok(JsonConvert.SerializeObject(List));
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un Skill by Id
        /// <summary>
        /// Get API/Skill/{id}
        /// </summary>
        /// <param name="id">id du Skill à récupérer</param>
        /// <returns>Skill avec l'id correspondant</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                SkillModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Ok(JsonConvert.SerializeObject(Objet));
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Skill by Id
        /// <summary>
        /// Delete API/Skill/{id}
        /// </summary>
        /// <param name="id">id du Skill à supprimer</param>
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

        #region PUT Update d'un Skill by Id
        /// <summary>
        /// Put API/Skill/{id}
        /// </summary>
        /// <param name="Skill">Skill à insérer</param>
        /// <param name="Id">Id du Skill à Updateier</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, SkillModel Skill)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (Skill == null || Skill.NameEN == null || Skill.NameFR == null || Skill.Classe.Id == 0) return BadRequest();
                else
                {
                    repo.Update(id, Skill.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
