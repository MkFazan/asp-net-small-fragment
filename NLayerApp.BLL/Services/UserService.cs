using AutoMapper;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Interfaces;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerApp.BLL.Infrastructure;

namespace NLayerApp.BLL.Services
{
    public class UserService : IBaseService<UserDTO>
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public UserDTO Get(string id)
        {
            try
            {
                var user = Database.Users.Get(id);
                if (user == null)
                    throw new OperationResult(false, "Object not found");

                return new UserDTO { UserName = user.UserName };
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IEnumerable<UserDTO> Find(Func<UserDTO, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Create(UserDTO userDTO)
        {
            try
            {
                User user =  new User 
                {   
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                    ConfirmPassword = userDTO.ConfirmPassword,
                };
                Database.Users.Create(user);
                var roles = Database.ApplicationRoles.Find(n => n.Name == userDTO.Role);
                Role role = null;
                if (roles.Count() != 0)
                {
                    role = roles.First();
                }
                else
                {
                    role = new Role { Name = userDTO.Role };
                    Database.ApplicationRoles.Create(role);
                }
                await Database.UserManager.AddToRoleAsync(user.Id, role.Name);
                Database.Commit();
                return new OperationResult(true, "Success");
            }
            catch (Exception exception)
            {
                Database.Rollback();
                throw exception;
            }
        }

        public async Task<OperationResult> Update(string id, UserDTO userDTO)
        {
            try
            {
                User user = new User
                {
                    UserName = userDTO.UserName
                };
                Database.Users.Update(id, user);
                Database.Commit();
                return new OperationResult(true, "Success");
            }
            catch (Exception exception)
            {
                Database.Rollback();
                throw exception;
            }
        }

        public async Task<OperationResult> Delete(string id)
        {
            try
            {
                Database.Users.Delete(id);
                Database.Commit();

                return new OperationResult(true, "Success");
            }
            catch (Exception exception)
            {
                Database.Rollback();
                throw exception;
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
