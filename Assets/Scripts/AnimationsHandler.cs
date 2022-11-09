using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsHandler : MonoBehaviour
{

    Animator myAnimator;
    void Start()
    {
        Player.changeRightLeftUpDown += ChangeRightLeftUpDown;
       
        myAnimator = GetComponent<Animator>();
    }


    void ChangeRightLeftUpDown(string Animation,float x,float y,bool a)
    {
        if (Animation == "Walking" && a == true)
        {
            myAnimator.SetFloat("moveX",x);
            myAnimator.SetFloat("moveY", y);
            myAnimator.SetBool("moving",a);
          

        }
        else if(Animation == "Walking" && a == false)
        {
            Debug.Log("aaa");
            myAnimator.SetBool("moving", a);
        }
       
        
    }
    



}
