﻿using Ninject.Modules;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLayerApp.WEB.Util
{
    public class ProductModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBaseService<ProductDTO>>().To<ProductService>();
        }
    }
}