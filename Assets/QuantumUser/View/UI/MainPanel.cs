using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;
using System;
using UnityEditor.PackageManager;
using System.Threading.Tasks;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEditor;
using Photon.Deterministic.Protocol;

namespace Quantum.Shootball
{
    public class MainPanel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
#if UNITY_EDITOR
            // Move this configuration where player choose the type of game
            ShootballConfigurationInfo.InitMap("Shootball/Maps/DefaultArenaMap");
            ShootballConfigurationInfo.InitSimulationConfig("Shootball/Config/ShootballSimulationConfig");
            ShootballConfigurationInfo.InitSystemConfig("Shootball/Config/ShootballSystemConfig");
            ShootballConfigurationInfo.InitGameMode("Shootball/GameModes/ClassicGameMode");

#endif
        }

        // Update is called once per frame
        void Update()
        {

        }

      

        public async void CreateOrJoinMultiplayerSession()
        {
            ShootballSessionManager.JoinMultiplayerSession(
                new ShootballSessionManager.SessionArgs()
                {
                    NumOfPlayers = 2,
                    RoomName = "Pippo"
                },
                (ret) => Debug.Log($"Join session result:{ret}"));
                
        }
    }

}

