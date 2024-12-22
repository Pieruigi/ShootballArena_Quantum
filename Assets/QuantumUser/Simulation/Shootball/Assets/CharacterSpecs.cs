namespace Quantum.Shootball
{
    using Photon.Deterministic;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Quantum;

    public class CharacterSpecs : AssetObject
    {
        public FP Acceleration;
        public FP MaxSpeed;
        public FP RotationSpeed;
        public FP MaxStamina;
        public FP JumpForce;
        public FP SprintMultiplier;
    }
}
