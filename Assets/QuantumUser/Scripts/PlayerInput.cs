using Photon.Deterministic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quantum.Shootball
{
    public class PlayerInput : MonoBehaviour
    {
        private void OnEnable()
        {
            QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
        }

        //private void OnDisable()
        //{
        //    QuantumCallback.UnsubscribeListener(this);
        //}

        private void PollInput(CallbackPollInput callback)
        {
            Input i = new Input();

            

            // Get input
            float x = UnityEngine.Input.GetAxis("Horizontal");
            float y = UnityEngine.Input.GetAxis("Vertical");
            bool jump = UnityEngine.Input.GetKey(KeyBindings.JumpKey);
            bool sprint = UnityEngine.Input.GetKey(KeyBindings.SprintKey);
            bool fire1 = UnityEngine.Input.GetKey(KeyBindings.Fire1Key);
            bool fire2 = UnityEngine.Input.GetKey(KeyBindings.Fire2Key);
            float aimX = UnityEngine.Input.GetAxis("Mouse X");
            float aimY = UnityEngine.Input.GetAxis("Mouse Y");

            i.Direction = new FPVector2(x.ToFP(), y.ToFP());
            i.AimDirection = new FPVector2(aimX.ToFP(), aimY.ToFP());
            i.Jump = jump;
            i.Sprint = sprint;
            i.Fire1 = fire1;
            i.Fire2 = fire2;

            callback.SetInput(i, DeterministicInputFlags.Repeatable);
        }
    }

}
