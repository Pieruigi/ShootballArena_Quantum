using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

namespace Quantum.Shootball
{
    /// <summary>
    /// Only needed for local player to store informations that will be used to fill the RuntimePlayer struct.
    /// </summary>
    public static class ShootballPlayerInfo
    {
        public static string PlayerName { get; set; }

        public static AssetRef<EntityPrototype> EntityPrototypeAssetRef { get; set; }

    }

}

