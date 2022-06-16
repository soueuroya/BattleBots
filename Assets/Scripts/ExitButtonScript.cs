using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
    public Animator anim;
    void OnMouseDown()
    {
        anim.SetTrigger("Exit");
    }
}
