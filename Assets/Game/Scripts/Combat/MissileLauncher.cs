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
        [SerializeField] CombatTarget currentTarget;
        [SerializeField] AudioSource firingAudioSource;

        private int currentNumberOfMissiles;
        bool canShoot = true;
        bool isLockAquired;
        bool isLockingStarted;

        float secondsSpentAquiringLock;
        float secondsSinceLockAquired;

        public event Action TargetAquiredUpdated;
        public event Action missileNumberUpdated;

        public bool IsLockAquired {  get { return isLockAquired; } }
        public int CurrentNumberOfMissiles {  get { return currentNumberOfMissiles; } }
        public int MaxNumberOfMissiles { get { return maxNumberOfMissiles; } }

        // Start is called before the first frame update
        void Start()
        {
            secondsSpentAquiringLock = 0f;
            secondsSinceLockAquired = 0f;
            isLockingStarted = false;
            isLockAquired = false;
            currentNumberOfMissiles = maxNumberOfMissiles;
            MissileNumberUpdate();
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
        public void UpdateTarget(CombatTarget target)
        {
            currentTarget = target;
            isLockAquired = false;
            secondsSpentAquiringLock = 0f;
            OnTargetAquiredUpdated();
        }

        private IEnumerator Shooting()
        {
            canShoot = false;

            if (currentNumberOfMissiles > 0)
            {
                GameObject newMissile = GameObject.Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);
                PlayFiringSound();
                if (currentTarget == null || currentTarget.gameObject == null)
                {
                    Debug.Log("Misslie Launcher unable to shoot");
                }
                else
                {
                    newMissile.GetComponent<AICombatControl>().SetCombatTarget(currentTarget);
                    currentNumberOfMissiles--;
                    MissileNumberUpdate();
                }

            }

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
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target != null && target == currentTarget && !target.DenyMissileLock)
                {
                    if (!isLockingStarted)
                    {
                        secondsSpentAquiringLock = 0f;
                    }
                    isLockingStarted = true;
                }
                else
                {
                    isLockingStarted = false;
                    secondsSpentAquiringLock = 0f;
                }
            }
            else
            {
                isLockingStarted = false;
                secondsSpentAquiringLock = 0f;
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

            if (hasTargetBeenAquired && !isLockAquired)
            {
                isLockAquired = hasTargetBeenAquired;
                OnTargetAquiredUpdated();
            }

            if (isLockAquired)
            {
                secondsSinceLockAquired += Time.deltaTime;
            }

            if ((secondsSinceLockAquired > secondsLockLastsFor  && isLockAquired)  || (currentTarget == null && isLockAquired))
            {
                isLockAquired = false;
                OnTargetAquiredUpdated();
            }

        }

        private void MissileNumberUpdate()
        {
            if (missileNumberUpdated != null)
            {
                missileNumberUpdated();
            }
        }

        private void OnTargetAquiredUpdated()
        {
            if (TargetAquiredUpdated != null)
            {
                TargetAquiredUpdated();
            }

        }

        private void PlayFiringSound()
        {
            if (firingAudioSource != null)
            {
                firingAudioSource.Play();
            }
        }
    }

}


