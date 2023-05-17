using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SC.Movement
{




    public class Rotate : MonoBehaviour
    {
        [SerializeField] float turnSpeed = 100f;
        [SerializeField] Transform rotationTarget;

        private Camera mainCamera;


        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {


            RespondToRotateInput();
        }

        private void RespondToRotateInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            PerformRotation(horizontalInput, verticalInput);
        }

        public void PerformRotateTowards(Vector2 target)
        {
            //Debug.Log("Perform rotate towards " + target.x + " " + target.y);

            //Vector3 newTarget = mainCamera.ScreenToWorldPoint(new Vector3(target.x, target.y, 50f));
           

            //rotationTarget.position = newTarget;


            //Vector3 targetDirection = newTarget - transform.position;

            //// The step size is equal to speed times frame time.
            //float singleStep = turnSpeed * Time.deltaTime;

            //// Rotate the forward vector towards the target direction by one step
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);
        }

        public void PerformRotation(float horizontalInput, float verticalInput)
        {

            float rotationThisFrame = turnSpeed * Time.deltaTime;
            if (horizontalInput < 0f || horizontalInput > 0f)
            {
                transform.Rotate(Vector3.up * rotationThisFrame * horizontalInput);
            }
            if (verticalInput < 0f || verticalInput > 0f)
            {
                transform.Rotate(Vector3.right * rotationThisFrame * verticalInput);
            }

            if (Input.GetKey(KeyCode.O))
            {
                transform.Rotate(-Vector3.forward * rotationThisFrame);
            }
            else if (Input.GetKey(KeyCode.P))
            {
                transform.Rotate(Vector3.forward * rotationThisFrame);
            }
        }
    }

}


