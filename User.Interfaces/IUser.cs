using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace Authorization.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IUser : IActor
    {
        Task<UserDto> GetInfoAsync(CancellationToken cancellationToken);
        Task SetInfoAsync(UserDto info, CancellationToken cancellationToken);
        Task<IList<UserPermission>> GetPermissionsAsync(CancellationToken cancellationToken);
        Task SetPermissionsAsync(IList<UserPermission> permissions, CancellationToken cancellationToken);
    }

    public class UserPermission
    {
        public string Name { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
