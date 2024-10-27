using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SC.Movement
{
    public class AIMovementControl : MonoBehaviour
    {
        [SerializeField] Transform moveTarget;
        [SerializeField] float turnSpeed = 10f;
        [SerializeField] bool canControlSpeed = true;
        [SerializeField] bool canManeuver = true;
        [SerializeField] float desiredSpeed;
        [SerializeField] GameObject engines;


        Move move;
        Vector3 currentMoveTarget;
        Vector3 collisionAviodanceDirection;
        bool collisonAvoidanceOn = false;
        bool isEnginesOn = true;

        // Start is called before the first frame update
        void Start()
        {
            move = GetComponent<Move>();
            if (moveTarget != null)
            {
                currentMoveTarget = moveTarget.position;
            }
            desiredSpeed = move.MaxSpeed;
            
        }

        // Update is called once per frame
        void Update()
        {
            ControlSpeed();
            FaceTarget();
        }

        public Vector3 GetCurrentMoveTarget()
        {
            return currentMoveTarget;
        }

        public void SetCurrentMoveTarget(Vector3 newCurrentMoveTarget)
        {
            currentMoveTarget = newCurrentMoveTarget;
        }

        public void SetMovementTarget(Transform newTarget)
        {
            moveTarget = newTarget;
            currentMoveTarget = moveTarget.position;
        }

        public void SetCanControLSpeed(bool newSetting)
        {
            canControlSpeed = newSetting;
        }

        public void SetCanManeuver(bool newSetting)
        {
            canManeuver = newSetting;
        }

        public void SetDesiredSpeed(float newDesiredSpeed)
        {
            desiredSpeed = Mathf.Clamp(newDesiredSpeed, move.MinSpeed, move.MaxSpeed);
        }

        public void SetCollisionAvoidanceDirection(Vector3 direction, bool setOnOff)
        {
            
            collisionAviodanceDirection = direction;
            collisonAvoidanceOn = setOnOff;
        }

        public void SetCollisionAvoidanceDirection(bool setOnOff)
        {
            collisonAvoidanceOn = setOnOff;
        }

        private void ControlSpeed()
            
        {
            if (!canControlSpeed) return;

            if (move.CurrentSpeed < desiredSpeed)
            {
                ToggleEngines(true);
                move.ChangeSpeed(1f);
            }
            else if (move.CurrentSpeed > desiredSpeed)
            {
                ToggleEngines(false);
                move.ChangeSpeed(-1f);
            }
        }

        private void FaceTarget()
        {
            if (!canManeuver) return;
            if (currentMoveTarget == null && !collisonAvoidanceOn) return;

            Vector3 targetDirection;

            if (collisonAvoidanceOn)
            {
                targetDirection = collisionAviodanceDirection;
            }
            else
            {
                targetDirection = currentMoveTarget - transform.position;
            }


            // The step size is equal to speed times frame time.
            float singleStep = turnSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        private void ToggleEngines(bool enginesOn)
        {
            if (engines == null ) return;
            if (enginesOn == engines.activeSelf) { return; }
            engines.SetActive(enginesOn);
        }


    }

}


