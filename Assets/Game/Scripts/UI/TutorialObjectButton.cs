using UnityEngine;
using UnityEngine.EventSystems;

namespace SC.UI
{
    public class TutorialObjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] GameObject tutorialObjectContainer;
        [SerializeField] TutorialObjectUI tutorialObjectUI;

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (tutorialObjectUI != null)
            {
                tutorialObjectUI.ApplyPlayerPrefs();
            }
            tutorialObjectContainer.SetActive(true);
        }



         void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            tutorialObjectContainer.SetActive(false);
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            tutorialObjectContainer.SetActive(false);
        }


    }
}


