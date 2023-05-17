using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public bool canChangeSpeed = true;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rigidbody.velocity = transform.forward * currentSpeed;
        }

        public void ChangeSpeed(float changeToSpeed)
        {
            if (canChangeSpeed)
            {
                StartCoroutine(ChangingSpeed(changeToSpeed));
            }


        }

        IEnumerator ChangingSpeed(float changeToSpeed)
        {
            canChangeSpeed = false;

            currentSpeed = Mathf.Clamp(currentSpeed + changeToSpeed, minSpeed, maxSpeed);
            yield return new WaitForSeconds(speedChangeResponseTime);
            canChangeSpeed = true;
        }

    }

}


