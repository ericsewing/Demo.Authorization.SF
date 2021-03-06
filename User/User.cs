﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Authorization.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace User
{
    /// <remarks>
    ///     This class represents an actor.
    ///     Every ActorID maps to an instance of this class.
    ///     The StatePersistence attribute determines persistence and replication of actor state:
    ///     - Persisted: State is written to disk and replicated.
    ///     - Volatile: State is kept in memory only and replicated.
    ///     - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class User : Actor, IUser
    {
        /// <summary>
        ///     Initializes a new instance of User
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public User(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public string UserInformation => "UserInformation";

        public string UserPermissions => "UserPermissions";

        public Task<UserDto> GetInfoAsync(CancellationToken cancellationToken)
        {
            return StateManager.GetStateAsync<UserDto>(UserInformation, cancellationToken);
        }

        public Task SetInfoAsync(UserDto info, CancellationToken cancellationToken)
        {
            return StateManager.AddStateAsync(UserInformation, info, cancellationToken);
        }

        public Task<IList<UserPermission>> GetPermissionsAsync(CancellationToken cancellationToken)
        {
            return StateManager.GetStateAsync<IList<UserPermission>>(UserPermissions, cancellationToken);
        }

        public Task SetPermissionsAsync(IList<UserPermission> permissions, CancellationToken cancellationToken)
        {
            return StateManager.AddStateAsync(UserPermissions, permissions, cancellationToken);
        }

        /// <summary>
        ///     This method is called whenever an actor is activated.
        ///     An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            return StateManager.TryAddStateAsync("count", 0);
        }
    }
}