using System;
using Business.Abstract;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Aspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IClaimService _claimService;
        private IRoomService _roomService;
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _claimService = ServiceTool.ServiceProvider.GetService<IClaimService>();
            _roomService = ServiceTool.ServiceProvider.GetService<IRoomService>();
        }

        public override void OnBefore(IInvocation invocation)
        {
            var room = _roomService.GetCurrentRoom().Data;
            var claims = _claimService.GetUserClaims(room.Id);
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
