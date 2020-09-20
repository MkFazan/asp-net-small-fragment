using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NLayerApp.WEB.Models
{
    public class RoleViewModel
    {
        /// <summary>  
        /// Role Id  
        /// </summary> 
        [Display(Name = "Id")]
        public string Id { get; set; }

        /// <summary>  
        /// Role Name  
        /// </summary> 
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,256}$", ErrorMessage = "Only letters of the Latin alphabet are allowed")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}