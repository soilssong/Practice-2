using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsHandler : MonoBehaviour
{

    Animator myAnimator;
    void Start()
    {
        Player.changeAnimation += Change;

        myAnimator = GetComponent<Animator>();
    }

   
    void Update()
    {
        
    }

    void Change(string Animation,bool b)
    {
        if (Animation == "Running")
        {
            myAnimator.SetBool("Running",b);
           
        }
        
    }

    
}
