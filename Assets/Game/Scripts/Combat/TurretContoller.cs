using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    public class TurretContoller : MonoBehaviour
    {
        [SerializeField] Transform gunTurret;
        [SerializeField] Transform targetingTransform;
        [SerializeField] float targetingRange = 900f;
        [SerializeField] LaserWeapon[] laserWeapons;


        CombatTarget currentCombatTarget;
        CombatTarget thisCombatTarget;

        // Start is called before the first frame update
        void Start()
        {
            thisCombatTarget = GetComponent<CombatTarget>();
        }

        private void OnDisable()
        {
            foreach (var laserWeapon in laserWeapons)
            {
                try
                {
                    laserWeapon.TargetAquiredUpdated -= ControlShooting;
                }
                catch
                {
                    Debug.Log("gunTurret  on disable failed on laserWeapon.TargetAquiredUpdated -= ControlShooting;");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            SelectTarget();
            FaceTarget();
            ControlShooting();
        }

        private void SelectTarget()
        {
            if (ExistingTargetStillValid()) return;

            var listOfTargets = TargetStore.Instance.ValidCombatTargetsNotInFaction(thisCombatTarget.GetFaction());
            foreach (var item in listOfTargets)
            {
                if(DistanceToTarget(item.Value) <= targetingRange)
                {
                    if(CanSeeTarget(item.Value))
                    {
                    currentCombatTarget = item.Value;
                    }
                }
            }
            if (currentCombatTarget == null) return;

            foreach (var laserWeapon in laserWeapons)
            {
                laserWeapon.UpdateTarget(currentCombatTarget);
            }
        }

        private float DistanceToTarget(CombatTarget combatTarget)
        {
            if (combatTarget == null) return 2000f;
            return Vector3.Distance(this.transform.position, combatTarget.transform.position);
        }

        private bool CanSeeTarget(CombatTarget combatTarget)
        {
            return ProcessRayCast(combatTarget);
        }

        private bool ExistingTargetStillValid()
        {
            if (currentCombatTarget == null) return false;
            if (DistanceToTarget(currentCombatTarget) <= targetingRange ) return true;//&& CanSeeTarget(currentCombatTarget)) return true;
            return false;
        }


        private void FaceTarget()
        {
            if (currentCombatTarget == null) return;

            gunTurret.transform.LookAt(currentCombatTarget.transform);
        }

        private void ControlShooting()
        {
            if (currentCombatTarget == null) return;

            foreach (var laserWeapon in laserWeapons)
            {
                laserWeapon.Shoot();
            }
        }

        private bool ProcessRayCast(CombatTarget combatTarget)
        {
            RaycastHit hit;
            targetingTransform.LookAt(combatTarget.transform.position);

            bool raycastForward = Physics.Raycast(targetingTransform.position, targetingTransform.forward, out hit, targetingRange);

            if (raycastForward)
            {
                CombatTarget targetToCheck = hit.transform.GetComponent<CombatTarget>();
                if (targetToCheck != null && targetToCheck == combatTarget) return true;
            }
            return false;
        }

    }

}


