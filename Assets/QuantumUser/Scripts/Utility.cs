using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quantum.Shootball
{
    public static class Utility
    {
        
        public static AssetRef<T> GetAssetRef<T>(string path) where T:AssetObject
        {
            var m = Resources.Load<AssetObject>(path);
            if (m == null)
            {
                Debug.LogError($"No asset found:{path}");
                return null;
            }

            return new AssetRef<T>(m.Guid);
        }


    }

}
