using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SuperAgenda.Api.Controllers;

public static class ControllerBaseExtensions
{
    public static int CurrentUserId(this ControllerBase controller)
    {
        var value = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(value!);
    }
}
