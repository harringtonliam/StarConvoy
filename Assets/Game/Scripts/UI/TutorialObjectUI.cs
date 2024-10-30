using UnityEngine;
using SC.Core;

namespace SC.UI
{

    public class TutorialObjectUI : MonoBehaviour
    {
        [SerializeField] GameObject defaultTextObject;
        [SerializeField] GameObject gameControllerTextObject;
        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ApplyPlayerPrefs();
        }

        public void ApplyPlayerPrefs()
        {
            if (PlayerPrefs.GetString(PlayerSettings.MouseOrControllerKey) == PlayerSettings.UseControllerSetting &&
                gameControllerTextObject != null)
            {
                gameControllerTextObject.SetActive(true);
                defaultTextObject.SetActive(false);
            }
            else
            {
                gameControllerTextObject.SetActive(false);
                defaultTextObject.SetActive(true);
            }
        }
    }

}


