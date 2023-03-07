using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBank.Application.Models
{
    public class ResultCommand
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public static ResultCommand NotFound()
        {
            return CreateResponse(StatusCodes.Status404NotFound);
        }
        public static ResultCommand Error()
        {
            return CreateResponse(StatusCodes.Status500InternalServerError);
        }
        public static ResultCommand Ok()
        {
            return CreateResponse(StatusCodes.Status200OK);
        }
        public static ResultCommand CreateResponse(int status)
        {
            return new ResultCommand { Status = status };
        }
    }
}
