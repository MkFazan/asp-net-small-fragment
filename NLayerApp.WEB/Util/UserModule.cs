using Ninject.Modules;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.Services;

namespace NLayerApp.WEB.Util
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBaseService<UserDTO>>().To<UserService>();
        }
    }
}