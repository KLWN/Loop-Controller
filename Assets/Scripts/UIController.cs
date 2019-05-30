using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

#pragma warning disable



public class UIController : MonoBehaviour
{
    public GameObject object1;
    public GameObject Object2;
    public GameObject Object3;
    public GameObject Object4;
    public GameObject Object5;
    public GameObject Object6;
    public GameObject Object7;
    public GameObject Object8;
    public GameObject Object9;
    public GameObject Object10;
    
    
    
    [Header("UI Elements:")]
    [Space(20)]
    public Text lowPassText;
    public Text bpmText;
    public Text pitchText;
    public Image imageMixerL;

    public Color keysHighlightColor = new Color32(50, 70, 90, 255);
    public Color textHighlightColor = new Color32(45, 55, 70, 255);
    public Color highlightColorL;
    


    // Start is called before the first frame update
    void Start()
    {
        
        // Assign Variables
        lowPassText = lowPassText.GetComponent<Text>();
        pitchText = pitchText.GetComponent<Text>();
        bpmText = bpmText.GetComponent<Text>();
        imageMixerL = imageMixerL.GetComponent<Image>();
    }

    private void Update()
    {
        highlightColorL = keysHighlightColor;
        highlightColorL.a = 0.25f;
        imageMixerL.color = highlightColorL;
    }
}
