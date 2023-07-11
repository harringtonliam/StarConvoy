using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using SC.Movement;

namespace SC.CollisionDetection
{

    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] GameObject parent;


        AIMovementControl aIMovementControl;

        // Start is called before the first frame update
        void Start()
        {
            aIMovementControl =  GetComponentInParent<AIMovementControl>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CombatTarget>() == null) return;
            if (other.gameObject.tag == "Player") return;

            var directionToOther = other.transform.position - transform.position;
            var awayFromTheOther = new Vector3(directionToOther.x * -1, directionToOther.y * -1, directionToOther.z * -1);
            aIMovementControl.SetCollisionAvoidanceDirection(awayFromTheOther, true);

        }


        private void OnTriggerExit(Collider other)
        {
            aIMovementControl.SetCollisionAvoidanceDirection(false);
        }
    }

}

