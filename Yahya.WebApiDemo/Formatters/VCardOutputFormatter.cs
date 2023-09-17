using System.Text;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Yahya.WebApiDemo.Models;

namespace Yahya.WebApiDemo.Formatters
{
    public class VCardOutputFormatter : TextOutputFormatter
    {
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        private static void FormatVcard(StringBuilder stringBuilder, ContactModel contactModel)
        {
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine("Version:2.1");
            stringBuilder.AppendLine($"N:{contactModel.LastName};{contactModel.FirstName}");
            stringBuilder.AppendLine($"N:{contactModel.FirstName};{contactModel.LastName}");
            stringBuilder.AppendLine($"UID:{contactModel.Id}\r\n");
            stringBuilder.AppendLine("END:VCARD");

        }
        protected override bool CanWriteType(Type? type)
        {
            if (typeof(ContactModel).IsAssignableFrom(type)|| typeof(List<ContactModel>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            else
            {
                return false;
            }
        }
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var stringBuilder = new StringBuilder();
            if (context.Object is List<ContactModel>)
            {
                foreach (ContactModel model in context.Object as List<ContactModel>)
                {
                    FormatVcard(stringBuilder, model);
                }
            }
            else
            {
                var contact1 = context.Object as ContactModel;
                FormatVcard(stringBuilder, contact1);
            }

            return response.WriteAsync(stringBuilder.ToString());
        }
    }
}
