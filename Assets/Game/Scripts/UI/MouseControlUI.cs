using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SC.Movement;

namespace SC.UI
{
    public class MouseControlUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] float deadZoneRadius = 10f;
        [SerializeField] Rotate rotate;




        bool isMousePointerOver = false;

        private Vector2 screenCenter;

        private void Awake()
        {
            screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector2 mouse = Input.mousePosition;

            Vector2 centred = mouse - screenCenter;

            float distanceFromScreenCentre = Vector2.Distance(screenCenter, mouse);
            float horizontalInput = centred.x / (Screen.width / 2);
            float verticaInput = centred.y / (Screen.height / 2);

            if (distanceFromScreenCentre > deadZoneRadius)
            {
                rotate.PerformRotation(horizontalInput, verticaInput * -1f);
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


