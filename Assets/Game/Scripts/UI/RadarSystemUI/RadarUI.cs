using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using System;

namespace SC.UI.RadarSystemUI
{
    public class RadarUI : MonoBehaviour
    {
        [SerializeField] Transform targetLocatorAboveUI;
        [SerializeField] Transform targetLocatorBelowUI;
        [SerializeField] float xScale = 0.083f;
        [SerializeField] float zScale = 0.016f;
        [SerializeField] float yScale = 0.0066f;


        GameObject player;
        TargetLocator targetLocator;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            targetLocator = player.GetComponent<TargetLocator>();
        }

        // Update is called once per frame
        void Update()
        {
            PositionTargetLocator(targetLocator.TargetPositionRelativeToPlayer);
        }

        private void PositionTargetLocator(Vector3 targetPositionRelativeToPlayer)
        {
            Transform targetLocatorToUse = targetLocatorAboveUI;
            if(targetPositionRelativeToPlayer.y < 0f)
            {
                targetLocatorBelowUI.gameObject.SetActive(true);
                targetLocatorToUse = targetLocatorBelowUI;
                targetLocatorAboveUI.gameObject.SetActive(false);
            }
            else
            {
                targetLocatorAboveUI.gameObject.SetActive(true);
                targetLocatorToUse = targetLocatorAboveUI;
                targetLocatorBelowUI.gameObject.SetActive(false);
            }


            float xLocalPosition = Mathf.Clamp(targetPositionRelativeToPlayer.x * xScale, -250f, 250f);
            float yLocalPosition = Mathf.Clamp(targetPositionRelativeToPlayer.z * zScale, -100f, 100f);
            targetLocatorToUse.localPosition = new Vector3(xLocalPosition, yLocalPosition, 0f);

            float yLocalScale = Mathf.Clamp(Mathf.Abs(targetPositionRelativeToPlayer.y) * yScale, -20f, 20f);
            targetLocatorToUse.localScale = new Vector3(1f, yLocalScale, 1f);
        }


        private float ClampTargetLocator(float value)
        {

            return Mathf.Clamp(value, -250f, 250);
        }
    }

}


