using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SARITASA.Lib
{
    public class ObjectResponse : IResponse
    {

        [JsonIgnore]
        public int __Id { get; set; }
        public virtual int StatusCode { get; set; } = StatusCodes.Status200OK;
    }
}
