using Bigstick.BuildingBlocks.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferreteria.GestionVentas.API.Configuration.ExecutionContext
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                if (_httpContextAccessor
                    .HttpContext?
                    .User != null)
                {
                    return _httpContextAccessor.HttpContext.User?.Identity?.Name;
                }

                throw new ApplicationException("User context is not available");
            }
        }

        public string UserName
        {
            get
            {
                if (_httpContextAccessor
                    .HttpContext?
                    .User != null)
                {
                    var claimUserName = _httpContextAccessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == "userName");
                    if (claimUserName != null) return claimUserName.Value;
                }
                return null;
            }
        }

        public string CodigoSap
        {
            get
            {
                if (_httpContextAccessor
                    .HttpContext?
                    .User != null)
                {
                    var claimssap = _httpContextAccessor.HttpContext.User?.Claims.FirstOrDefault(x=>x.Type== "sap_code");
                    if (claimssap != null) return claimssap.Value;
                }
                return null;
            }
        }

        public Guid CorrelationId
        {
            get
            {
                if (IsAvailable && _httpContextAccessor.HttpContext.Request.Headers.Keys.Any(
                    x => x == CorrelationMiddleware.CorrelationHeaderKey))
                {
                    return Guid.Parse(
                        _httpContextAccessor.HttpContext.Request.Headers[CorrelationMiddleware.CorrelationHeaderKey]);
                }

                throw new ApplicationException("Http context and correlation id is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
    }
}
