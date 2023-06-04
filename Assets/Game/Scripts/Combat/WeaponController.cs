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

        bool isEnabled;

        private void Start()
        {
            isEnabled = true;
            missileLauncherIndex = 0;
            targetSelection = GetComponent<TargetSelection>();
            targetSelection.CurrentTargetChanged += OnTargetChanged;

        }

        private void OnDestroy()
        {
            try
            {
                targetSelection.CurrentTargetChanged -= OnTargetChanged;
            }
            catch (Exception ex)
            {
                Debug.Log("WeaponController OnDestroy " + ex.Message);
            }
        }

        // Update is called once per frame
        void Update()
        {
            RespondToWeaponInput();
        }

        public void SetEnabled(bool enable)
        {
            isEnabled = enable;
        }

        public bool GetEnabled()
        {
            return isEnabled;
        }

        private void RespondToWeaponInput()
        {
            if (!isEnabled) return;

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


