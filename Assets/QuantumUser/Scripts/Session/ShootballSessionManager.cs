
using Photon.Deterministic.Protocol;
using Photon.Realtime;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Quantum.Shootball
{
    public class ShootballSessionManager: Singleton<ShootballSessionManager>
    {
        
        public RealtimeClient Client { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
//#if UNITY_EDITOR
            // Move this configuration where player choose the type of game
            ShootballSessionInfo.Map = Utility.GetAssetRef<Map>("Shootball/Maps/DefaultArenaMap");
            ShootballSessionInfo.SimulationConfig = Utility.GetAssetRef<SimulationConfig>("Shootball/Config/ShootballSimulationConfig");
            ShootballSessionInfo.SystemsConfig = Utility.GetAssetRef<SystemsConfig>("Shootball/Config/ShootballSystemConfig");
          
            ShootballSessionInfo.GameMode = Utility.GetAssetRef<GameMode>("Shootball/GameModes/ClassicGameMode");
            ShootballSessionInfo.NumOfPlayers = 2;
            ShootballSessionInfo.SessionName = "shootball_test_session";

            // Player
            ShootballPlayerInfo.PlayerName = "Player-1";
            ShootballPlayerInfo.EntityPrototypeAssetRef = Utility.GetAssetRef<EntityPrototype>("Shootball/Players/PlayerRedEntityPrototype");
            Debug.Log($"Player:{ShootballPlayerInfo.EntityPrototypeAssetRef}, isValid:{ShootballSessionInfo.SystemsConfig.IsValid}");
            //QuantumRunner.Default.Session
//#endif
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += HandleOnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleOnSceneLoaded;
        }

        private void HandleOnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Set the scene we just loaded as the active scene and unload the old one.
            Scene oldScene = SceneManager.GetActiveScene();
            if(scene == oldScene)
            {
                Debug.LogWarning($"Loaded scene is the same as the active scene, loaded:{scene}, active:{oldScene}");
                return;
            }

            // Set the new scene as the active one
            SceneManager.SetActiveScene(scene);
            // Unload the old scene
            SceneManager.UnloadSceneAsync(oldScene);

        }

        public async void JoinMultiplayerSession(UnityAction<bool> callback)
        {
            var connectionArguments = new MatchmakingArguments
            {
                // The Photon application settings
                PhotonSettings = PhotonServerSettings.Global.AppSettings,
                //Will be configured as "EnterRoomArgs.RoomOptions.PlayerTtl" when creating a Photon room
                //PlayerTtlInSeconds = 10,
                //Will be configured as "EnterRoomArgs.RoomOptions.EmptyRoomTtl" when creating a Photon room
                //EmptyRoomTtlInSeconds = 10,
                // Will be configured as "EnterRoomArgs.RoomOptions.RoomName when creating a Photon room.
                RoomName = "QRoom",   //ShootballSessionInfo.SessionName,
                // The maximum number of clients for the room, in this case we use the code-generated max possible players for the Quantum simulation
                MaxPlayers = 2,//ShootballSessionInfo.NumOfPlayers,
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
                //ReconnectInformation = new MatchmakingReconnectInformation(),
                //Optional Realtime lobby to use for matchmaking
                //Lobby = new TypedLobby(),
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
            
            // Create session 
            var sessionRunnerArguments = new SessionRunner.Arguments
            {
                // The runner factory is the glue between the Quantum.Runner and Unity
                RunnerFactory = QuantumRunnerUnityFactory.DefaultFactory,
                // Creates a default version of `QuantumGameStartParameters`
                GameParameters = QuantumRunnerUnityFactory.CreateGameParameters,
                // A secret user id that is for example used to reserved player slots to reconnect into a running session
                ClientId = Client.UserId,
                SessionConfig = QuantumDeterministicSessionConfigAsset.DefaultConfig,
                PlayerCount = 2,//ShootballSessionInfo.NumOfPlayers,
                StartGameTimeoutInSeconds = 30, // TODO: we must use WaitForGameStart()
                Communicator = new QuantumNetworkCommunicator(Client),
                GameMode = Photon.Deterministic.DeterministicGameMode.Multiplayer,

                // RuntimeConfig info
                RuntimeConfig = new RuntimeConfig()
                {
                    Map = ShootballSessionInfo.Map,
                    Seed = DateTime.Now.Millisecond,
                    SystemsConfig = ShootballSessionInfo.SystemsConfig,
                    SimulationConfig = ShootballSessionInfo.SimulationConfig,
                    GameMode = ShootballSessionInfo.GameMode
                  
                }
            };

            QuantumRunner runner = (QuantumRunner)await SessionRunner.StartAsync(sessionRunnerArguments);
            if(runner == null || !runner.Session.IsRunning)
            { 

                callback?.Invoke(false); 
            }
            else
            {
                var runtimePlayer = new RuntimePlayer()
                {
                    PlayerAvatar = ShootballPlayerInfo.EntityPrototypeAssetRef,
                    PlayerNickname = ShootballPlayerInfo.PlayerName
                };
                runner.Game.AddPlayer(runtimePlayer);
                callback?.Invoke(true);
            }
        }
    }
}
