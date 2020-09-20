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
    //[Authorize(Roles = "Admin")]
    public class ProductsController : ApiController
    {
        IBaseService<ProductDTO> productService;
        public ProductsController(IBaseService<ProductDTO> serv)
        {
            productService = serv;
        }

        // GET api/products
        //[Authorize]
        public IEnumerable<ProductDTO> Get()
        {
            try
            {
                return productService.GetAll();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // GET api/products/{id}
        public ProductDTO Get(string id)
        {
            try
            {
                return productService.Get(id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // POST api/products
        public async Task<IHttpActionResult> Post(ProductViewModel productViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, ProductDTO>()).CreateMapper();
                ProductDTO product = mapper.Map<ProductViewModel, ProductDTO>(productViewModel);
                OperationResult result = await productService.Create(product);

                return Ok();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // PUT api/products/{id}
        public async Task<IHttpActionResult> Put(string id, [FromBody]ProductViewModel productViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, ProductDTO>()).CreateMapper();
                ProductDTO product = mapper.Map<ProductViewModel, ProductDTO>(productViewModel);

                OperationResult result = await productService.Update(id, product);

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

        // DELETE api/products/{id}
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                OperationResult result = await productService.Delete(id);
                return Ok();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}