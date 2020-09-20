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
    public class RoleService : IBaseService<RoleDTO>
    {
        IUnitOfWork Database { get; set; }

        public RoleService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Role>, List<RoleDTO>>(Database.ApplicationRoles.GetAll());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public RoleDTO Get(string id)
        {
            try
            {
                var role = Database.ApplicationRoles.Get(id);
                if (role == null)
                    throw new OperationResult(false, "Object not found");

                return new RoleDTO { Name = role.Name };
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IEnumerable<RoleDTO> Find(Func<RoleDTO, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Create(RoleDTO roleDTO)
        {
            try
            {
                Role role = new Role
                {
                    Name = roleDTO.Name
                };
                Database.ApplicationRoles.Create(role);
                Database.Commit();
                return new OperationResult(true, "Success");
            }
            catch (Exception exception)
            {
                Database.Rollback();
                throw exception;
            }
        }

        public async Task<OperationResult> Update(string id, RoleDTO roleDTO)
        {
            try
            {
                Role role = new Role
                {
                    Name = roleDTO.Name
                };
                Database.ApplicationRoles.Update(id, role);
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
                Database.ApplicationRoles.Delete(id);
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
