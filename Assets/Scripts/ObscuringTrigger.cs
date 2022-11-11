using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObscuringTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObscuringItem[] itemstoObscure = collision.gameObject.GetComponentsInChildren<ObscuringItem>();
        if (itemstoObscure.Length>0)
        {
            for (int i = 0; i < itemstoObscure.Length; i++)
            {
                itemstoObscure[i].FadeOut();
                Debug.Log("fadeout");
            }
        }

        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ObscuringItem[] itemstoObscure = other.gameObject.GetComponentsInChildren<ObscuringItem>();
        if (itemstoObscure.Length > 0)
        {
            for (int i = 0; i < itemstoObscure.Length; i++)
            {
                itemstoObscure[i].FadeIn();
                Debug.Log("fadein");
            }
        }
    }
}
