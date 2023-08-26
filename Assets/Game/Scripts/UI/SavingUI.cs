using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.SceneControl;


namespace SC.UI
{
    public class SavingUI : MonoBehaviour
    {
        [SerializeField] GameObject uiCanvas;
        [SerializeField] float displayPeriod = 2f;

        private void Start()
        {
           StartCoroutine(SavingGameUIProcess());
        }



        private IEnumerator SavingGameUIProcess()
        {
            Debug.Log("***SHow save UI");
            ShowSavingGameUI(true);
            yield return new WaitForSeconds(1f);
            Debug.Log("***Hide save UI");
            ShowSavingGameUI(false);
        }


        private void ShowSavingGameUI(bool showUI)
        {
            uiCanvas.SetActive(showUI);
        }
    }

}


