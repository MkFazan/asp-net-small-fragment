using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.DTO;
using NLayerApp.WEB.Models;
using AutoMapper;
using NLayerApp.BLL.Infrastructure;
using System.Dynamic;

namespace NLayerApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        IBaseService<UserDTO> userService;
        IBaseService<RoleDTO> roleService;
        public HomeController(IBaseService<UserDTO> userServ, IBaseService<RoleDTO> roleServ)
        {
            userService = userServ;
            roleService = roleServ;
        }
        public ActionResult Index()
        {
            IEnumerable<UserDTO> userDtos = userService.GetAll();
            var mapperUsers = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            var users = mapperUsers.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDtos);

            IEnumerable<RoleDTO> roleDtos = roleService.GetAll();
            var mapperRoles = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleViewModel>()).CreateMapper();
            var roles = mapperRoles.Map<IEnumerable<RoleDTO>, List<RoleViewModel>>(roleDtos);

            ViewBag.Title = "Index page";
            //IndexDataViewModel mymodel = new IndexDataViewModel();
            //mymodel.Users = users;
            //mymodel.Roles = roles;

            //dynamic mymodel = new ExpandoObject();
            //mymodel.Users = users;
            //mymodel.Roles = roles;

            //ViewBag.Users = users;
            //ViewBag.Roles = roles;
            //var tupleModel = new Tuple<IEnumerable<UserViewModel>, IEnumerable<RoleViewModel>>(users, roles);
            ViewBag.Message = "Helo world!";
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            base.Dispose(disposing);
        }
    }
}