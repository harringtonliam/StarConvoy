using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Movement
{
    public class StationaryObject : MonoBehaviour
    {
        [SerializeField] float rotateSpeed = 1f;
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float postiionTolerance = 1f;
        [SerializeField] float rotationTolerance = 1f;

        Vector3 startPosition;
        Vector3 startRotation;


        // Start is called before the first frame update
        void Start()
        {
            startPosition = transform.position;
            startRotation = transform.forward;
        }

        // Update is called once per frame
        void Update()
        {
            CheckPostion();
            CheckRotation();
        }

        private void CheckPostion()
        {
            if (Vector3.Distance(startPosition, transform.position) < postiionTolerance) return;

            float step = moveSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);

        }

        private void CheckRotation()
        {
            if (Vector3.Angle(startRotation, transform.forward) < rotationTolerance) return;

            float step = rotateSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, startRotation, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        
    }

}


