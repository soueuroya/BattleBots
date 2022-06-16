using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonScript : MonoBehaviour
{
    public Animator anim;
    void OnMouseDown()
    {
        anim.SetTrigger("Back");
    }
}
