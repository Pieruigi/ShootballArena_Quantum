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

        // Update is called once per frame
        void Update()
        {

        }

      

        public void CreateOrJoinMultiplayerSession()
        {
            ShootballSessionManager.Instance.JoinMultiplayerSession((ret) => Debug.Log($"Join session result:{ret}"));
                
        }
    }

}

