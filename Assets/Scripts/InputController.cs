using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

#pragma warning disable


public class InputController : MonoBehaviour
{
    public bool Activated1 { get; private set; }

    

    private void Awake()
    {
        Activated1 = false;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Activated1 = !Activated1;
        }
        
        
    }
}