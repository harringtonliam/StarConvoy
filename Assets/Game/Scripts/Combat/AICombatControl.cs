using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;
using SC.Attributes;


namespace SC.Combat
{
    public class AICombatControl : MonoBehaviour
    {
        [SerializeField] CombatTarget combatTarget;
        [SerializeField] float closestApproach = 200f;
        [SerializeField] float breakOffDistance = 500f;
        [SerializeField] float breakOffTolerance = 20f;
        [SerializeField] float turnSpeed = 10f;
        [SerializeField] float targetingAccuracy = 10f;
        [SerializeField] LaserWeapon[] laserWeapons;

        //Vector3 currentMoveTarget;

        bool movingAwayFromCombatTarget;

        Move move;
        AIMovementControl aIMovementControl;

        CombatTarget thisShipCombatTarget;
   
        // Start is called before the first frame update
        void Start()
        {
            thisShipCombatTarget = GetComponent<CombatTarget>();
            aIMovementControl = GetComponent<AIMovementControl>();

            CheckCombatTarget();
            move = GetComponent<Move>();
            if (combatTarget != null)
            {
                aIMovementControl.SetMovementTarget(combatTarget.transform);
            }

            movingAwayFromCombatTarget = false;
        }

        // Update is called once per frame
        void Update()
        {
            ControlSpeed();
            CheckMoveTarget();
            //FaceTarget();
            ControlShooting();
        }

        public void SetCombatTarget(CombatTarget target)
        {
            combatTarget = target;
        }

        public bool CheckCombatTarget()
        {
            if (combatTarget != null  && !combatTarget.GetIsHidden()) return true;
            return FindCombatTarget();
        }

        public bool FindCombatTarget()
        {
            var targetsToChooseFrom = TargetStore.Instance.ValidCombatTargetsNotInFaction(thisShipCombatTarget.GetFaction());
            if (targetsToChooseFrom.Count <= 0) return false;

            int randomTarget = Random.Range(0, targetsToChooseFrom.Count - 1);
            int i = 0;
            foreach (var item in targetsToChooseFrom)
            {
                if (i == randomTarget)
                {
                    combatTarget = item.Value;
                    return true;
                }
                i++;
            }

            return false;

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
            if (CheckCombatTarget() == false) return false;
            Vector3 directionToTarget = combatTarget.transform.position - transform.position;
            float angleTocombatTarget = Vector3.Angle(transform.forward, directionToTarget);
            return angleTocombatTarget <= targetingAccuracy;
        }

        private float DistanceToCombatTarget()
        {
            if (CheckCombatTarget() == false)
            {
                return Mathf.Infinity;
            }
            return Vector3.Distance(combatTarget.transform.position, transform.position);
        }

        //private void FaceTarget()
        //{
        //    if (currentMoveTarget == null) return;
        //    Vector3 targetDirection = currentMoveTarget - transform.position;

        //    // The step size is equal to speed times frame time.
        //    float singleStep = turnSpeed * Time.deltaTime;

        //    // Rotate the forward vector towards the target direction by one step
        //    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        //    // Draw a ray pointing at our target in
        //    Debug.DrawRay(transform.position, newDirection, Color.red);

        //    // Calculate a rotation a step closer to the target and applies rotation to this object
        //    transform.rotation = Quaternion.LookRotation(newDirection);

        //}

        private void CheckMoveTarget()
        {
            if (CheckCombatTarget() == false) return;
            float distanceToCombatTarget = DistanceToCombatTarget();
            if (distanceToCombatTarget > closestApproach && !movingAwayFromCombatTarget)
            {
                aIMovementControl.SetMovementTarget(combatTarget.transform);
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
            if (CheckCombatTarget() == false) return;
            float distanceToBreakOffPoint = Vector3.Distance(aIMovementControl.GetCurrentMoveTarget(), transform.position);
            if (distanceToBreakOffPoint <= breakOffTolerance)
            {
                movingAwayFromCombatTarget = false;
                aIMovementControl.SetCurrentMoveTarget(combatTarget.transform.position);
            }
        }

        private void CalculateNewMovementTarget()
        {
            if (CheckCombatTarget() == false) return;
            
            int randomBreakoff = Random.Range(0, 3);
            switch (randomBreakoff)
            {
                case 0:
                    aIMovementControl.SetCurrentMoveTarget(combatTarget.transform.position + new Vector3(0f, breakOffDistance, 0f));
                    break;
                case 1:
                    aIMovementControl.SetCurrentMoveTarget(combatTarget.transform.position + new Vector3(0f, breakOffDistance * -1, 0f));
                    break;
                case 2:
                    aIMovementControl.SetCurrentMoveTarget(combatTarget.transform.position + new Vector3(breakOffDistance, breakOffDistance, 0f));
                    break;
                case 3:
                    aIMovementControl.SetCurrentMoveTarget(combatTarget.transform.position + new Vector3(breakOffDistance * -1, breakOffDistance * -1, 0f));
                    break;
                default:
                    aIMovementControl.SetCurrentMoveTarget(combatTarget.transform.position + new Vector3(0f, breakOffDistance, 0f));
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

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, closestApproach);
        }
    }

}


