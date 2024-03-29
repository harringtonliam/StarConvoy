using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SC.UI.Tabs
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;
        public Sprite tabIdle;
        public Sprite tabHover;
        public Sprite tabActive;
        public TabButton selectedTab;
        public List<GameObject> objectsToSwap;

        private void Start()
        {
            OnTabSelected(selectedTab);
        }

        public void Subscribe(TabButton button)
        {
            if (tabButtons == null)
            {
                tabButtons = new List<TabButton>();
            }
            tabButtons.Add(button);
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab)
            {
                button.backGround.sprite = tabHover;
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab)
            {
                button.backGround.sprite = tabIdle;
            }

        }   
        
        public void OnTabSelected(TabButton button)
        {
            Debug.Log("OnTabSelected " + button.transform.GetSiblingIndex().ToString());
            selectedTab = button;
            ResetTabs();
            button.backGround.sprite = tabActive;
            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                if (index == i)
                {
                    objectsToSwap[i].SetActive(true);
                }
                else
                {
                    objectsToSwap[i].SetActive(false);
                }
            }
        }

        public void ResetTabs()
        {
            Debug.Log("reset " + selectedTab.name);
            foreach (TabButton button in tabButtons)
            {
                if (selectedTab != null && button != selectedTab)
                {
                    button.backGround.sprite = tabIdle;
                }

            }
        }


    }

}


