using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.SceneControl;
using System;

namespace SC.UI.SavingSystem
{

    public class SavedGamesUI : MonoBehaviour
    {
        [SerializeField] SaveGameUI loadGameGamePrefab = null;

        SavingWrapper savingWrapper = null;

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.onSaveUpated += Redraw;
            Redraw();

        }

        public void Redraw()
        {
            if (savingWrapper == null) return;

            Dictionary<string, DateTime> saveFiles = savingWrapper.ListSaveFiles();

            try
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (var saveFile in saveFiles)
                {
                    var savedGameGameUI = Instantiate(loadGameGamePrefab, transform);
                    savedGameGameUI.Setup(saveFile.Key, saveFile.Value.ToString());
                }
            }
            catch (Exception)
            {

                Debug.Log("Unable to update SaveGameUI");
            }

        }

        private void OnDestroy()
        {
            if (savingWrapper != null)
            {
                savingWrapper.onSaveUpated -= Redraw;
            }
            
        }


    }

}


