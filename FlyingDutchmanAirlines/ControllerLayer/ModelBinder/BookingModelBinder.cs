using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.ControllerLayer.JsonData;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlyingDutchmanAirlines.ControllerLayer.ModelBinder
{
    public class BookingModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
                throw new ArgumentException();
            
            ReadResult result = await bindingContext.HttpContext.Request.BodyReader.ReadAsync();
            ReadOnlySequence<byte> buffer = result.Buffer;
            string body = Encoding.UTF8.GetString(buffer.FirstSpan);

            BookingData data = JsonSerializer.Deserialize<BookingData>(body); 

            bindingContext.Result = ModelBindingResult.Success(data);
        }
    }
}