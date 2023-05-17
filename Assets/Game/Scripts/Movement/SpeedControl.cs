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


        // Start is called before the first frame update
        void Start()
        {
            move = GetComponent<Move>();
        }

        // Update is called once per frame
        void Update()
        {
            RespondToSpeedInput();
        }

        private void RespondToSpeedInput()
        {
     
            if (Input.GetKey(KeyCode.Keypad1))
            {
                move.ChangeSpeed(1f);
            }
            else if (Input.GetKey(KeyCode.Keypad0))
            {
                move.ChangeSpeed(-1f);
            }

        }
    }

}


