﻿using Microsoft.AspNetCore.Http;

namespace ParanaBank.Application.Models
{
    public class ResultCommand
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public static ResultCommand NotFound(string message)
        {
            return CreateResponse(StatusCodes.Status404NotFound, message);
        }
        public static ResultCommand Error()
        {
            return CreateResponse(StatusCodes.Status500InternalServerError);
        }
        public static ResultCommand Ok(string message)
        {
            return CreateResponse(StatusCodes.Status200OK, message);
        }
        public static ResultCommand Ok()
        {
            return CreateResponse(StatusCodes.Status200OK);
        }

        public static ResultCommand CreateResponse(int status, string message)
        {
            return new ResultCommand { Status = status, Message = message };
        }
        public static ResultCommand CreateResponse(int status)
        {
            return new ResultCommand { Status = status };
        }
    }
}
