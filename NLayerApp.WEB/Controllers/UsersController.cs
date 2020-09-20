using AutoMapper;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.BLL.Interfaces;
using NLayerApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace NLayerApp.WEB.Controllers
{
    public class UsersController : ApiController
    {
        IBaseService<UserDTO> userService;
        public UsersController(IBaseService<UserDTO> serv)
        {
            userService = serv;
        }

        // GET api/roles
        //[Authorize]
        public IEnumerable<UserDTO> Get()
        {
            try
            {
                return userService.GetAll();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // GET api/roles/1
        public UserDTO Get(string id)
        {
            try
            {
                return userService.Get(id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // POST api/roles
        public async Task<IHttpActionResult> Post([FromBody]UserViewModel userViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserDTO>()).CreateMapper();
                UserDTO userDTO = mapper.Map<UserViewModel, UserDTO>(userViewModel);
                OperationResult result = await userService.Create(userDTO);

                if (result.Status)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // PUT api/roles/5
        public async Task<IHttpActionResult> Put(string id, [FromBody]UserViewModel userViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserDTO>()).CreateMapper();
                UserDTO userDTO = mapper.Map<UserViewModel, UserDTO>(userViewModel);
                OperationResult result = await userService.Create(userDTO);

                if (result.Status)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // DELETE api/roles/5
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                OperationResult result = await userService.Delete(id);

                if (result.Status)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}