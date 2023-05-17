using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SC.Combat
{
    public class MissileLauncher : MonoBehaviour
    {
        [SerializeField] GameObject missilePrefab;
        [SerializeField] float targetingRange;
        [SerializeField] float timeBetweenShots = 2f;
        [SerializeField] int maxNumberOfMissiles = 2;
        [SerializeField] float secondsNeededForMissileLock = 2f;
        [SerializeField] float secondsLockLastsFor = 10f;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Transform targetingPoint;
        [SerializeField] Health currentTarget;

        private int currentNumberOfMissiles;
        bool canShoot = true;
        bool isLockAquired;
        bool isLockingStarted;

        float secondsSpentAquiringLock;
        float secondsSinceLockAquired;

        public event Action TargetAquiredUpdated;

        public bool IsLockAquired {  get { return isLockAquired; } }

        // Start is called before the first frame update
        void Start()
        {
            secondsSpentAquiringLock = 0f;
            secondsSinceLockAquired = 0f;
            isLockingStarted = false;
            isLockAquired = false;
            currentNumberOfMissiles = maxNumberOfMissiles;
        }

        // Update is called once per frame
        void Update()
        {
            CheckTargeting();
        }


        public void Shoot()
        {
            if (canShoot && isLockAquired)
            {
                StartCoroutine(Shooting());
            }
        }

        private IEnumerator Shooting()
        {
            canShoot = false;

            if (currentNumberOfMissiles > 0)
            {
                currentNumberOfMissiles--;
            }

            GameObject newMissile = GameObject.Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);
            newMissile.GetComponent<AICombatControl>().SetCombatTarget(currentTarget.gameObject);
            yield return new WaitForSeconds(timeBetweenShots);
            canShoot = true;

        }

        private void CheckTargeting()
        {
            bool hasTargetBeenAquired = false;

            if (currentTarget == null) hasTargetBeenAquired = false;

            RaycastHit hit;
            bool raycastForward = Physics.Raycast(targetingPoint.position, transform.forward, out hit, targetingRange);

            if (raycastForward)
            {
                Health target = hit.transform.GetComponent<Health>();
                if (target != null && target == currentTarget)
                {
                    if (!isLockingStarted)
                    {
                        secondsSpentAquiringLock = 0f;
                    }
                    isLockingStarted = true;
                }
            }

            if (isLockingStarted)
            {
                secondsSpentAquiringLock += Time.deltaTime;
            }

            if (secondsSpentAquiringLock >= secondsNeededForMissileLock)
            {
                secondsSinceLockAquired = 0f;
                hasTargetBeenAquired = true;
            }

            if (hasTargetBeenAquired != isLockAquired)
            {
                isLockAquired = hasTargetBeenAquired;
                if (TargetAquiredUpdated != null)
                {
                    TargetAquiredUpdated();
                }
            }

            if (isLockAquired)
            {
                secondsSinceLockAquired = Time.deltaTime;
            }

            if ((secondsSinceLockAquired > secondsLockLastsFor  && isLockAquired)  || (currentTarget == null && isLockAquired))
            {
                isLockAquired = false;
                TargetAquiredUpdated();
            }

        }
    }

}


