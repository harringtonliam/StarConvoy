using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SC.Combat
{

    public class DelayedDestory : MonoBehaviour
    {
        [SerializeField] Health objectToDestroy;
        [SerializeField] float destroyDelaySeconds = 10f;

        float timePassed = 0f;

        // Start is called before the first frame update
        void Start()
        {
            timePassed = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            timePassed += Time.deltaTime;
            if (timePassed >= destroyDelaySeconds)
            {
                objectToDestroy.TakeDamage(objectToDestroy.CurrentHealth);
            }
        }
    }

}

