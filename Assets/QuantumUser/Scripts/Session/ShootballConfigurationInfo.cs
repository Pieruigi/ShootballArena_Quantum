using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Deterministic;
using Quantum;
using Photon.Deterministic.Protocol;

namespace Quantum.Shootball
{
    public static class ShootballConfigurationInfo
    {
        static AssetRef<Map> map;
        public static AssetRef<Map> Map {  get { return map; } }
        
        static AssetRef<SimulationConfig> simulationConfig;
        public static AssetRef<SimulationConfig> SimulationConfig { get { return simulationConfig; } }

        static AssetRef<SystemsConfig> systemConfig;
        public static AssetRef<SystemsConfig> SystemConfig { get {return systemConfig; } } 

        static AssetRef<GameMode> gameMode;
        public static AssetRef<GameMode> GameMode { get { return gameMode; } }
        
        public static void InitMap(string path)
        {
            //map = new AssetRef<Map>()
            var m = Resources.Load<Map>(path);
            if(m == null)
            {
                Debug.LogError($"No map found:{path}");
                return;
            }

            map = new AssetRef<Map>(Resources.Load<Map>(path).Guid);
            Debug.Log($"Map:{map.Id}, {map.IsValid}");
        }

        public static void InitSimulationConfig(string path)
        {
            //map = new AssetRef<Map>()
            var m = Resources.Load<SimulationConfig>(path);
            if (m == null)
            {
                Debug.LogError($"No simulation config found:{path}");
                return;
            }

            simulationConfig = new AssetRef<SimulationConfig>(Resources.Load<SimulationConfig>(path).Guid);
            Debug.Log($"Simulation:{simulationConfig.Id}, {simulationConfig.IsValid}");
        }

        public static void InitSystemConfig(string path)
        {
            //map = new AssetRef<Map>()
            var m = Resources.Load<SystemsConfig>(path);
            if (m == null)
            {
                Debug.LogError($"No system config found:{path}");
                return;
            }

            systemConfig = new AssetRef<SystemsConfig>(Resources.Load<SystemsConfig>(path).Guid);
            Debug.Log($"System:{systemConfig.Id}, {systemConfig.IsValid}");
        }

        public static void InitGameMode(string path)
        {
            //map = new AssetRef<Map>()
            var m = Resources.Load<GameMode>(path);
            if (m == null)
            {
                Debug.LogError($"No game mode found:{path}");
                return;
            }

            gameMode = new AssetRef<GameMode>(Resources.Load<GameMode>(path).Guid);
            Debug.Log($"GameMode:{gameMode.Id}, {gameMode.IsValid}");
        }
    }
}
