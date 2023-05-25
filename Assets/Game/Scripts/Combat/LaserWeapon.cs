using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace SC.Combat
{
    public class LaserWeapon : MonoBehaviour
    {
        [SerializeField] float damage = 10f;
        [SerializeField] float timeBetweenShots = 1f;
        [SerializeField] float range = 500f;
        [SerializeField] ParticleSystem muzzleFlash;
        [SerializeField] AudioClip firingSound;
        [SerializeField] GameObject hitVFX;
        [SerializeField] CombatTarget currentTarget;
        [SerializeField] float maxTemperature = 100f;
        [SerializeField] float temperatureIncreasePerShot = 5f;
        [SerializeField] float temperatureCooldownRate = 1f;
        [SerializeField] float temperatureCooldownTime = 2f;

        bool canShoot = true;
        float currentTemperature = 0f;

        private CombatTarget targetInSights = null;

        bool isTargetAquired = false;

        public  event Action TargetAquiredUpdated;

        public float Range { get { return range; } }
        
        public bool IsTargetAquired {  get { return isTargetAquired; } }

        private float timeSinceTemperatureReduction = 0f;

        public float GetCurrentTemperature()
        {
            return currentTemperature;
        }

        public float GetMaxTemperature()
        {
            return maxTemperature;
        }

        public event Action temperatureUpdated;

        private void Start()
        {
            SignalTemperatureUpdated();
        }

        void Update()
        {
            CheckTargeting();
            ReduceTemperature();
        }

        private void  ReduceTemperature()
        {
            timeSinceTemperatureReduction += Time.deltaTime;
            if (timeSinceTemperatureReduction < temperatureCooldownTime) return;
            if (currentTemperature < 0f)
            {
                currentTemperature = 0f;
            }
            if (Mathf.Approximately(currentTemperature, 0f)) return;

            currentTemperature = Mathf.Clamp(currentTemperature - temperatureCooldownRate, 0f, GetMaxTemperature());
            timeSinceTemperatureReduction = 0f; 
            if (currentTemperature < 0f)
            {
                currentTemperature = 0f;
            }
            SignalTemperatureUpdated();

        }

        public void Shoot()
        {
            if (canShoot && TemperatureOkToSoot())
            {
                StartCoroutine(Shooting());
            }
        }

        public void UpdateTarget(CombatTarget target)
        {
            currentTarget = target;
        }

        public CombatTarget GetTargetInSights()
        {
            return targetInSights;
        }

        private IEnumerator Shooting()
        {
            Debug.Log("SHooting");
            canShoot = false;
            PlayVFX();
            PlayFiringSound();
            ProcessRayCast();
            currentTemperature += temperatureIncreasePerShot;
            SignalTemperatureUpdated();
            yield return new WaitForSeconds(timeBetweenShots);
            canShoot = true;

        }

        private bool TemperatureOkToSoot()
        {
            return currentTemperature < maxTemperature;
        }

        private void ProcessRayCast()
        {
            RaycastHit hit;
            bool raycastForward = Physics.Raycast(transform.position, transform.forward, out hit, range);

            if (raycastForward)
            {
                CreateHitImpact(hit);
                Health target = hit.transform.GetComponent<Health>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

            }
        }

        private void CreateHitImpact(RaycastHit hit)
        {
            if (hitVFX == null) return;
            GameObject hitEffect = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitEffect, 0.5f);
        }

        private void PlayFiringSound()
        {
            if (firingSound != null)
            {
                AudioSource.PlayClipAtPoint(firingSound, transform.position);
            }
        }

        private void PlayVFX()
        {
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
        }

        private void CheckTargeting()
        {
            bool hasTargetBeenAquired = false;

            if (currentTarget == null) hasTargetBeenAquired = false;

            RaycastHit hit;
            bool raycastForward = Physics.Raycast(transform.position, transform.forward, out hit, range);

            if (raycastForward)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                targetInSights = target;
                if (target != null && target == currentTarget)
                {
                    hasTargetBeenAquired = true;
                }
            }

            if (hasTargetBeenAquired != isTargetAquired)
            {
                isTargetAquired = hasTargetBeenAquired;
                if (TargetAquiredUpdated != null)
                {
                    TargetAquiredUpdated();
                }
            }


        }

        private void SignalTemperatureUpdated()
        {
            if (temperatureUpdated != null)
            {
                temperatureUpdated();
            }
        }


    }
}


