using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SC.Movement
{

    public class SpeedControl : MonoBehaviour
    {
        [SerializeField] float speedChangeResponseTime = 0.2f;

        Move move;
        bool isEnabled;


        // Start is called before the first frame update
        void Start()
        {
            isEnabled = true;
            move = GetComponent<Move>();
        }

        // Update is called once per frame
        void Update()
        {
            RespondToSpeedInput();
        }

        public void SetEnabled(bool enable)
        {
            isEnabled = enable;
        }

        public bool GetEnabled()
        {
            return isEnabled;
        }

        private void RespondToSpeedInput()
        {
            if (!isEnabled) return;

            float forwardInput = Input.GetAxis("Forward");

            if (forwardInput > 0f)
            {
                move.ChangeSpeed(1f);
            }
            else if (forwardInput < 0f)
            {
                move.ChangeSpeed(-1f);
            }

        }


    }

}


