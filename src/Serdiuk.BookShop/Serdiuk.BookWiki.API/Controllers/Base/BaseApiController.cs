using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Serdiuk.API.Controllers.Base
{
    public class BaseApiController : ControllerBase
    {
        internal string UserId => !User.Identity.IsAuthenticated
           ? string.Empty
           : User.FindFirst(ClaimTypes.NameIdentifier).Value;
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual void HandleResult(ResultBase result)
        {
            if (result.IsFailed)
                throw new Exception(String.Join(" ,", result.Reasons.Select(x => x.Message)));
        }
    }
}
