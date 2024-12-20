using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Deterministic;
using Quantum;
using Photon.Deterministic.Protocol;

namespace Quantum.Shootball
{
    /// <summary>
    /// Used in the main scene.
    /// Store informations about session that will be used to setup the runner.
    /// </summary>
    public static class ShootballSessionInfo
    {
        public static AssetRef<Map> Map { get; set; }
        
        public static AssetRef<SimulationConfig> SimulationConfig { get; set; }

        public static AssetRef<SystemsConfig> SystemsConfig { get; set; } 
        
        public static AssetRef<GameMode> GameMode { get; set; }

        public static int NumOfPlayers { get; set; }

        public static string SessionName { get; set; }

    }
}
