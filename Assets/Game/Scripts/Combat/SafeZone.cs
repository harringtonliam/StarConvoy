using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Movement;


namespace SC.Combat
{

    public class SafeZone : MonoBehaviour
    {
        [SerializeField] Faction faction;
        [SerializeField] bool ignorePlayer = true;
        [SerializeField] bool bringToStopOnEnter = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            CombatTarget combatTarget = other.GetComponent<CombatTarget>();
            if (combatTarget == null) return;
            if (combatTarget.GetFaction() != faction) return;
            if (combatTarget.gameObject.tag == "Player" && ignorePlayer) return;

            combatTarget.SetIsSafe(true);

            BringToAStop(other);

        }

        private void BringToAStop(Collider other)
        {
            Debug.Log("Safe zone bring to a stop");
            Move move = other.GetComponent<Move>();
            if (bringToStopOnEnter && move != null)
            {
                other.GetComponent<Move>().ChangeSpeed(0f);
            }
        }
    }
}

