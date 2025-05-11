using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application
{
    public class BusinessApplicationRuleValidationException: Exception
    {
        public BusinessApplicationRuleValidationException(string message, string details) 
        {
            Details = string.IsNullOrWhiteSpace(details)? message: details;
        }
        public string Details { get; }

    }
}
