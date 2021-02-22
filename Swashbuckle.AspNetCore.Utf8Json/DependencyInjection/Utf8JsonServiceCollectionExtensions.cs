using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Utf8Json;
using Utf8Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Utf8JsonServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerGenUtf8JsonSupport(this IServiceCollection services)
        {
            return services.Replace(
                ServiceDescriptor.Transient<ISerializerDataContractResolver>((s) =>
                {
                    var serializerSettings = s.GetRequiredService<IJsonFormatterResolver>();

                    return new Utf8JsonDataContractResolver(serializerSettings);
                }));
        }
    }
}
