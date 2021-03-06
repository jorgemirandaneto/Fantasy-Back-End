﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FantasyServer.Dao;
using FantasyServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Fantasy_server.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {        
        [HttpPost,Route("login")]
        public IActionResult Login(
            [FromBody]User usuario,
            [FromServices]UserDao usersDao)
        {
            bool credenciaisValidas = true;
            
            if (usuario != null)
            {
                var usuarioBase = usersDao.find(usuario);
                credenciaisValidas = (usuarioBase != null &&
                    usuario.nome == usuarioBase.nome &&
                    usuario.senha == usuarioBase.senha);
            }

            if (credenciaisValidas)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}