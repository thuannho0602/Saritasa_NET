﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARITASA.Lib
{
    public class GenericResponse : IResponse
    {
        public static GenericResponse NoContent { get; } = new GenericResponse { StatusCode = StatusCodes.Status204NoContent };
        public static GenericResponse Created { get; } = new GenericResponse { StatusCode = StatusCodes.Status201Created };
        public static ErrorResponse NotFound { get; } = new ErrorResponse
        {
            StatusCode = StatusCodes.Status404NotFound,
            Details = new List<string>(new string[] { "notFound" })
        };

        public int StatusCode { get; set; }
    }
}
