using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.Services;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Repositories;
using NLayerApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NLayerApp.WEB.Controllers
{
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        IBaseService<UserDTO> userService;
        public AccountController(IBaseService<UserDTO> serv)
        {
            userService = serv;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<RegisterModel, UserDTO>()).CreateMapper();
                    UserDTO userDTO = mapperUser.Map<RegisterModel, UserDTO>(registerModel);
                    await userService.Create(userDTO);

                    return Ok();
                } else
                {
                    return BadRequest(ModelState);
                }   
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}