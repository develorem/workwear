using Microsoft.AspNetCore.Mvc.Formatters;
using System.IO;
using System.Threading.Tasks;

namespace WorkWearApi.Services
{
    /// <summary>
    /// Allows support for plain strings passed in controller actions as [FromBody]
    /// </summary>
    public class TextPlainInputFormatter : InputFormatter
    {
        private const string _contentType = "text/plain";

        public TextPlainInputFormatter()
        {
            SupportedMediaTypes.Add(_contentType);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            using (var reader = new StreamReader(request.Body))
            {
                var content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }

        public override bool CanRead(InputFormatterContext context)
        {
            var contentType = context.HttpContext.Request.ContentType;
            return contentType.StartsWith(_contentType);
        }
    }
}
