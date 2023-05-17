using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] Health currentTarget;


        bool canShoot = true;

  
        bool isTargetAquired = false;

        public  event Action TargetAquiredUpdated;

        public float Range { get { return range; } }
        
        public bool IsTargetAquired {  get { return isTargetAquired; } }



        void Update()
        {
            CheckTargeting();
        }

        public void Shoot()
        {
            if (canShoot)
            {
                StartCoroutine(Shooting());
            }
        }

        public void UpdateTarget(Health target)
        {
            currentTarget = target;
        }

        private IEnumerator Shooting()
        {
            Debug.Log("SHooting");
            canShoot = false;
            PlayVFX();
            PlayFiringSound();
            ProcessRayCast();

            yield return new WaitForSeconds(timeBetweenShots);
            canShoot = true;

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
                Health target = hit.transform.GetComponent<Health>();
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


    }
}


