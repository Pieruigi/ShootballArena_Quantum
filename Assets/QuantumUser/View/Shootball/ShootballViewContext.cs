using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

namespace Quantum.Shootball
{
    public class ShootballViewContext : MonoBehaviour, IQuantumViewContext
    {
        [SerializeField]
        Camera _camera;
        public Camera Camera {  get { return _camera; } }
    }

}
