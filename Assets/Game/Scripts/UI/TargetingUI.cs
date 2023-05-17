using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using UnityEngine.UI;

namespace SC.UI

{
    public class TargetingUI : MonoBehaviour
    {
        [SerializeField] Image targetAquiredImage;
        [SerializeField] Image missileLockAquiredImage;

        GameObject player;

        LaserWeapon[] laserWeapons;
        MissileLauncher[] missileLaunchers;

        private void OnEnable()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            WeaponController weaponController = player.GetComponent<WeaponController>();
            laserWeapons = weaponController.LaserWeapons;
            missileLaunchers = weaponController.MissileLaunchers;
            targetAquiredImage.gameObject.SetActive(false);
            missileLockAquiredImage.gameObject.SetActive(false);

            foreach (var laserWeapon in laserWeapons)
            {
                laserWeapon.TargetAquiredUpdated += SetTargetAquiredVisiblity;
            }

            foreach ( var missileLauncher in missileLaunchers)
            {
                missileLauncher.TargetAquiredUpdated += SetMissileLockAquiredVisibility;
            }
        }

        private void OnDisable()
        {
            foreach (var laserWeapon in laserWeapons)
            {
                laserWeapon.TargetAquiredUpdated -= SetTargetAquiredVisiblity;
            }

            foreach (var missileLauncher in missileLaunchers)
            {
                missileLauncher.TargetAquiredUpdated -= SetMissileLockAquiredVisibility;
            }
        }


        private void SetTargetAquiredVisiblity()
        {

            targetAquiredImage.gameObject.SetActive(false);
            foreach (var laserWeapon in laserWeapons)
            {
                if (laserWeapon.IsTargetAquired)
                {
                    targetAquiredImage.gameObject.SetActive(true);
                    return;
                }
            }

        }

        private void SetMissileLockAquiredVisibility()
        {

            missileLockAquiredImage.gameObject.SetActive(false);
            foreach (var missileLauncher in missileLaunchers)
            {
                if (missileLauncher.IsLockAquired)
                {
                    missileLockAquiredImage.gameObject.SetActive(true);
                    return;
                }
            }
        }
    }
}


