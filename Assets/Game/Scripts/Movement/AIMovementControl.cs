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


        Move move;
        Vector3 currentMoveTarget;

        // Start is called before the first frame update
        void Start()
        {
            move = GetComponent<Move>();
            currentMoveTarget = moveTarget.position;
        }

        // Update is called once per frame
        void Update()
        {
            ControlSpeed();
            FaceTarget();
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

        private void ControlSpeed()
            
        {
            if (!canControlSpeed) return;

            if (move.CurrentSpeed < move.MaxSpeed)
            {
                move.ChangeSpeed(1f);
            }
        }

        private void FaceTarget()
        {
            if (!canManeuver) return;
            if (currentMoveTarget == null) return;

            Vector3 targetDirection = currentMoveTarget - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = turnSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

}


