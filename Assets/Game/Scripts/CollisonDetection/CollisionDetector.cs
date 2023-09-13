using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using SC.Movement;

namespace SC.CollisionDetection
{

    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] float maxCollisionAvoidanceTime = 10f;


        AIMovementControl aIMovementControl;

        float timeSinceCollisionAvoidanceStarted = 0f;
        bool collisionAvoidanceStarted = false;

        // Start is called before the first frame update
        void Start()
        {
            aIMovementControl =  GetComponentInParent<AIMovementControl>();
        }

        // Update is called once per frame
        void Update()
        {
            if (collisionAvoidanceStarted)
            {
                timeSinceCollisionAvoidanceStarted += Time.deltaTime;
            }
            if (collisionAvoidanceStarted && timeSinceCollisionAvoidanceStarted >= maxCollisionAvoidanceTime)
            {
                StopCollisionAvoidance();
            }
        }



        private void OnTriggerEnter(Collider other)
        {
            StartCollisinAvoidance(other);

        }
        private void OnTriggerExit(Collider other)
        {
            StopCollisionAvoidance();
        }

        private void StartCollisinAvoidance(Collider other)
        {
            if (other.GetComponent<CombatTarget>() == null) return;
            if (other.gameObject.tag == "Player") return;

            var directionToOther = other.transform.position - transform.position;
            var awayFromTheOther = new Vector3(directionToOther.x * -1, directionToOther.y * -1, directionToOther.z * -1);
            aIMovementControl.SetCollisionAvoidanceDirection(awayFromTheOther, true);
            timeSinceCollisionAvoidanceStarted = 0f;
            collisionAvoidanceStarted = true;
        }

        private void StopCollisionAvoidance()
        {
            aIMovementControl.SetCollisionAvoidanceDirection(false);
            collisionAvoidanceStarted = false;
        }

    }

}

