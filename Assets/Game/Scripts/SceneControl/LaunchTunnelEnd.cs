using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.JumpGate;

namespace SC.SceneControl
{
    public class LaunchTunnelEnd : MonoBehaviour
    {


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") return;

            other.GetComponent<JumpGateBehaviour>().EnableDisablePlayerControls(true);

        }
    }

}


