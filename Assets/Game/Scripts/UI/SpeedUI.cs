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
            try
            {
                playerMove.speedUpdated -= Redraw;
            }
            catch (System.Exception ex)
            {
                Debug.Log("SpeedUI OnDisable" + ex.Message);
            }
            
        }

        private void Redraw()
        {
            if (playerMove.CurrentSpeed <= playerMove.MaxSpeed)
            {
                moveValueText.text = playerMove.CurrentSpeed.ToString("###0");
            }
            else
            {
                moveValueText.text = string.Empty;
            }
           
            if (foregroudnHealthBar == null) return;
            Vector3 newScale = new Vector3(Mathf.Clamp(playerMove.CurrentSpeed / playerMove.MaxSpeed, 0f, 1f), 1, 1);
            foregroudnHealthBar.localScale = newScale;

        }
    }

}
