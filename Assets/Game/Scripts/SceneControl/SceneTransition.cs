using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SC.SceneControl
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] float fadeTime = 2f;
        [SerializeField] string sceneToLoad;

        SavingWrapper savingWrapper;
        Fader fader;

        private void Start()
        {
            savingWrapper = FindObjectOfType<SavingWrapper>();
        }

        public void StartNewGame(string saveGameName)
        {
            savingWrapper.DeleteDefaultSaveFile();
            TransitionToNextScene(saveGameName);
        }

        public void TransitionToNextScene(string saveGameName)
        {
            StartCoroutine(Transition(saveGameName));
        }


        private IEnumerator Transition(string saveGameName)
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            //yield return fader.FadeOut(fadeTime);
            savingWrapper.AutoSave();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            savingWrapper.Load();
            savingWrapper.AutoSave();
            savingWrapper.Save(saveGameName);
            yield return fader.FadeIn(fadeTime);
            Destroy(gameObject);
        }
    }

}
