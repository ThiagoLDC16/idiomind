namespace Idiomind.Infrastructure.Identity.Context;

public class RequestContext(IHttpContextAccessor httpContextAccessor) : IRequestContext
{
    public Guid? UserId =>
        Guid.Parse(httpContextAccessor.HttpContext?.User?.FindFirstValue(AppClaimTypes.UserId) ?? string.Empty);
}