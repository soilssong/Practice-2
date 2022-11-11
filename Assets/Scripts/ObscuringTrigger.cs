using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObscuringTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObscuringItem itemtoObscure;
        itemtoObscure = collision.gameObject.GetComponentInChildren<ObscuringItem>();
        if (itemtoObscure != null)
        {
            itemtoObscure.FadeOut();
        }


       
       




    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ObscuringItem itemtoObscure;
        itemtoObscure = other.gameObject.GetComponentInChildren<ObscuringItem>();

        if (itemtoObscure)
        {
            itemtoObscure.FadeIn();
        }
       
        
    }
}
