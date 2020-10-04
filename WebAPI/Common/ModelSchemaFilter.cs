using Application.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Common
{
    public class ModelSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(ProblemDetails))
            {
                schema.Example = new OpenApiObject
                {
                    ["StatusCode"] = new OpenApiInteger(400),
                    ["Message"] = new OpenApiString("One or more validations failed"),
                    ["Errors"] = new OpenApiArray()
                    {
                        new OpenApiObject()
                        {
                            ["FieldName"] = new OpenApiString("LastName"),
                            ["Message"] = new OpenApiString("Last Name must be Alphabetic characters"),
                        }
                    },
                };
            }

            if (context.Type == typeof(ClientListDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["fullName"] = new OpenApiString("James Adebayo"),
                    ["code"] = new OpenApiString("GF564"),
                    ["dateRegistered"] = new OpenApiString("Oct 04, 2020"),
                };
            }

            if (context.Type == typeof(ClientDetailDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["fullName"] = new OpenApiString("James Adebayo"),
                    ["code"] = new OpenApiString("GF564"),
                    ["dateRegistered"] = new OpenApiString("Oct 04, 2020"),
                    ["matters"] = new OpenApiArray()
                    {
                        new OpenApiObject
                        {
                            ["title"] = new OpenApiString("Dawn of a new Horizon"),
                            ["amount"] = new OpenApiInteger(20),
                            ["code"] = new OpenApiString("MD487"),
                        }
                    },
                };
            }

            if (context.Type == typeof(CreateClientDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["firstName"] = new OpenApiString("James"),
                    ["lastName"] = new OpenApiString("Adebayo"),
                    ["code"] = new OpenApiString("GF564"),
                };
            }

            if (context.Type == typeof(UpdateClientDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["firstName"] = new OpenApiString("John"),
                    ["lastName"] = new OpenApiString("Adebayo"),
                };
            }

            if (context.Type == typeof(CreateMatterDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["title"] = new OpenApiString("Dawn of a new Horizon"),
                    ["code"] = new OpenApiString("MD487"),
                    ["amount"] = new OpenApiInteger(20),
                };
            }

            if (context.Type == typeof(MatterListDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["title"] = new OpenApiString("Dawn of a new Horizon"),
                    ["code"] = new OpenApiString("MD487"),
                    ["amount"] = new OpenApiInteger(20),
                };
            }

            if (context.Type == typeof(UpdateMatterDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["title"] = new OpenApiString("Rise of a new Horizon"),
                    ["amount"] = new OpenApiInteger(10),
                };
            }
        }
    }
}
