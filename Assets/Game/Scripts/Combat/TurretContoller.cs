using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    public class TurretContoller : MonoBehaviour
    {
        [SerializeField] Transform gunTurret;
        [SerializeField] float maxXRotation = 0f;
        [SerializeField] float minXRotation = -60f;
        [SerializeField] float targetingAccuracy = 5f;
        [SerializeField] LaserWeapon[] laserWeapons;

        [SerializeField] float turnSpeed = 1f;


        CombatTarget combatTarget;
        CombatTarget thisCombatTarget;

        // Start is called before the first frame update
        void Start()
        {
            thisCombatTarget = GetComponent<CombatTarget>();
            foreach (var laserWeapon in laserWeapons)
            {
                laserWeapon.TargetAquiredUpdated += ControlShooting;
            }
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
            FaceTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            CombatTarget otherCombatTarget = other.GetComponent<CombatTarget>();
            SetNewCombatTarget(otherCombatTarget);
        }

        private void SetNewCombatTarget(CombatTarget otherCombatTarget)
        {
            if (IsValidTarget(otherCombatTarget))
            {
                combatTarget = otherCombatTarget;
                foreach (var laserWeapon in laserWeapons)
                {
                    laserWeapon.UpdateTarget(combatTarget);
                }
            }
        }

        private bool IsValidTarget(CombatTarget otherCombatTarget)
        {
            if (otherCombatTarget == null) return false;
            if (otherCombatTarget.GetFaction() == thisCombatTarget.GetFaction()) return false;
            if (otherCombatTarget.GetFaction() == TargetStore.Instance.GetNeutralFaction()) return false;
            return true;
        }

        private void FaceTarget()
        {
            if (combatTarget == null) return;

            gunTurret.transform.LookAt(combatTarget.transform);
        }

        private void ControlShooting()
        {
            foreach (var laserWeapon in laserWeapons)
            {
                laserWeapon.Shoot();
            }
        }

    }

}


