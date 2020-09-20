using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISchemaFilter = Swashbuckle.Swagger.ISchemaFilter;

namespace NLayerApp.WEB.Swagger
{
    public class ApplyModelNameFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            schema.title = type.Name;
        }
    }
}