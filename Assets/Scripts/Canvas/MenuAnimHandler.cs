using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimHandler : MonoBehaviour
{
    public Animator menuAnim;

    public void StartLightning()
    {
        menuAnim.SetTrigger("PlayLightning");
    }
}
