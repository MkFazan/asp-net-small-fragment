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
using System.Collections.ObjectModel;

namespace NLayerApp.BLL.Services
{
    public class ProductService : IBaseService<ProductDTO>
    {
        IUnitOfWork Database { get; set; }

        public ProductService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            try
            {
                IEnumerable<Product> products = Database.Products.GetAll();
                IEnumerable<ProductDTO> productDTOs = this.MappProductToProductDTO(products);

                return productDTOs;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public ProductDTO Get(string id)
        {
            try
            {
                var product = Database.Products.Get(id);
                if (product == null)
                    throw new OperationResult(false, "Object not found");
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Property, PropertyDTO>()).CreateMapper();
                
                ProductDTO productDTO = new ProductDTO 
                { 
                    Id = product.Id,
                    Name = product.Name,
                    Properties = mapper.Map<IEnumerable<Property>, List<PropertyDTO>>(product.Properties)
                };

                return productDTO;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IEnumerable<ProductDTO> Find(Func<ProductDTO, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Create(ProductDTO productDTO)
        {
            try
            {
                Product product = new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = productDTO.Name
                };
                Database.Products.Create(product);
                Database.Commit();
                return new OperationResult(true, "Success");
            }
            catch (Exception exception)
            {
                Database.Rollback();
                throw exception;
            }
        }

        public async Task<OperationResult> Update(string id, ProductDTO productDTO)
        {
            try
            {
                Product product = new Product
                {
                    Name = productDTO.Name
                };
                Database.Products.Update(id, product);
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
                Database.Products.Delete(id);
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

        private ICollection<ProductDTO> MappProductToProductDTO(IEnumerable<Product> products)
        {
            ICollection<ProductDTO> productDTOs = new Collection<ProductDTO>(); ;
            var mapperProperty = new MapperConfiguration(cfg => cfg.CreateMap<Property, PropertyDTO>()).CreateMapper();

            foreach (Product product in products)
            {
                IEnumerable<Property> properties = product.Properties;
                var propertyDTOs = mapperProperty.Map<IEnumerable<Property>, List<PropertyDTO>>(properties);

                productDTOs.Add(new ProductDTO { 
                    Id = product.Id,
                    Name = product.Name,
                    Properties = propertyDTOs
                });
            }

            return productDTOs;
        }
    }
}
