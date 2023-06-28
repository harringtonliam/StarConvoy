using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Attributes
{
    [CreateAssetMenu(menuName = ("Player/Player Information"))]
    public class PlayerInformation : ScriptableObject, ISerializationCallbackReceiver
    {
        [Tooltip("Auto-generated UUID for saving/loading. Clear this field if you want to generate a new one.")]
        [SerializeField] string playerinfoID = null;
        [Tooltip("Info name to be displayed in UI.")]
        [SerializeField] string displayName = null;
        [Tooltip("The UI image to represent the small character portrait.")]
        [SerializeField] Sprite portrait = null;
        [Tooltip("The UI imate to represent the small character image.")]
        [SerializeField] Sprite fullimage = null;


        static Dictionary<string, PlayerInformation> playerInfoLookupCache;

        public string PlayerinfoId {  get { return playerinfoID; } }
        public string DisplayName {  get { return displayName; } }
        public Sprite Portrait {  get { return portrait; } }
        public Sprite FullImage {  get { return fullimage; } }

        public static Dictionary<string, PlayerInformation> GetPlayerInfoLookUpCache()
        {
            if (playerInfoLookupCache == null)
            {
                BuildCache();
            }

            return playerInfoLookupCache;

        }

        private static void BuildCache()
        {
            playerInfoLookupCache = new Dictionary<string, PlayerInformation>();
            var playerInfoList = Resources.LoadAll<PlayerInformation>("");
            foreach (var playerinfo in playerInfoList)
            {
                if (playerInfoLookupCache.ContainsKey(playerinfo.playerinfoID))
                {
                    Debug.LogError(string.Format("Looks like there's a duplicate playerinfoID  for objects: {0} and {1}", playerInfoLookupCache[playerinfo.playerinfoID], playerinfo));
                    continue;
                }

                playerInfoLookupCache.Add(playerinfo.playerinfoID, playerinfo);
            }
        }

        public static PlayerInformation GetFromID(string playerinfoID)
        {
            if (playerInfoLookupCache == null)
            {
                BuildCache();
                
            }

            if (playerinfoID == null || !playerInfoLookupCache.ContainsKey(playerinfoID))
            {
                return null;
            }

            return playerInfoLookupCache[playerinfoID];
        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrWhiteSpace(playerinfoID))
            {
                playerinfoID = System.Guid.NewGuid().ToString();
            }
        }
    }


}


