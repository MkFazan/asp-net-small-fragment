using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.BLL.Interfaces;
using NLayerApp.WEB.Models;
using NLayerApp.WEB.Swagger.Requests.Roles;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace NLayerApp.WEB.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RolesController : BaseController
    {
        IBaseService<RoleDTO> roleService;
        public RolesController(IBaseService<RoleDTO> serv)
        {
            roleService = serv;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <remarks>
        /// Gets a list of all roles
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If the item is null</response>          
        [ResponseType(typeof(IEnumerable<RoleViewModel>))]
        public IEnumerable<RoleViewModel> Get()
        {
            try
            {
                IEnumerable<RoleDTO> roleDTOs = roleService.GetAll();
                var mapperRole = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleViewModel>()).CreateMapper();
                IEnumerable<RoleViewModel> roleViews = mapperRole.Map<IEnumerable<RoleDTO>, IEnumerable<RoleViewModel>>(roleDTOs);

                return roleViews;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Get role for id
        /// </summary>
        /// <remarks>
        /// Gets a role for id
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If the item is null</response>        
        public RoleViewModel Get(string id)
        {
            try
            {
                var mapperRole = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleViewModel>()).CreateMapper();
                RoleViewModel roleView = mapperRole.Map<RoleDTO, RoleViewModel>(roleService.Get(id));

                return roleView;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Creates an Role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/roles
        ///     {        
        ///       "name": "Admin",
        ///     }
        /// </remarks>
        /// <param name="roleViewModel"></param>
        /// <returns>A newly created role</returns>
        /// <response code="201">Created new role</response>
        /// <response code="400">If the item is null</response>      
        [SwaggerRequestExample(typeof(RoleViewModel), typeof(CreateRoleExample))]
        public async Task<IHttpActionResult> Post([Microsoft.AspNetCore.Mvc.FromBody]RoleViewModel roleViewModel)
        {
            try
            {
                if (roleViewModel == null)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleViewModel, RoleDTO>()).CreateMapper();
                RoleDTO role = mapper.Map<RoleViewModel, RoleDTO>(roleViewModel);
                OperationResult result = await roleService.Create(role);

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

        /// <summary>
        /// Updates an Role for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put api/roles
        ///     {        
        ///       "name": "Admin",
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="roleViewModel"></param>
        /// <returns>A newly created role</returns>
        /// <response code="201">Created new role</response>
        /// <response code="400">If the item is null</response>      
        public async Task<IHttpActionResult> Put(string id, [Microsoft.AspNetCore.Mvc.FromBody]RoleViewModel roleViewModel)
        {
            try
            {
                if (roleViewModel == null)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleViewModel, RoleDTO>()).CreateMapper();
                RoleDTO role = mapper.Map<RoleViewModel, RoleDTO>(roleViewModel);
                OperationResult result = await roleService.Update(id, role);

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

        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                OperationResult result = await roleService.Delete(id);
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