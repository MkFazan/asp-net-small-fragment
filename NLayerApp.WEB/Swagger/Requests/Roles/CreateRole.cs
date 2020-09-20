using NLayerApp.WEB.Models;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLayerApp.WEB.Swagger.Requests.Roles
{
    public class CreateRoleExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new RoleViewModel
                {
                    Name = "Admin"
                };
            }
        }
}