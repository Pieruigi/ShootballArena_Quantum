using Photon.Deterministic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quantum.Shootball
{
    public class PlayerInput : MonoBehaviour
    {
        float mouseSensitivity = 1;

        float yawDelta;
        public float YawDelta
        {
            get { return yawDelta; }
        }

        float pitchDelta;
        public float PitchDelta
        {
            get { return pitchDelta; }
        }
        private void Update()
        {
            // Accumulate yaw and pitch
            yawDelta += UnityEngine.Input.GetAxis("Mouse X") * mouseSensitivity;
            pitchDelta -= UnityEngine.Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

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
            
            i.Direction = new FPVector2(FP.FromFloat_UNSAFE(x), FP.FromFloat_UNSAFE(y));
           
            i.YawDelta = FP.FromFloat_UNSAFE(yawDelta);
            yawDelta = 0;
            i.PitchDelta = FP.FromFloat_UNSAFE(pitchDelta);
            pitchDelta = 0;

            i.Jump = jump;
            i.Sprint = sprint;
            i.Fire1 = fire1;
            i.Fire2 = fire2;

            callback.SetInput(i, DeterministicInputFlags.Repeatable);
        }
    }

}
