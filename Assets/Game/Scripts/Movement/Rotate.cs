using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SC.Movement
{




    public class Rotate : MonoBehaviour
    {
        [SerializeField] float turnSpeed = 100f;

        private Camera mainCamera;

        public float TurnSpeed { get { return turnSpeed; } }

        public bool isEnabled;
    

        // Start is called before the first frame update
        void Start()
        {
            //isEnabled = true;
            mainCamera = Camera.main;
        }



        // Update is called once per frame
        void Update()
        {
            RespondToRotateInput();
        }

        public void SetEnabled(bool enable)
        {
            isEnabled = enable;
        }

        public bool GetEnabled()
        {
            return isEnabled;
        }

        private void RespondToRotateInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float forwardInput = Input.GetAxis("Forward");
            if (forwardInput != 0f)
            {
                Debug.Log("ForwardInput = " + forwardInput.ToString());
            }

            PerformRotation(horizontalInput, verticalInput);
        }

        public void PerformRotation(float horizontalInput, float verticalInput)
        {
            if (!isEnabled) return;

            float rotationThisFrame = turnSpeed * Time.deltaTime;
            if (horizontalInput < 0f || horizontalInput > 0f)
            {
                transform.Rotate(Vector3.up * rotationThisFrame * horizontalInput);
            }
            if (verticalInput < 0f || verticalInput > 0f)
            {
                transform.Rotate(Vector3.right * rotationThisFrame * verticalInput);
            }

            if (Input.GetKey(KeyCode.B))
            {
                transform.Rotate(-Vector3.forward * rotationThisFrame);
            }
            else if (Input.GetKey(KeyCode.M))
            {
                transform.Rotate(Vector3.forward * rotationThisFrame);
            }
        }
    }

}


