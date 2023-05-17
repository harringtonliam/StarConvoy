using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] LaserWeapon[] laserWeapons;
        [SerializeField] MissileLauncher[] missileLaunchers;

        public LaserWeapon[] LaserWeapons {  get { return laserWeapons; } }
        public MissileLauncher[] MissileLaunchers {  get { return missileLaunchers; } }

        int missileLauncherIndex;

        TargetSelection targetSelection;

        private void Start()
        {
            missileLauncherIndex = 0;
            targetSelection = GetComponent<TargetSelection>();
            targetSelection.CurrentTargetChanged += OnTargetChanged;

        }

        private void OnDestroy()
        {
            targetSelection.CurrentTargetChanged -= OnTargetChanged;

        }

        // Update is called once per frame
        void Update()
        {
            RespondToWeaponInput();
        }

        private void RespondToWeaponInput()
        {
            if (Input.GetButton("Fire1"))
            {
                foreach (var laserWeapon in laserWeapons)
                {
                    laserWeapon.Shoot();
                }
            }

            if (Input.GetButtonDown("Fire2"))
            {
                missileLaunchers[missileLauncherIndex].Shoot();
                missileLauncherIndex++;
                if (missileLauncherIndex >= missileLaunchers.Length)
                {
                    missileLauncherIndex = 0;
                }

            }
        }

        private void OnTargetChanged()
        {
            foreach (var laserWeapon in laserWeapons)
            {
                laserWeapon.UpdateTarget( targetSelection.GetCurrentTarget());
            }

            foreach (var missileLauncher in missileLaunchers)
            {
                missileLauncher.UpdateTarget(targetSelection.GetCurrentTarget());
            }

        }
    }


}


