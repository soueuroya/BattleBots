using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerButtonScript : MonoBehaviour
{
    public Animator anim;
    void OnMouseDown()
    {
        anim.SetTrigger("Single");
    }
}
