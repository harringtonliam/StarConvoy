using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;
using SC.JumpGate;

namespace SC.Cinematics
{
    public class SpawnDebris : MonoBehaviour
    {
        [SerializeField] GameObject debrisPrefabToSpawn;
        [SerializeField] Transform debrisprefabSpawnPoint;
        [SerializeField] Transform positionShipAt;
        [SerializeField] float explosivePower = 50f;
        [SerializeField] float explosionRadius = 300f;


        public void PositionPlayerShip()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Move playerMove = player.GetComponent<Move>();
            JumpGateBehaviour jumpGateBehaviour = player.GetComponent<JumpGateBehaviour>();
            jumpGateBehaviour.EnableDisablePlayerControls(false);
            player.transform.position = positionShipAt.position;
            player.transform.rotation = positionShipAt.rotation;
            playerMove.SetCurrentSpeed(playerMove.MaxSpeed);
        }

        public void ApplyExplosion()
        {
            Debug.Log("Apply explosion");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var playerRigidBody = player.GetComponent<Rigidbody>();
            playerRigidBody.AddExplosionForce(explosivePower, debrisprefabSpawnPoint.position, explosionRadius, 3.0f);
        }

        public void Spawn()
        {

            var newObject = Instantiate(debrisPrefabToSpawn, debrisprefabSpawnPoint.position, debrisprefabSpawnPoint.rotation);
            //newObject.transform.parent = this.transform;

            AIMovementControl aIMovementControl = newObject.GetComponent<AIMovementControl>();



        }


    }

}


