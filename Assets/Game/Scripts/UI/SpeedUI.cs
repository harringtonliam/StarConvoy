using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;
using TMPro;

namespace SC.UI
{
    public class SpeedUI : MonoBehaviour
    {


        [SerializeField] TextMeshProUGUI moveValueText;
        [SerializeField] RectTransform foregroudnHealthBar;

        GameObject player;
        Move playerMove;


        // Start is called before the first frame update
        void Start()
        {

            player = GameObject.FindGameObjectWithTag("Player");
            playerMove = player.GetComponent<Move>();
            playerMove.speedUpdated += Redraw;
            Redraw();
        }

        private void OnDisable()
        {
            playerMove.speedUpdated -= Redraw;
        }

        private void Redraw()
        {
            moveValueText.text = playerMove.CurrentSpeed.ToString("###0");
            if (foregroudnHealthBar == null) return;
            Vector3 newScale = new Vector3(playerMove.CurrentSpeed / playerMove.MaxSpeed, 1, 1);
            foregroudnHealthBar.localScale = newScale;

        }
    }

}
