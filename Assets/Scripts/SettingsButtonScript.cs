using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonScript : MonoBehaviour
{
    public Animator anim;
    void OnMouseDown()
    {
        anim.SetTrigger("Config");
    }
}
