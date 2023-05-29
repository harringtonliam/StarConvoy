using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SC.Combat;
using SC.Attributes;

namespace SC.UI
{
    public class ShipListUI : MonoBehaviour
    {
        [SerializeField] ShipDetailUI shipDetailPrefab;
        [SerializeField] TextMeshProUGUI listCountText;
        [SerializeField] bool fullList = true;
        [SerializeField] bool isSafe = true;

        GameObject player;
        CombatTarget playerCombatTarget;

        // Start is called before the first frame update
        void Start()
        {
             player = GameObject.FindGameObjectWithTag("Player");
             playerCombatTarget = player.GetComponent<CombatTarget>();
             Redraw();
            StartCoroutine(StartRedraw());
        }


        IEnumerator StartRedraw()
        {
            yield return new WaitForSeconds(2f);
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            SortedDictionary<string, CombatTarget> listToDisplay;
            if (fullList)
            {
                listToDisplay = TargetStore.Instance.CombatTargetsInFaction(playerCombatTarget.GetFaction());
            }
            else
            {
                listToDisplay = TargetStore.Instance.SafeCombatTargetsInFaction(playerCombatTarget.GetFaction(), isSafe);
            }

            if (listCountText != null)
            {
                listCountText.text = listToDisplay.Count.ToString();
            }

            foreach( var item in listToDisplay)
            {
                if (item.Value != playerCombatTarget)
                {
                    ShipInformation currentShip = item.Value.GetComponent<ShipInformation>();
                    if (currentShip != null)
                    {
                        var newShipDetailUI = Instantiate(shipDetailPrefab, transform);
                        var currentShipDetails = currentShip.GetShipDetails();
                        newShipDetailUI.SetUp(currentShipDetails.captainSprite, currentShipDetails.captainName, currentShipDetails.shipSprite, currentShipDetails.shipName, currentShipDetails.shipType);
                    }
                }
            }
        }
    }

}


