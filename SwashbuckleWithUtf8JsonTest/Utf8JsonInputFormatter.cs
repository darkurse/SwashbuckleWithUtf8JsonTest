using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;

namespace SwashbuckleWithUtf8JsonTest
{
    internal sealed class Utf8JsonInputFormatter : TextInputFormatter
    {
        private readonly IJsonFormatterResolver _resolver;

        public Utf8JsonInputFormatter() : this(null) { }
        public Utf8JsonInputFormatter(IJsonFormatterResolver resolver)
        {
            _resolver = resolver ?? JsonSerializer.DefaultResolver;

            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);

            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        }

        public async override Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Body.CanSeek && request.Body.Length == 0)
                return await InputFormatterResult.NoValueAsync();

            var result = await JsonSerializer.NonGeneric.DeserializeAsync(context.ModelType, request.Body, _resolver);
            return await InputFormatterResult.SuccessAsync(result);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            return ReadAsync(context);
        }
    }
}
