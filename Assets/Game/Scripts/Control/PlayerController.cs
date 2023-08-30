using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Combat;
using SC.Movement;

namespace SC.Control
{
    public class PlayerController : MonoBehaviour
    {
        public void EnableDisablePlayerControl(bool enable)
        {
            if (gameObject.tag != "Player") return;
            SpeedControl speedControl = GetComponent<SpeedControl>();
            speedControl.SetEnabled(enable);
            Rotate rotate = GetComponent<Rotate>();
            rotate.SetEnabled(enable);
            WeaponController weaponController = GetComponent<WeaponController>();
            weaponController.SetEnabled(enable);
        }
    }
}

