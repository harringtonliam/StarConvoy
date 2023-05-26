using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC.Combat
{
    public class Radar : MonoBehaviour
    {
        CombatTarget combatTarget;

        // Start is called before the first frame update
        void Start()
        {
            combatTarget = GetComponent<CombatTarget>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetEnemyCombatTargets();
            }
        }

        private void GetEnemyCombatTargets()
        {
            var enemyCombatTargets = TargetStore.Instance.CombatTargetsNotInFaction(combatTarget.GetFaction());
            foreach (var item in enemyCombatTargets)
            {
                Debug.Log("Enemey found " + item.Value.gameObject.name);
            }
        }
    }
}


