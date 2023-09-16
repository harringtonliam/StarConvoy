using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SC.UI
{
    public class CursorControl : MonoBehaviour
    {
        [SerializeField] CursorSetting[] cursorSettings;
        [SerializeField] CursorType defaultCursorType;
       


        [Serializable]
        public struct CursorSetting
        {
            public CursorType cursorType;
            public Texture2D cursorTexture2D;
            public Vector2 cursorHotspot;
        }

        private static CursorControl _instance;

        public static CursorControl Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void SetNewCursor(CursorType newCursorType)
        {
            for (int i = 0; i < cursorSettings.Length; i++)
            {
                if (cursorSettings[i].cursorType == newCursorType )
                {
                    SetCursor(cursorSettings[i]);
                    return;
                }
            }
        }


        private void SetCursor(CursorSetting cursorSetting)
        {
            Cursor.SetCursor(cursorSetting.cursorTexture2D, cursorSetting.cursorHotspot, CursorMode.Auto);
        }

    }

    public enum CursorType
    {
        UICursor,
        GameCursor
    }


}


