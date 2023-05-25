using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{

    public class TargetLocator : MonoBehaviour
    {

        CombatTarget currentTarget;
        TargetSelection targetSelection;

        Vector3 targetPositionRelativeToPlayer;
        float xAngleToTarget;
        float yAngleToTarget;
        float zAngleToTarget;


        public Vector3 TargetPositionRelativeToPlayer { get { return targetPositionRelativeToPlayer; } }
        public float XAngleToTarget { get { return xAngleToTarget; } }
        public float YAngleToTarget { get { return yAngleToTarget; } }
        public float ZAngleToTarget { get { return zAngleToTarget; } }


        // Start is called before the first frame update
        void Start()
        {
            targetSelection = GetComponent<TargetSelection>();
        }

        // Update is called once per frame
        void Update()
        {
            currentTarget = targetSelection.GetCurrentTarget();
            if (currentTarget == null) return;

            targetPositionRelativeToPlayer = transform.InverseTransformPoint(currentTarget.transform.position);

            Vector3 directionToTarget = currentTarget.transform.position - transform.position;
            Vector3 projectedVectorForY = Vector3.ProjectOnPlane(directionToTarget, transform.right);
            Vector3 projectedVectorForX = Vector3.ProjectOnPlane(directionToTarget, transform.up);
            yAngleToTarget = Vector3.SignedAngle(transform.forward, projectedVectorForY, transform.right);
            xAngleToTarget = Vector3.SignedAngle(transform.forward, projectedVectorForX, transform.up); //Moving left and right rotates around the y axis

        }
    }
}