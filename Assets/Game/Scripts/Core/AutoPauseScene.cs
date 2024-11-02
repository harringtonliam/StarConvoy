using System.Collections;
using UnityEngine;

namespace SC.Core

{
    public class AutoPauseScene : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(AutoPause());
        }

        private IEnumerator AutoPause()
        {
            yield return new WaitForSeconds(0.01f);
            Time.timeScale = 0f;
        }
    }

}


