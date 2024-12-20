
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Quantum.Shootball
{
    public static class ShootballSessionManager
    {
        public struct SessionArgs
        {
            public string RoomName;
            public int NumOfPlayers;
            
        }
        
        static RealtimeClient Client;

        public static async void JoinMultiplayerSession(SessionArgs args, UnityAction<bool> callback)
        {
            var connectionArguments = new MatchmakingArguments
            {
                // The Photon application settings
                PhotonSettings = PhotonServerSettings.Global.AppSettings,
                //Will be configured as "EnterRoomArgs.RoomOptions.PlayerTtl" when creating a Photon room
                PlayerTtlInSeconds = 10,
                //Will be configured as "EnterRoomArgs.RoomOptions.EmptyRoomTtl" when creating a Photon room
                EmptyRoomTtlInSeconds = 10,
                // Will be configured as "EnterRoomArgs.RoomOptions.RoomName when creating a Photon room.
                RoomName = args.RoomName,
                // The maximum number of clients for the room, in this case we use the code-generated max possible players for the Quantum simulation
                MaxPlayers = args.NumOfPlayers,
                // Configure if the connect request can also create rooms or if it only tries to join
                CanOnlyJoin = false,
                // Custom room properties that are configured as "EnterRoomArgs.RoomOptions.CustomRoomProperties"
                //CustomProperties = customRoomProperties,
                // Async configuration that include TaskFactory and global cancellation support. If null then "AsyncConfig.Global" is used
                //AsyncConfig = customAsyncConfig,
                //Provide authentication values for the Photon server connection. Use this in conjunction with custom authentication. This field is created when "UserId" is set
                //AuthValues = customAuthValues,
                // The plugin to request from the Photon cloud
                PluginName = "QuantumPlugin",
                //Optional object to save and load reconnect information
                ReconnectInformation = new MatchmakingReconnectInformation(),
                //Optional Realtime lobby to use for matchmaking
                Lobby = new TypedLobby(),
                // This sets the AuthValues and should be replaced with the custom authentication
                UserId = Guid.NewGuid().ToString(),
            };

            Client = await MatchmakingExtensions.ConnectToRoomAsync(connectionArguments);

            Debug.Log("CLient:" + Client.CurrentRoom);

            if(Client == null || !Client.IsConnected)
            {
                // Something wrong
                callback.Invoke(false);
                return;
            }

            //
            // Start game
            //
            var sessionRunnerArguments = new SessionRunner.Arguments
            {
                // The runner factory is the glue between the Quantum.Runner and Unity
                RunnerFactory = QuantumRunnerUnityFactory.DefaultFactory,
                // Creates a default version of `QuantumGameStartParameters`
                GameParameters = QuantumRunnerUnityFactory.CreateGameParameters,
                // A secret user id that is for example used to reserved player slots to reconnect into a running session
                ClientId = Client.UserId,
                SessionConfig = QuantumDeterministicSessionConfigAsset.DefaultConfig,
                PlayerCount = 1,
                StartGameTimeoutInSeconds = 10,
                Communicator = new QuantumNetworkCommunicator(Client),

                // RuntimeConfig info
                RuntimeConfig = new RuntimeConfig()
                {
                    Map = ShootballConfigurationInfo.Map,
                    Seed = DateTime.Now.Millisecond,
                    SystemsConfig = ShootballConfigurationInfo.SystemConfig,
                    SimulationConfig = ShootballConfigurationInfo.SimulationConfig,
                    GameMode = ShootballConfigurationInfo.GameMode
                }
            };

            QuantumRunner runner = (QuantumRunner)await SessionRunner.StartAsync(sessionRunnerArguments);
            if(runner == null || !runner.Session.IsRunning)
            { 
                callback?.Invoke(false); 
            }
            else
            {
                callback?.Invoke(true);
            }
        }
    }
}
