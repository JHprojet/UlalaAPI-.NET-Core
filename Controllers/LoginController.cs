using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Services;
using Models;
using API.Helper;
using DAL.Entities.Entities;
using JwtToolBox;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UserRepository repo = new UserRepository();

        #region GET Anonyme Token
        /// <summary>
        /// Post API/Login
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpGet]
        public IActionResult Get()
        {
            //if (HttpContext.Request.Path.ToUriComponent().Contains("localhost:4200"))
            //{
                UserEntity U = new UserEntity();
                U.Role = "Anonymous";
                JWTService jwt = new JWTService("FZeDfgPkyXaDFyMwQfSbIoJhF", "localhost:4200", "localhost:4200");
                string token = jwt.Encode(U);
                return Ok(JsonConvert.SerializeObject(token));
            //}
            //else return Unauthorized();
        }
        #endregion

        #region POST Vérification couple Username-password
        /// <summary>
        /// Post API/Login
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel User)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (User == null || User.Password == null || User.Username == null) return BadRequest();
                else
                {
                    if (repo.Check(User.Username, User.Password))
                    {
                        UserEntity U = repo.GetOneByUsername(User.Username);
                        JWTService jwt = new JWTService("FZeDfgPkyXaDFyMwQfSbIoJhF", "localhost:4200", "localhost:4200");
                        string token = jwt.Encode(U);
                        return Ok(JsonConvert.SerializeObject(token));
                    }
                    else return BadRequest();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Verification Token d'activation
        /// <summary>
        /// Post API/Login/{Id}/?Token={Token}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] string Token)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Token == null) return BadRequest();
                else
                {
                    if (repo.UpdateToken(id, Token)) return Ok();
                    else return BadRequest();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Renvoi Token Activation
        /// <summary>
        /// Post API/Login/?IdU={Id}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost("IdU={IdU}")]
        public IActionResult Post(int IdU)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(IdU) == null) return BadRequest();
                else
                {
                    repo.RenvoiToken(IdU);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Changement Mot de passe
        /// <summary>
        /// Post API/Login/?IdUser={Id}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost("IdUser={IdUser}")]
        public IActionResult PostPass(int IdUser, [FromBody] string NewPassword)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(IdUser) == null || NewPassword == "") return BadRequest();
                else
                {
                    repo.UpdatePassword(IdUser, NewPassword);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Nouveau Mot de passe
        /// <summary>
        /// Post API/Login/?Mail={Mail}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost("Mail={Mail}")]
        public IActionResult PostPass(string Mail)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOneByMail(Mail) == null || Mail == "") return BadRequest();
                else
                {
                    repo.NouveauPassword(Mail);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Récupération Username
        /// <summary>
        /// Post API/Login/?MailforUsername={MailforUsername}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost("MailforUsername={MailforUsername}")]
        public IActionResult PostUsername( string MailforUsername)
        {
            if ((new[] { "Admin", "User", "Anonymous" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOneByMail(MailforUsername) == null || MailforUsername == "") return BadRequest();
                else
                {
                    repo.RetrieveUsername(MailforUsername);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
