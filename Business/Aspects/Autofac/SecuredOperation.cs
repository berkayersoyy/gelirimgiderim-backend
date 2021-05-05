using System;
using System.Linq;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Aspects.Autofac
{
  public class SecuredOperation : MethodInterception
  {
    private string[] _roles;
    private IHttpContextAccessor _httpContextAccessor;
    private IClaimService _claimService;
    private IRoomService _roomService;
    public SecuredOperation(string roles)
    {
      _roles = roles.Split(',');
      _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
      _claimService = ServiceTool.ServiceProvider.GetService<IClaimService>();
      _roomService = ServiceTool.ServiceProvider.GetService<IRoomService>();
    }

    public override void OnBefore(IInvocation invocation)
    {
      var roomId = _httpContextAccessor.HttpContext.Items["currentRoom"].ToString();
      var room = _roomService.Get(roomId);
      var claims = _claimService.GetUsersClaims(room.Data);
      foreach (var role in _roles)
      {
        foreach (var claim in claims.Data)
        {
          if (claim.ClaimProperties.Contains(role))
          {
            return;
          }
        }
      }
      throw new Exception(Messages.AuthorizationDenied);
    }
  }
}
