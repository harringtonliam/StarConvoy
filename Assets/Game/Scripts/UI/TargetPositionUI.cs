using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using TMPro;


namespace SC.UI
{
    public class TargetPositionUI : MonoBehaviour
    {
        [SerializeField] Transform targetLocatorUI;
        [SerializeField] TextMeshProUGUI xAngleText;
        [SerializeField] TextMeshProUGUI yAngleText;
        [SerializeField] TextMeshProUGUI zDistanceText;


        GameObject player;
        TargetLocator targetLocator;
   

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            targetLocator = player.GetComponent<TargetLocator>();
        }

        void Update()
        {

            xAngleText.text = targetLocator.TargetPositionRelativeToPlayer.x.ToString("####0") + "/" + targetLocator.XAngleToTarget.ToString("##0");
            yAngleText.text = targetLocator.TargetPositionRelativeToPlayer.y.ToString("####0") + "/" + targetLocator.YAngleToTarget.ToString("##0");
            zDistanceText.text = targetLocator.TargetPositionRelativeToPlayer.z.ToString("####0") + "/" + targetLocator.ZAngleToTarget.ToString("##0");

            PositionTargetLocator(targetLocator.XAngleToTarget, targetLocator.YAngleToTarget);
        }

        private void PositionTargetLocator(float xAngle, float yAngle)
        {
            float postionOnXAxis = 0f;
            float postionOnYAxis = 0f;

            if (xAngle <= 90f  || xAngle >= -90f)
            {
                postionOnXAxis = (xAngle / 90f) * 100f;
            }
            else if (xAngle > 90f)
            {
                postionOnXAxis = ((180 - xAngle)/ 90f) * 100f;
            }
            else if (xAngle < -90f)
            {
                postionOnXAxis = ((180 + xAngle) / 90f) * 100f;
            }

            if (yAngle <= 90f || yAngle >= -90f)
            {
                postionOnYAxis = (yAngle / 90f) * -100f;
            }

            targetLocatorUI.localPosition = new Vector3(postionOnXAxis, postionOnYAxis, 0f);
        }
    }

}


