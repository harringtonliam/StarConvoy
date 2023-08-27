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

        public void StartNewGame()
        {
            savingWrapper.DeleteDefaultSaveFile();
            TransitionToNextScene();
        }

        public void TransitionToNextScene()
        {
            StartCoroutine(Transition(GetSaveGameName()));
        }

        private string GetSaveGameName()
        {
            return sceneToLoad;
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
