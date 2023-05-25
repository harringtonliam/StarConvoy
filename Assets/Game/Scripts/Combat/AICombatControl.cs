using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;


namespace SC.Combat
{
    public class AICombatControl : MonoBehaviour
    {
        [SerializeField] GameObject combatTarget;
        [SerializeField] float closestApproach = 200f;
        [SerializeField] float breakOffDistance = 500f;
        [SerializeField] float breakOffTolerance = 20f;
        [SerializeField] float turnSpeed = 10f;
        [SerializeField] float targetingAccuracy = 10f;
        [SerializeField] LaserWeapon[] laserWeapons;

        Vector3 currentMoveTarget;

        bool movingAwayFromCombatTarget;

        Move move;
   
        // Start is called before the first frame update
        void Start()
        {
            move = GetComponent<Move>();
            currentMoveTarget = combatTarget.transform.position;
            movingAwayFromCombatTarget = false;
        }

        // Update is called once per frame
        void Update()
        {
            ControlSpeed();
            CheckMoveTarget();
            FaceTarget();
            ControlShooting();
        }

        public void SetCombatTarget(GameObject target)
        {
            combatTarget = target;
        }

        private void ControlShooting()
        {
            if (!IsTargetInWeaponRange()) return;
            if (!IsFaceingCombatTarget()) return;
            foreach (var laserWeapon in laserWeapons)
            {
                laserWeapon.Shoot();
            }
        }

        private bool IsTargetInWeaponRange()
        {
            float distanceToCombatTarget = DistanceToCombatTarget();

            foreach (var laserWeapon in laserWeapons)
            {
                if (laserWeapon.Range > DistanceToCombatTarget())
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsFaceingCombatTarget()
        {
            if (combatTarget == null) return false;
            Vector3 directionToTarget = combatTarget.transform.position - transform.position;
            float angleTocombatTarget = Vector3.Angle(transform.forward, directionToTarget);
            return angleTocombatTarget <= targetingAccuracy;
        }

        private float DistanceToCombatTarget()
        {
            if (combatTarget == null)
            {
                return Mathf.Infinity;
            }
            return Vector3.Distance(combatTarget.transform.position, transform.position);
        }

        private void FaceTarget()
        {
            if (currentMoveTarget == null) return;
            Vector3 targetDirection = currentMoveTarget - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = turnSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);

        }

        private void CheckMoveTarget()
        {
            float distanceToCombatTarget = DistanceToCombatTarget();
            if (distanceToCombatTarget > closestApproach && !movingAwayFromCombatTarget)
            {
                currentMoveTarget = combatTarget.transform.position;
            }
            else if(!movingAwayFromCombatTarget)
            {
                movingAwayFromCombatTarget = true;
                CalculateNewMovementTarget();
            }
            else
            {
                CheckBreakOff();
            }
        }

        private void CheckBreakOff()
        {
            float distanceToBreakOffPoint = Vector3.Distance(currentMoveTarget, transform.position);
            if (distanceToBreakOffPoint <= breakOffTolerance)
            {
                movingAwayFromCombatTarget = false;
                currentMoveTarget = combatTarget.transform.position;
            }
        }

        private void CalculateNewMovementTarget()
        {
            if (combatTarget == null) return;
            
            int randomBreakoff = Random.Range(0, 3);
            switch (randomBreakoff)
            {
                case 0:
                    currentMoveTarget = combatTarget.transform.position + new Vector3(0f, breakOffDistance, 0f);
                    break;
                case 1:
                    currentMoveTarget = combatTarget.transform.position + new Vector3(0f, breakOffDistance * -1, 0f);
                    break;
                case 2:
                    currentMoveTarget = combatTarget.transform.position + new Vector3(breakOffDistance, breakOffDistance, 0f);
                    break;
                case 3:
                    currentMoveTarget = combatTarget.transform.position + new Vector3(breakOffDistance * -1, breakOffDistance * -1, 0f);
                    break;
                default:
                    currentMoveTarget = combatTarget.transform.position + new Vector3(0f, breakOffDistance, 0f);
                    break;
            }
        }

        private void ControlSpeed()
        {
            if (move.CurrentSpeed < move.MaxSpeed)
            {
                move.ChangeSpeed(1f);
            }
        }
    }

}


