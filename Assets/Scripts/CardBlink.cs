using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBlink : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            //Do something when animator isn't playing
            {
                Destroy(gameObject);
            }
        }



    }
}
