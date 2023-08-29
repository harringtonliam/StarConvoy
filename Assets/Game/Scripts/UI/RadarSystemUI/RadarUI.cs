using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SC.Combat;
using SC.Attributes;
using System;

namespace SC.UI.RadarSystemUI
{
    public class RadarUI : MonoBehaviour
    {
        [SerializeField] Transform targetLocatorUI;
        [SerializeField] Transform targetLocatorAboveUI;
        [SerializeField] Transform targetLocatorAboveTopUI;
        [SerializeField] Transform targetLocatorBelowUI;
        [SerializeField] TextMeshProUGUI distanceText;
        [SerializeField] float xScale = 0.083f;
        [SerializeField] float zScale = 0.016f;
        [SerializeField] float yScale = 0.0066f;


        GameObject player;
        TargetLocator targetLocator;
        TargetSelection targetSelection;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            targetLocator = player.GetComponent<TargetLocator>();
            targetSelection = player.GetComponent<TargetSelection>();
        }

        // Update is called once per frame
        void Update()
        {
            PositionTargetLocator(targetLocator.TargetPositionRelativeToPlayer);
            DisplayDistance(targetLocator.TargetPositionRelativeToPlayer);
        }

        private void DisplayDistance(Vector3 targetPositionRelativeToPlayer)
        {
            float distanceToTarget = Vector3.Distance(new Vector3(0f, 0f, 0f), targetPositionRelativeToPlayer);
            distanceText.text = distanceToTarget.ToString("########0");
        }

        private void PositionTargetLocator(Vector3 targetPositionRelativeToPlayer)
        {
            Transform targetLocatorToUse = targetLocatorAboveUI;
            if(targetPositionRelativeToPlayer.y < 0f)
            {

                targetLocatorToUse = targetLocatorBelowUI;
                targetLocatorAboveUI.localScale= new Vector3(1f, 0.1f, 1f);
            }
            else
            {
                targetLocatorToUse = targetLocatorAboveUI;
                targetLocatorBelowUI.localScale = new Vector3(1f, 0.1f, 1f);
            }


            float xLocalPosition = Mathf.Clamp(targetPositionRelativeToPlayer.x * xScale, -250f, 250f);
            float yLocalPosition = Mathf.Clamp(targetPositionRelativeToPlayer.z * zScale, -100f, 100f);
            targetLocatorUI.localPosition = new Vector3(xLocalPosition, yLocalPosition, 0f);

            float yLocalScale = Mathf.Clamp(Mathf.Abs(targetPositionRelativeToPlayer.y) * yScale, -20f, 20f);
            targetLocatorToUse.localScale = new Vector3(1f, yLocalScale, 1f);
        }


        private float ClampTargetLocator(float value)
        {

            return Mathf.Clamp(value, -250f, 250);
        }
    }

}


