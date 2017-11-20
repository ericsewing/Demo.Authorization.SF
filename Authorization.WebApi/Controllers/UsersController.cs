using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Authorization.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace Authorization.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"Jim Everett", "Rodney Peete", "Steve Young", "Tom Brady", "Bernie Kosar"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id)
        {
            var service = ActorProxy.Create<IUser>(new ActorId(id));
            return await service.GetInfoAsync(CancellationToken.None);
        }

        // POST api/values
        [HttpPost]
        public void Post(UserDto info)
        {
            var service = ActorProxy.Create<IUser>(new ActorId(info.Id));
            service.SetInfoAsync(info, CancellationToken.None);
        }

        [Route("{id}/permissions")]
        [HttpGet]
        public async Task<IList<UserPermission>> GetUserPermissions(int id)
        {
            var service = ActorProxy.Create<IUser>(new ActorId(id));
            return await service.GetPermissionsAsync(CancellationToken.None);
        }

        [Route("{id}/permissions")]
        [HttpPost]
        public void Post(int id, IList<UserPermission> permissions)
        {
            var service = ActorProxy.Create<IUser>(new ActorId(id));
            service.SetPermissionsAsync(permissions, CancellationToken.None);
        }
    }
}