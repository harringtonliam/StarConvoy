using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SC.Movement;
using SC.Core;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;

namespace SC.UI
{
    public class MouseControlUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] float deadZoneRadius = 10f;
        [SerializeField] Rotate rotate;
        [SerializeField] TextMeshProUGUI xRotateText = null;
        [SerializeField] TextMeshProUGUI yRotateText = null;

        bool isMouseControlEnabled  = true;
 
        private Vector2 screenCenter;

        PlayerSettings playerSettings;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerSettings = player.GetComponent<PlayerSettings>();
            if (playerSettings != null)
            {
                playerSettings.onSettingsUpdated += EnableMouseControl;
            }

            EnableMouseControl();
        }

        private void OnDisable()
        {
            try
            {
                playerSettings.onSettingsUpdated -= EnableMouseControl;

            }
            catch (System.Exception)
            {
                Debug.Log("MouseControlUI Ondisable unable to unsubscribe playerSettings");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!isMouseControlEnabled) return;

            Vector2 mouse = Input.mousePosition;
            screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 centred = mouse - screenCenter;

            float distanceFromScreenCentre = Vector2.Distance(screenCenter, mouse);
            float horizontalInput = centred.x / (Screen.width / 2);
            float verticaInput = centred.y / (Screen.height / 2);

            //xRotateText.text = centred.x.ToString("####0.00000") + " / " + centred.y.ToString("####0.00000");
            //yRotateText.text = distanceFromScreenCentre.ToString();

            if (distanceFromScreenCentre > deadZoneRadius)
            {
                rotate.PerformRotation(horizontalInput, verticaInput * -1f);
            }
        }

        public void EnableMouseControl()

        {
            if (!PlayerPrefs.HasKey(PlayerSettings.MouseOrControllerKey))
            {
                isMouseControlEnabled = true;
            }
            else if (PlayerPrefs.GetString(PlayerSettings.MouseOrControllerKey) == PlayerSettings.UseMouseSetting)
            {
                isMouseControlEnabled = true;
            }
            else
            {
                isMouseControlEnabled = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }


}


