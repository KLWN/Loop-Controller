using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

#pragma warning disable


public class AudioMixerControllerV2 : MonoBehaviour
{
    private float calcBpmPitch;
    private float calcBpmPitchShifter;
    private float fadeInConversion;
    private float fadeOutConversion;
    
    private const float lowPassStandard = 5000f;
    private const float pitchStandard = 1f;
    private const float pitchshifterStandard = 1f;
    private const float bpmStandard = 1f;
    private const float volumeStandard = -80f;
    private const float volumeWhenOn = 0.0f;

    [Header("Debug Stuff:")] 
    public float FPS = 30f;
    public float FrameLengthLoop = 641f;
    public float InGameTime;
    public string InGameFrameCount;
    private float FrameCountFloat;

    [Header("Basic Settings:")]
    public float FadeInTime = 0.15f;
    public float FadeOutTime = 0.75f;
    public AudioMixer Mixer;
    public Color KeysHighlightColor = new Color32(50, 70, 90, 255);
    public Color TextHighlightColor = new Color32(45, 55, 70, 255);
    
    [Space(5)]
    public bool pitchFilter = false;
    [Range(0.5f, 1.5f)] public float pitchRange = 1.2f;
    [Space(5)]
    public bool lowPassFilter = false;
    [Range(250f, 5000f)] public float lowPassRange = 1500f;
    [Space(5)]
    public bool bpmFilter = false;
    [Range(-0.5f, 1.5f)] public float bpmRange = 1.2f;


    [Header("UI Elements:")] 
    public Text LowPassText;
    public Text BPMText;
    public Text PitchText;
    public Image ImageMixer1;
    private bool volumeMixer1 = false;
    private Color HighlightColor1;
    private float timer1 = 0.0f;
    private float fadeValue1 = 0.0f;
    private float currentValue1 = -80f;
    private int InputCount1 = 0;

    private bool volumeMixer2 = false;
    public Image ImageMixer2;
    private Color HighlightColor2;
    private float timer2 = 0.0f;
    private float fadeValue2 = 0.0f;
    private float currentValue2 = -80f;
    private int InputCount2 = 0;

    private bool volumeMixer3 = false;
    public Image ImageMixer3;
    private Color HighlightColor3;
    private float timer3 = 0.0f;
    private float fadeValue3 = 0.0f;
    private float currentValue3 = -80f;
    private int InputCount3 = 0;

    private bool volumeMixer4 = false;
    public Image ImageMixer4;
    private Color HighlightColor4;
    private float timer4 = 0.0f;
    private float fadeValue4 = 0.0f;
    private float currentValue4 = -80f;
    private int InputCount4 = 0;

    private bool volumeMixer5 = false;
    public Image ImageMixer5;
    private Color HighlightColor5;
    private float timer5 = 0.0f;
    private float fadeValue5 = 0.0f;
    private float currentValue5 = -80f;
    private int InputCount5 = 0;

    private bool volumeMixer6 = false;
    public Image ImageMixer6;
    private Color HighlightColor6;
    private float timer6 = 0.0f;
    private float fadeValue6 = 0.0f;
    private float currentValue6 = -80f;
    private int InputCount6 = 0;

    private bool volumeMixer7 = false;
    public Image ImageMixer7;
    private Color HighlightColor7;
    private float timer7 = 0.0f;
    private float fadeValue7 = 0.0f;
    private float currentValue7 = -80f;
    private int InputCount7 = 0;

    private bool volumeMixer8 = false;
    public Image ImageMixer8;
    private Color HighlightColor8;
    private float timer8 = 0.0f;
    private float fadeValue8 = 0.0f;
    private float currentValue8 = -80f;
    private int InputCount8 = 0;

    private bool volumeMixer9 = false;
    public Image ImageMixer9;
    private Color HighlightColor9;
    private float timer9 = 0.0f;
    private float fadeValue9 = 0.0f;
    private float currentValue9 = -80f;
    private int InputCount9 = 0;

    private bool volumeMixerK = false;
    public Image ImageMixerK;
    private Color HighlightColorK;
    private float timerK = 0.0f;
    private float fadeValueK = 0.0f;
    private float currentValueK = -80f;
    private int InputCountK = 0;

    private bool volumeMixerL = false;
    public Image ImageMixerL;
    private Color HighlightColorL;
    private float timerL = 0.0f;
    private float fadeValueL = 0.0f;
    private float currentValueL = -80f;
    private int InputCountL = 0;
    


    public void Awake()
    {
        FrameCountFloat = InGameTime * FPS;
    }


    public void Update()
    {
        
        //Frame Zähler:
        InGameFrameCount = FrameCountFloat.ToString("0");
        InGameTime += Time.deltaTime;
        
        if (FrameCountFloat != FrameLengthLoop)
        {
            FrameCountFloat = InGameTime * FPS;
        }
        else
        {
            InGameTime = 0f;
            FrameCountFloat = 0f;
        }
        
        
        
        // Input Listener
        // Später auslagern in eigene Class!!
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lowPassFilter = !lowPassFilter;
            
            if (lowPassFilter)
            {
                LowPassText.GetComponent<Text>().color = TextHighlightColor;
                Mixer.SetFloat("LowPassMaster", lowPassRange);
            }
            else
            {
                LowPassText.GetComponent<Text>().color = Color.white;
                Mixer.SetFloat("LowPassMaster", lowPassStandard);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pitchFilter = !pitchFilter;
            
            if (pitchFilter)
            {
                PitchText.GetComponent<Text>().color = TextHighlightColor;
                Mixer.SetFloat("PitchMaster", pitchRange);
            }
            else
            {
                PitchText.GetComponent<Text>().color = Color.white;
                Mixer.SetFloat("PitchMaster", pitchStandard);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bpmFilter = !bpmFilter;
            
            if (bpmFilter)
            {
                BPMText.GetComponent<Text>().color = TextHighlightColor;
                Mixer.SetFloat("PitchMaster", calcBpmPitch);
                Mixer.SetFloat("PitchShifterMaster", calcBpmPitchShifter);
            }
            else
            {
                BPMText.GetComponent<Text>().color = Color.white;
                Mixer.SetFloat("PitchMaster", pitchStandard);
                Mixer.SetFloat("PitchShifterMaster", pitchshifterStandard);
            }
        }
        
        
        
        // Berechnung BPM
        calcBpmPitch = bpmStandard * bpmRange;
        calcBpmPitchShifter = bpmStandard / bpmRange;
        
        
        // Berechnung Fade In/Out
        fadeInConversion = 1f / FadeInTime;
        fadeOutConversion = 1f / FadeOutTime;
        


        // Mixer Aktivierung + Fade-In/Out
        // Mixer 1
        if (volumeMixer1)
        {
            HighlightColor1 = KeysHighlightColor;
            HighlightColor1.a = 1f;
            ImageMixer1.GetComponent<Image>().color = HighlightColor1;
            timer1 += Time.deltaTime * fadeInConversion;
            fadeValue1 = Mathf.Lerp(currentValue1, volumeWhenOn, timer1);
            Mixer.SetFloat("Volume1", fadeValue1);
        } 
        
        if (!volumeMixer1 && InputCount1 > 0)
        {
            HighlightColor1 = KeysHighlightColor;
            HighlightColor1.a = 0.25f;
            ImageMixer1.GetComponent<Image>().color = HighlightColor1;
            timer1 += Time.deltaTime * fadeOutConversion;
            fadeValue1 = Mathf.Lerp(currentValue1, volumeStandard, timer1);
            Mixer.SetFloat("Volume1", fadeValue1);
        }

        // Mixer 2
        if (volumeMixer2)
        {
            HighlightColor2 = KeysHighlightColor;
            HighlightColor2.a = 1f;
            ImageMixer2.GetComponent<Image>().color = HighlightColor2;
            timer2 += Time.deltaTime * fadeInConversion;
            fadeValue2 = Mathf.Lerp(currentValue2, volumeWhenOn, timer2);
            Mixer.SetFloat("Volume2", fadeValue2);
        } 
        
        if (!volumeMixer2 && InputCount2 > 0)
        {
            HighlightColor2 = KeysHighlightColor;
            HighlightColor2.a = 0.25f;
            ImageMixer2.GetComponent<Image>().color = HighlightColor2;
            timer2 += Time.deltaTime * fadeOutConversion;
            fadeValue2 = Mathf.Lerp(currentValue2, volumeStandard, timer2);
            Mixer.SetFloat("Volume2", fadeValue2);
        }
        
        // Mixer 3
        if (volumeMixer3)
        {
            HighlightColor3 = KeysHighlightColor;
            HighlightColor3.a = 1f;
            ImageMixer3.GetComponent<Image>().color = HighlightColor3;
            timer3 += Time.deltaTime * fadeInConversion;
            fadeValue3 = Mathf.Lerp(currentValue3, volumeWhenOn, timer3);
            Mixer.SetFloat("Volume3", fadeValue3);
        } 
        
        if (!volumeMixer3 && InputCount3 > 0)
        {
            HighlightColor3 = KeysHighlightColor;
            HighlightColor3.a = 0.25f;
            ImageMixer3.GetComponent<Image>().color = HighlightColor3;
            timer3 += Time.deltaTime * fadeOutConversion;
            fadeValue3 = Mathf.Lerp(currentValue3, volumeStandard, timer3);
            Mixer.SetFloat("Volume3", fadeValue3);
        }
        
        // Mixer 4
        if (volumeMixer4)
        {
            HighlightColor4 = KeysHighlightColor;
            HighlightColor4.a = 1f;
            ImageMixer4.GetComponent<Image>().color = HighlightColor4;
            timer4 += Time.deltaTime * fadeInConversion;
            fadeValue4 = Mathf.Lerp(currentValue4, volumeWhenOn, timer4);
            Mixer.SetFloat("Volume4", fadeValue4);
        } 
        
        if (!volumeMixer4 && InputCount4 > 0)
        {   
            HighlightColor4 = KeysHighlightColor;
            HighlightColor4.a = 0.25f;
            ImageMixer4.GetComponent<Image>().color = HighlightColor4;
            timer4 += Time.deltaTime * fadeOutConversion;
            fadeValue4 = Mathf.Lerp(currentValue4, volumeStandard, timer4);
            Mixer.SetFloat("Volume4", fadeValue4);
        }
        
        // Mixer 5
        if (volumeMixer5)
        {
            HighlightColor5 = KeysHighlightColor;
            HighlightColor5.a = 1f;
            ImageMixer5.GetComponent<Image>().color = HighlightColor5;
            timer5 += Time.deltaTime * fadeInConversion;
            fadeValue5 = Mathf.Lerp(currentValue5, volumeWhenOn, timer5);
            Mixer.SetFloat("Volume5", fadeValue5);
        } 
        
        if (!volumeMixer5 && InputCount5 > 0)
        {
            HighlightColor5 = KeysHighlightColor;
            HighlightColor5.a = 0.25f;
            ImageMixer5.GetComponent<Image>().color = HighlightColor5;
            timer5 += Time.deltaTime * fadeOutConversion;
            fadeValue5 = Mathf.Lerp(currentValue5, volumeStandard, timer5);
            Mixer.SetFloat("Volume5", fadeValue5);
        }
        
        // Mixer 6
        if (volumeMixer6)
        {
            HighlightColor6 = KeysHighlightColor;
            HighlightColor6.a = 1f;
            ImageMixer6.GetComponent<Image>().color = HighlightColor6;
            timer6 += Time.deltaTime * fadeInConversion;
            fadeValue6 = Mathf.Lerp(currentValue6, volumeWhenOn, timer6);
            Mixer.SetFloat("Volume6", fadeValue6);
        } 
        
        if (!volumeMixer6 && InputCount6 > 0)
        {
            HighlightColor6 = KeysHighlightColor;
            HighlightColor6.a = 0.25f;
            ImageMixer6.GetComponent<Image>().color = HighlightColor6;
            timer6 += Time.deltaTime * fadeOutConversion;
            fadeValue6 = Mathf.Lerp(currentValue6, volumeStandard, timer6);
            Mixer.SetFloat("Volume6", fadeValue6);
        }
        
        // Mixer 7
        if (volumeMixer7)
        {
            HighlightColor7 = KeysHighlightColor;
            HighlightColor7.a = 1f;
            ImageMixer7.GetComponent<Image>().color = HighlightColor7;
            timer7 += Time.deltaTime * fadeInConversion;
            fadeValue7 = Mathf.Lerp(currentValue7, volumeWhenOn, timer7);
            Mixer.SetFloat("Volume7", fadeValue7);
        } 
        
        if (!volumeMixer7 && InputCount7 > 0)
        {
            HighlightColor7 = KeysHighlightColor;
            HighlightColor7.a = 0.25f;
            ImageMixer7.GetComponent<Image>().color = HighlightColor7;
            timer7 += Time.deltaTime * fadeOutConversion;
            fadeValue7 = Mathf.Lerp(currentValue7, volumeStandard, timer7);
            Mixer.SetFloat("Volume7", fadeValue7);
        }
        
        // Mixer 8
        if (volumeMixer8)
        {
            HighlightColor8 = KeysHighlightColor;
            HighlightColor8.a = 1f;
            ImageMixer8.GetComponent<Image>().color = HighlightColor8;
            timer8 += Time.deltaTime * fadeInConversion;
            fadeValue8 = Mathf.Lerp(currentValue8, volumeWhenOn, timer8);
            Mixer.SetFloat("Volume8", fadeValue8);
        } 
        
        if (!volumeMixer8 && InputCount8 > 0)
        {
            HighlightColor8 = KeysHighlightColor;
            HighlightColor8.a = 0.25f;
            ImageMixer8.GetComponent<Image>().color = HighlightColor8;
            timer8 += Time.deltaTime * fadeOutConversion;
            fadeValue8 = Mathf.Lerp(currentValue8, volumeStandard, timer8);
            Mixer.SetFloat("Volume8", fadeValue8);
        }
        
        // Mixer 9
        if (volumeMixer9)
        {
            HighlightColor9 = KeysHighlightColor;
            HighlightColor9.a = 1f;
            ImageMixer9.GetComponent<Image>().color = HighlightColor9;
            timer9 += Time.deltaTime * fadeInConversion;
            fadeValue9 = Mathf.Lerp(currentValue9, volumeWhenOn, timer9);
            Mixer.SetFloat("Volume9", fadeValue9);
        } 
        
        if (!volumeMixer9 && InputCount9 > 0)
        {
            HighlightColor9 = KeysHighlightColor;
            HighlightColor9.a = 0.25f;
            ImageMixer9.GetComponent<Image>().color = HighlightColor9;
            timer9 += Time.deltaTime * fadeOutConversion;
            fadeValue9 = Mathf.Lerp(currentValue9, volumeStandard, timer9);
            Mixer.SetFloat("Volume9", fadeValue9);
        }
        
        // Mixer K
        if (volumeMixerK)
        {
            HighlightColorK = KeysHighlightColor;
            HighlightColorK.a = 1f;
            ImageMixerK.GetComponent<Image>().color = HighlightColorK;
            timerK += Time.deltaTime * fadeInConversion;
            fadeValueK = Mathf.Lerp(currentValueK, volumeWhenOn, timerK);
            Mixer.SetFloat("Volume11", fadeValueK);
        } 
        
        if (!volumeMixerK && InputCountK > 0)
        {
            HighlightColorK = KeysHighlightColor;
            HighlightColorK.a = 0.25f;
            ImageMixerK.GetComponent<Image>().color = HighlightColorK;
            timerK += Time.deltaTime * fadeOutConversion;
            fadeValueK = Mathf.Lerp(currentValueK, volumeStandard, timerK);
            Mixer.SetFloat("Volume11", fadeValueK);
        }
        
        // Mixer L
        if (volumeMixerL)
        {
            HighlightColorL = KeysHighlightColor;
            HighlightColorL.a = 1f;
            ImageMixerL.GetComponent<Image>().color = HighlightColorL;
            timerL += Time.deltaTime * fadeInConversion;
            fadeValueL = Mathf.Lerp(currentValueL, volumeWhenOn, timerL);
            Mixer.SetFloat("Volume12", fadeValueL);
        } 
        
        if (!volumeMixerL && InputCountL > 0)
        {
            HighlightColorL = KeysHighlightColor;
            HighlightColorL.a = 0.25f;
            ImageMixerL.GetComponent<Image>().color = HighlightColorL;
            timerL += Time.deltaTime * fadeOutConversion;
            fadeValueL = Mathf.Lerp(currentValueL, volumeStandard, timerL);
            Mixer.SetFloat("Volume12", fadeValueL);
        }
        
        
        
        
        // Input Listener
        // Später auslagern in eigene Class!!
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (InputCount1 == 0) 
            {
                currentValue1 = volumeStandard;
            }
            else
            {
                currentValue1 = fadeValue1;
            }
            
            fadeValue1 = 0.0f;
            timer1 = 0.0f;
            InputCount1++;
            volumeMixer1 = !volumeMixer1;
        }
        
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (InputCount2 == 0) 
            {
                currentValue2 = volumeStandard;
            }
            else
            {
                currentValue2 = fadeValue2;
            }
            
            fadeValue2 = 0.0f;
            timer2 = 0.0f;
            InputCount2++;
            volumeMixer2 = !volumeMixer2;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (InputCount3 == 0) 
            {
                currentValue3 = volumeStandard;
            }
            else
            {
                currentValue3 = fadeValue3;
            }
            
            fadeValue3 = 0.0f;
            timer3 = 0.0f;
            InputCount3++;
            volumeMixer3 = !volumeMixer3;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (InputCount4 == 0) 
            {
                currentValue4 = volumeStandard;
            }
            else
            {
                currentValue4 = fadeValue4;
            }
            
            fadeValue4 = 0.0f;
            timer4 = 0.0f;
            InputCount4++;
            volumeMixer4 = !volumeMixer4;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (InputCount5 == 0) 
            {
                currentValue5 = volumeStandard;
            }
            else
            {
                currentValue5 = fadeValue5;
            }
            
            fadeValue5 = 0.0f;
            timer5 = 0.0f;
            InputCount5++;
            volumeMixer5 = !volumeMixer5;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (InputCount6 == 0) 
            {
                currentValue6 = volumeStandard;
            }
            else
            {
                currentValue6 = fadeValue6;
            }
            
            fadeValue6 = 0.0f;
            timer6 = 0.0f;
            InputCount6++;
            volumeMixer6 = !volumeMixer6;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (InputCount7 == 0) 
            {
                currentValue7 = volumeStandard;
            }
            else
            {
                currentValue7 = fadeValue7;
            }
            
            fadeValue7 = 0.0f;
            timer7 = 0.0f;
            InputCount7++;
            volumeMixer7 = !volumeMixer7;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (InputCount8 == 0) 
            {
                currentValue8 = volumeStandard;
            }
            else
            {
                currentValue8 = fadeValue8;
            }
            
            fadeValue8 = 0.0f;
            timer8 = 0.0f;
            InputCount8++;
            volumeMixer8 = !volumeMixer8;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (InputCount9 == 0) 
            {
                currentValue9 = volumeStandard;
            }
            else
            {
                currentValue9 = fadeValue9;
            }
            
            fadeValue9 = 0.0f;
            timer9 = 0.0f;
            InputCount9++;
            volumeMixer9 = !volumeMixer9;
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (InputCountK == 0) 
            {
                currentValueK = volumeStandard;
            }
            else
            {
                currentValueK = fadeValueK;
            }
            
            fadeValueK = 0.0f;
            timerK = 0.0f;
            InputCountK++;
            volumeMixerK = !volumeMixerK;
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (InputCountL == 0) 
            {
                currentValueL = volumeStandard;
            }
            else
            {
                currentValueL = fadeValueL;
            }
            
            fadeValueL = 0.0f;
            timerL = 0.0f;
            InputCountL++;
            volumeMixerL = !volumeMixerL;
        }

        
    }

}
