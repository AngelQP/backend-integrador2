﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Response
{
    
    public class RequestResult<T> : RequestResult
    {
        public RequestResult(T data)
        {
            Data = data;
        }

        public RequestResult() { }

        public T Data { get; set; }
    }
}
