using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SC.Saving;
using System;


namespace SC.SceneControl
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeTime = 0.2f;
        [SerializeField] Fader fader;

        const string defaultSaveFile = "autosave";
        const string quickSaveFile = "quicksave";

        public event Action onSaveUpated;

        string mostRecentSavedGame;

        public string MostRecentSavedGame { get {return  mostRecentSavedGame; } }

         private void Start()
        {

        }

        private IEnumerator LoadLastScene(string savedGame)
        {
            yield return  GetComponent<SavingSystem>().LoadLastScene(savedGame);
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeTime); ;
        }
    
        public void LoadSavedGame(string savedGame)
        {
            StartCoroutine(LoadLastScene(savedGame));
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);  
        }

        public void Save(string fileName)
        {
            mostRecentSavedGame = fileName;
            GetComponent<SavingSystem>().Save(fileName);
            if (onSaveUpated!= null)
            {
                onSaveUpated();
            }
        }

        public void QuickSave()
        {
            GetComponent<SavingSystem>().Save(quickSaveFile);
            if (onSaveUpated != null)
            {
                onSaveUpated();
            }
        }

        public void AutoSave()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
            if (onSaveUpated != null)
            {
                onSaveUpated();
            }
        }

        public void Delete(string filename)
        {
            GetComponent<SavingSystem>().Delete(filename);
            if (onSaveUpated != null)
            {
                onSaveUpated();
            }
        }

        public void DeleteDefaultSaveFile()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
            if (onSaveUpated != null)
            {
                onSaveUpated();
            }
        }


        public Dictionary<string, DateTime> ListSaveFiles()
        {

            Dictionary<string, DateTime> allSaveFiles = GetComponent<SavingSystem>().ListAllSaveFiles();
            if (allSaveFiles.ContainsKey(defaultSaveFile))
            {
                allSaveFiles.Remove(defaultSaveFile);
            }

            return allSaveFiles;

        }
    }



}
