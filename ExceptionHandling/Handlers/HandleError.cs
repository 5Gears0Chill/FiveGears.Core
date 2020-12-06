using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FiveGears.Core.ExceptionHandling.Handlers
{
    public class HandleError
    {
        public static Task WithDefaultJsonConverter<T>(HttpContext context, T toSerialse)
        {
            return context.Response.WriteAsync(JsonConvert.SerializeObject(toSerialse));
        }
    }
}
