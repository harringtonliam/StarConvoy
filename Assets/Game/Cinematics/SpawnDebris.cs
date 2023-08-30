using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;
using SC.Control;
using SC.SceneControl;

namespace SC.Cinematics
{
    public class SpawnDebris : MonoBehaviour
    {
        [SerializeField] GameObject debrisPrefabToSpawn;
        [SerializeField] Transform debrisprefabSpawnPoint;
        [SerializeField] Transform positionShipAt;
        [SerializeField] float explosivePower = 50f;
        [SerializeField] float explosionRadius = 300f;
        [SerializeField] GameObject[] playerUIS;


        public void StopPlayerShip()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Move playerMove = player.GetComponent<Move>();
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.EnableDisablePlayerControl(false);
            playerMove.SetCurrentSpeed(0f);
        }

        public void PositionPlayerShip()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Move playerMove = player.GetComponent<Move>();
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.EnableDisablePlayerControl(false);
            player.transform.position = positionShipAt.position;
            player.transform.rotation = positionShipAt.rotation;
            playerMove.SetCurrentSpeed(playerMove.MaxSpeed);
        }

        public void ApplyExplosion()
        {
            Debug.Log("Apply explosion");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var playerRigidBody = player.GetComponent<Rigidbody>();
            playerRigidBody.AddExplosionForce(explosivePower, debrisprefabSpawnPoint.position, explosionRadius, -2.0f);
        }

        public void Spawn()
        {

            var newObject = Instantiate(debrisPrefabToSpawn, debrisprefabSpawnPoint.position, debrisprefabSpawnPoint.rotation);
            //newObject.transform.parent = this.transform;

            AIMovementControl aIMovementControl = newObject.GetComponent<AIMovementControl>();
        }

        public void HidePlayerUI()
        {
            ToggelPlayerUI(false);
        }

        public void ShowPlayerUI()
        {
            ToggelPlayerUI(true);
        }

        private void ToggelPlayerUI(bool isActive)
        {
            for (int i = 0; i < playerUIS.Length; i++)
            {
                playerUIS[i].SetActive(isActive);
            }
        }

        public void EndGame()
        {
            SceneController.Instance.EndScene();
        }


    }

}


