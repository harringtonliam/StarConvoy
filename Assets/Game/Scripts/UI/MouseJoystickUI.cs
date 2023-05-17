using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;
using UnityEngine.EventSystems;

namespace SC.UI
{
    public class MouseJoystickUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] float moveDirectionY = 1f;
        [SerializeField] float moveDirectionX = 0f;
        [SerializeField] Rotate rotate;

        bool isMousePointerOver = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMousePointerOver = true;
            Debug.Log("OnpointerEnter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMousePointerOver = false;
            Debug.Log("Onpointerexit");
        }

        private void Update()
        {
            if (isMousePointerOver)
            {
                Debug.Log("MouseJoystickUI call to perform rotation");
                rotate.PerformRotation(moveDirectionX, moveDirectionY);
            }

        }
    }

}


