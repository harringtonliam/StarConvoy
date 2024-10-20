using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SC.Movement
{
    public class Move : MonoBehaviour
    {
        [SerializeField] float currentSpeed = 100f;
        [SerializeField] float minSpeed = 0f;
        [SerializeField] float maxSpeed = 500f;
        [SerializeField] float speedChangeResponseTime = 0.2f;

        Rigidbody rigidbody;

        public float CurrentSpeed { get { return currentSpeed; } }   
        public float MaxSpeed { get { return maxSpeed;  } }
        public float MinSpeed { get { return minSpeed; } }

        public bool canChangeSpeed = true;

        public event Action speedUpdated;

        public float GetRateOfAcceleration()
        {
            return 1f / speedChangeResponseTime;
        }

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            if (speedUpdated != null)
            {
                speedUpdated();
            }
        }

        // Update is called once per frame
        void Update()
        {
            rigidbody.linearVelocity = transform.forward * currentSpeed;
        }

        public void ChangeSpeed(float changeToSpeed)
        {
            if (canChangeSpeed)
            {
                StartCoroutine(ChangingSpeed(changeToSpeed));
            }
            if (speedUpdated != null)
            {
                speedUpdated();
            }

        }

        IEnumerator ChangingSpeed(float changeToSpeed)
        {
            canChangeSpeed = false;

            currentSpeed = Mathf.Clamp(currentSpeed + changeToSpeed, minSpeed, maxSpeed);
            if (speedUpdated != null)
            {
                speedUpdated();
            }
            yield return new WaitForSeconds(speedChangeResponseTime);
            canChangeSpeed = true;
        }

        public void SetCurrentSpeed(float newSpeed)
        {
            SetCurrentSpeed(newSpeed, false);
            if (speedUpdated != null)
            {
                speedUpdated();
            }
        }

        public void SetCurrentSpeed(float newSpeed, bool overRideMaxSpeed)
        {
            if (overRideMaxSpeed)
            {
                currentSpeed = newSpeed;
            }
            else
            {
                currentSpeed = Mathf.Clamp(newSpeed, 0f, MaxSpeed);
            }
            if (speedUpdated != null)
            {
                speedUpdated();
            }
        }

    }

}


