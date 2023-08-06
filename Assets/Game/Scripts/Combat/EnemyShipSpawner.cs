using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace SC.Combat
{
    public class EnemyShipSpawner : MonoBehaviour
    {
        [SerializeField] float spawnStartDelayTime = 600f;
        [SerializeField] float spawnFrequecy = 90f;
        [SerializeField] SpawnCollection spawnCollection;
        [SerializeField] bool spawningEnabled = true;

        [Serializable]
        public struct SpawnCollection
        {
            public GameObject[] shipToSpawnPrefabs;
            public Transform[] spawnPoints;
            public CombatTarget[] combatTargets;
        }


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(StartSpawning());
        }

        private IEnumerator StartSpawning()
        {
            yield return new WaitForSeconds(spawnStartDelayTime);
            StartCoroutine(SpawnProcess());

        }

        private IEnumerator SpawnProcess()
        {
            while(spawningEnabled)
            {
                SpawnGameObjects();
                yield return new WaitForSeconds(spawnFrequecy);
            }

        }

        private void SpawnGameObjects()
        {
            for (int i = 0; i < spawnCollection.shipToSpawnPrefabs.Length; i++)
            {
                var newShip = GameObject.Instantiate(spawnCollection.shipToSpawnPrefabs[i], spawnCollection.spawnPoints[i].position, spawnCollection.spawnPoints[i].rotation);
                newShip.transform.parent = this.transform;
                if(spawnCollection.combatTargets[i] != null)
                {
                    newShip.GetComponent<AICombatControl>().SetCombatTarget(spawnCollection.combatTargets[i]);
                }
                CombatTarget newShipCombatTarget = newShip.GetComponent<CombatTarget>();
                TargetStore.Instance.AddTarget(newShipCombatTarget.GetUniqueIdentifier(), newShipCombatTarget);

            }
            TargetStore.Instance.OnTargetStoreUpdated();
        }
    }

}


