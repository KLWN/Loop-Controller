using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

#pragma warning disable
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]




public class AudioMixerControllerV3 : MonoBehaviour
{
    private float _calcBpmPitch;
    private float _calcBpmPitchShifter;
    private float _fadeInConversion;
    private float _fadeOutConversion;

    private const float LowPassStandard = 5000f;
    private const float PitchStandard = 1f;
    private const float PitchshifterStandard = 1f;
    private const float BpmStandard = 1f;
    private const float VolumeStandard = -80f;
    private const float VolumeWhenOn = 0.0f;

    [Header("Debug Stuff:")] public int fps = 30;
    public int frameLengthLoops = 641;
    public int frameOffset = 20;
    public int schlaufen = 0;
    public int frameFixAnimParam = 1;
    
    
    public double dspTime;
    private double dspStartTime;
    
    public int frameFixAnimSpeed = 4;
    public float FrameFixMultiplier;
    public float CurrentAnimSpeed;
   
    public float inGameFrames;
    
    
    
    

    [Header("Basic Settings:")] public float fadeInTime = 0.15f;
    public float fadeOutTime = 0.75f;
    public float timeToWaitMixerL = 0.5f;
    private float _waitTimerMixerL = 0.0f;
    public AudioMixer mixer;
    public GameObject puppe;
    private Animator _animatorPuppe;
    public AudioSource audioSource;
    public Color keysHighlightColor = new Color32(50, 70, 90, 255);
    public Color textHighlightColor = new Color32(45, 55, 70, 255);

    [Space(5)] public bool pitchFilter = false;
    [Range(0.5f, 1.5f)] public float pitchRange = 1.2f;
    [Space(5)] public bool lowPassFilter = false;
    [Range(250f, 5000f)] public float lowPassRange = 1500f;
    [Space(5)] public bool bpmFilter = false;
    [Range(-0.5f, 1.5f)] public float bpmRange = 1.2f;


    [Header("UI Elements:")] public Text lowPassText;
    public Text bpmText;
    public Text pitchText;
    public Image imageMixer1;

    private bool _mixerActivated1 = false;
    private Color _highlightColor1;
    private float _timer1 = 0.0f;
    private float _fadeValue1 = 0.0f;
    private float _currentValue1 = -80f;
    private int _inputCount1 = 0;

    private bool _mixerActivated2 = false;
    public Image imageMixer2;
    private Color _highlightColor2;
    private float _timer2 = 0.0f;
    private float _fadeValue2 = 0.0f;
    private float _currentValue2 = -80f;
    private int _inputCount2 = 0;

    private bool _mixerActivated3 = false;
    public Image imageMixer3;
    private Color _highlightColor3;
    private float _timer3 = 0.0f;
    private float _fadeValue3 = 0.0f;
    private float _currentValue3 = -80f;
    private int _inputCount3 = 0;

    private bool _mixerActivated4 = false;
    public Image imageMixer4;
    private Color _highlightColor4;
    private float _timer4 = 0.0f;
    private float _fadeValue4 = 0.0f;
    private float _currentValue4 = -80f;
    private int _inputCount4 = 0;

    private bool _mixerActivated5 = false;
    public Image imageMixer5;
    private Color _highlightColor5;
    private float _timer5 = 0.0f;
    private float _fadeValue5 = 0.0f;
    private float _currentValue5 = -80f;
    private int _inputCount5 = 0;

    private bool _mixerActivated6 = false;
    public Image imageMixer6;
    private Color _highlightColor6;
    private float _timer6 = 0.0f;
    private float _fadeValue6 = 0.0f;
    private float _currentValue6 = -80f;
    private int _inputCount6 = 0;

    private bool _mixerActivated7 = false;
    public Image imageMixer7;
    private Color _highlightColor7;
    private float _timer7 = 0.0f;
    private float _fadeValue7 = 0.0f;
    private float _currentValue7 = -80f;
    private int _inputCount7 = 0;

    private bool _mixerActivated8 = false;
    public Image imageMixer8;
    private Color _highlightColor8;
    private float _timer8 = 0.0f;
    private float _fadeValue8 = 0.0f;
    private float _currentValue8 = -80f;
    private int _inputCount8 = 0;

    private bool _mixerActivated9 = false;
    public Image imageMixer9;
    private Color _highlightColor9;
    private float _timer9 = 0.0f;
    private float _fadeValue9 = 0.0f;
    private float _currentValue9 = -80f;
    private int _inputCount9 = 0;

    private bool _mixerActivatedK = false;
    public Image imageMixerK;
    private Color _highlightColorK;
    private float _timerK = 0.0f;
    private float _fadeValueK = 0.0f;
    private float _currentValueK = -80f;
    private int _inputCountK = 0;

    private bool _mixerActivatedL = false;
    public Image imageMixerL;
    private Color _highlightColorL;
    private float _timerL = 0.0f;
    private float _fadeValueL = 0.0f;
    private float _currentValueL = -80f;
    private int _inputCountL = 0;
    
    



    private void Start()
    {

        _animatorPuppe = puppe.GetComponent<Animator>();

        dspStartTime = AudioSettings.dspTime;
        audioSource.PlayScheduled(dspStartTime);

        // Berechnung BPM
        _calcBpmPitch = BpmStandard * bpmRange;
        _calcBpmPitchShifter = BpmStandard / bpmRange;


        // Berechnung Fade In/Out
        _fadeInConversion = 1f / fadeInTime;
        _fadeOutConversion = 1f / fadeOutTime;


        // Assign Variables
        lowPassText = lowPassText.GetComponent<Text>();
        pitchText = pitchText.GetComponent<Text>();
        bpmText = bpmText.GetComponent<Text>();
        imageMixer1 = imageMixer1.GetComponent<Image>();
        imageMixer2 = imageMixer2.GetComponent<Image>();
        imageMixer3 = imageMixer3.GetComponent<Image>();
        imageMixer4 = imageMixer4.GetComponent<Image>();
        imageMixer5 = imageMixer5.GetComponent<Image>();
        imageMixer6 = imageMixer6.GetComponent<Image>();
        imageMixer7 = imageMixer7.GetComponent<Image>();
        imageMixer8 = imageMixer8.GetComponent<Image>();
        imageMixer9 = imageMixer9.GetComponent<Image>();
        imageMixerK = imageMixerK.GetComponent<Image>();
        imageMixerL = imageMixerL.GetComponent<Image>();
    }


    public void Update()
    {
        
        // Animator Parameters
        _animatorPuppe.SetBool("Activated1", _mixerActivatedL);
        _animatorPuppe.SetInteger("Sound1", Mathf.FloorToInt(inGameFrames) + frameOffset);
        
        
        // dspTime Berechnung + FrameFix für Animator Parameters
        dspTime = AudioSettings.dspTime - dspStartTime;
        inGameFrames = ((float)dspTime * fps) - (schlaufen * frameLengthLoops) - (frameFixAnimParam - 1);

        if (frameLengthLoops <= inGameFrames)
        {
            schlaufen++;
            frameFixAnimParam++;
        }
        

        // Animator Video Speed Fix
        FrameFixMultiplier = (((float)frameFixAnimParam / frameFixAnimSpeed) / (float)fps) / 100f;
        _animatorPuppe.speed = 1f - FrameFixMultiplier;
        
        CurrentAnimSpeed = _animatorPuppe.speed;

 
        
        
        
        // Input Listener
        // Später eigene Class!!!
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _mixerActivated1 = !_mixerActivated1;
            KeyPressed1();
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _mixerActivated2 = !_mixerActivated2;
            KeyPressed2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _mixerActivated3 = !_mixerActivated3;
            KeyPressed3();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _mixerActivated4 = !_mixerActivated4;
            KeyPressed4();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _mixerActivated5 = !_mixerActivated5;
            KeyPressed5();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _mixerActivated6 = !_mixerActivated6;
            KeyPressed6();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _mixerActivated7 = !_mixerActivated7;
            KeyPressed7();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _mixerActivated8 = !_mixerActivated8;
            KeyPressed8();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _mixerActivated9 = !_mixerActivated9;
            KeyPressed9();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _mixerActivatedK = !_mixerActivatedK;
            KeyPressedK();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            _mixerActivatedL = !_mixerActivatedL;
            KeyPressedL();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lowPassFilter = !lowPassFilter;
            KeyPressedUp();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pitchFilter = !pitchFilter;
            KeyPressedRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bpmFilter = !bpmFilter;
            KeyPressedLeft();
        }


        
        // Fade-In/Out Controller
        
        // Mixer 1
        if (_mixerActivated1)
        {
            _highlightColor1 = keysHighlightColor;
            _highlightColor1.a = 1f;
            imageMixer1.color = _highlightColor1;
            _timer1 += Time.deltaTime * _fadeInConversion;
            _fadeValue1 = Mathf.Lerp(_currentValue1, VolumeWhenOn, _timer1);
            mixer.SetFloat("Volume1", _fadeValue1);
        }

        if (!_mixerActivated1 && _inputCount1 > 0)
        {
            _highlightColor1 = keysHighlightColor;
            _highlightColor1.a = 0.25f;
            imageMixer1.color = _highlightColor1;
            _timer1 += Time.deltaTime * _fadeOutConversion;
            _fadeValue1 = Mathf.Lerp(_currentValue1, VolumeStandard, _timer1);
            mixer.SetFloat("Volume1", _fadeValue1);
        }


        // Mixer 2
        if (_mixerActivated2)
        {
            _highlightColor2 = keysHighlightColor;
            _highlightColor2.a = 1f;
            imageMixer2.color = _highlightColor2;
            _timer2 += Time.deltaTime * _fadeInConversion;
            _fadeValue2 = Mathf.Lerp(_currentValue2, VolumeWhenOn, _timer2);
            mixer.SetFloat("Volume2", _fadeValue2);
        }

        if (!_mixerActivated2 && _inputCount2 > 0)
        {
            _highlightColor2 = keysHighlightColor;
            _highlightColor2.a = 0.25f;
            imageMixer2.color = _highlightColor2;
            _timer2 += Time.deltaTime * _fadeOutConversion;
            _fadeValue2 = Mathf.Lerp(_currentValue2, VolumeStandard, _timer2);
            mixer.SetFloat("Volume2", _fadeValue2);
        }


        // Mixer 3
        if (_mixerActivated3)
        {
            _highlightColor3 = keysHighlightColor;
            _highlightColor3.a = 1f;
            imageMixer3.color = _highlightColor3;
            _timer3 += Time.deltaTime * _fadeInConversion;
            _fadeValue3 = Mathf.Lerp(_currentValue3, VolumeWhenOn, _timer3);
            mixer.SetFloat("Volume3", _fadeValue3);
        }

        if (!_mixerActivated3 && _inputCount3 > 0)
        {
            _highlightColor3 = keysHighlightColor;
            _highlightColor3.a = 0.25f;
            imageMixer3.color = _highlightColor3;
            _timer3 += Time.deltaTime * _fadeOutConversion;
            _fadeValue3 = Mathf.Lerp(_currentValue3, VolumeStandard, _timer3);
            mixer.SetFloat("Volume3", _fadeValue3);
        }


        // Mixer 4
        if (_mixerActivated4)
        {
            _highlightColor4 = keysHighlightColor;
            _highlightColor4.a = 1f;
            imageMixer4.color = _highlightColor4;
            _timer4 += Time.deltaTime * _fadeInConversion;
            _fadeValue4 = Mathf.Lerp(_currentValue4, VolumeWhenOn, _timer4);
            mixer.SetFloat("Volume4", _fadeValue4);
        }

        if (!_mixerActivated4 && _inputCount4 > 0)
        {
            _highlightColor4 = keysHighlightColor;
            _highlightColor4.a = 0.25f;
            imageMixer4.color = _highlightColor4;
            _timer4 += Time.deltaTime * _fadeOutConversion;
            _fadeValue4 = Mathf.Lerp(_currentValue4, VolumeStandard, _timer4);
            mixer.SetFloat("Volume4", _fadeValue4);
        }


        // Mixer 5
        if (_mixerActivated5)
        {
            _highlightColor5 = keysHighlightColor;
            _highlightColor5.a = 1f;
            imageMixer5.color = _highlightColor5;
            _timer5 += Time.deltaTime * _fadeInConversion;
            _fadeValue5 = Mathf.Lerp(_currentValue5, VolumeWhenOn, _timer5);
            mixer.SetFloat("Volume5", _fadeValue5);
        }

        if (!_mixerActivated5 && _inputCount5 > 0)
        {
            _highlightColor5 = keysHighlightColor;
            _highlightColor5.a = 0.25f;
            imageMixer5.color = _highlightColor5;
            _timer5 += Time.deltaTime * _fadeOutConversion;
            _fadeValue5 = Mathf.Lerp(_currentValue5, VolumeStandard, _timer5);
            mixer.SetFloat("Volume5", _fadeValue5);
        }


        // Mixer 6
        if (_mixerActivated6)
        {
            _highlightColor6 = keysHighlightColor;
            _highlightColor6.a = 1f;
            imageMixer6.color = _highlightColor6;
            _timer6 += Time.deltaTime * _fadeInConversion;
            _fadeValue6 = Mathf.Lerp(_currentValue6, VolumeWhenOn, _timer6);
            mixer.SetFloat("Volume6", _fadeValue6);
        }

        if (!_mixerActivated6 && _inputCount6 > 0)
        {
            _highlightColor6 = keysHighlightColor;
            _highlightColor6.a = 0.25f;
            imageMixer6.color = _highlightColor6;
            _timer6 += Time.deltaTime * _fadeOutConversion;
            _fadeValue6 = Mathf.Lerp(_currentValue6, VolumeStandard, _timer6);
            mixer.SetFloat("Volume6", _fadeValue6);
        }


        // Mixer 7
        if (_mixerActivated7)
        {
            _highlightColor7 = keysHighlightColor;
            _highlightColor7.a = 1f;
            imageMixer7.color = _highlightColor7;
            _timer7 += Time.deltaTime * _fadeInConversion;
            _fadeValue7 = Mathf.Lerp(_currentValue7, VolumeWhenOn, _timer7);
            mixer.SetFloat("Volume7", _fadeValue7);
        }

        if (!_mixerActivated7 && _inputCount7 > 0)
        {
            _highlightColor7 = keysHighlightColor;
            _highlightColor7.a = 0.25f;
            imageMixer7.color = _highlightColor7;
            _timer7 += Time.deltaTime * _fadeOutConversion;
            _fadeValue7 = Mathf.Lerp(_currentValue7, VolumeStandard, _timer7);
            mixer.SetFloat("Volume7", _fadeValue7);
        }


        // Mixer 8
        if (_mixerActivated8)
        {
            _highlightColor8 = keysHighlightColor;
            _highlightColor8.a = 1f;
            imageMixer8.color = _highlightColor8;
            _timer8 += Time.deltaTime * _fadeInConversion;
            _fadeValue8 = Mathf.Lerp(_currentValue8, VolumeWhenOn, _timer8);
            mixer.SetFloat("Volume8", _fadeValue8);
        }

        if (!_mixerActivated8 && _inputCount8 > 0)
        {
            _highlightColor8 = keysHighlightColor;
            _highlightColor8.a = 0.25f;
            imageMixer8.color = _highlightColor8;
            _timer8 += Time.deltaTime * _fadeOutConversion;
            _fadeValue8 = Mathf.Lerp(_currentValue8, VolumeStandard, _timer8);
            mixer.SetFloat("Volume8", _fadeValue8);
        }


        // Mixer 9
        if (_mixerActivated9)
        {
            _highlightColor9 = keysHighlightColor;
            _highlightColor9.a = 1f;
            imageMixer9.color = _highlightColor9;
            _timer9 += Time.deltaTime * _fadeInConversion;
            _fadeValue9 = Mathf.Lerp(_currentValue9, VolumeWhenOn, _timer9);
            mixer.SetFloat("Volume9", _fadeValue9);
        }

        if (!_mixerActivated9 && _inputCount9 > 0)
        {
            _highlightColor9 = keysHighlightColor;
            _highlightColor9.a = 0.25f;
            imageMixer9.color = _highlightColor9;
            _timer9 += Time.deltaTime * _fadeOutConversion;
            _fadeValue9 = Mathf.Lerp(_currentValue9, VolumeStandard, _timer9);
            mixer.SetFloat("Volume9", _fadeValue9);
        }


        // Mixer K
        if (_mixerActivatedK)
        {
            _highlightColorK = keysHighlightColor;
            _highlightColorK.a = 1f;
            imageMixerK.color = _highlightColorK;
            _timerK += Time.deltaTime * _fadeInConversion;
            _fadeValueK = Mathf.Lerp(_currentValueK, VolumeWhenOn, _timerK);
            mixer.SetFloat("Volume11", _fadeValueK);
        }

        if (!_mixerActivatedK && _inputCountK > 0)
        {
            _highlightColorK = keysHighlightColor;
            _highlightColorK.a = 0.25f;
            imageMixerK.color = _highlightColorK;
            _timerK += Time.deltaTime * _fadeOutConversion;
            _fadeValueK = Mathf.Lerp(_currentValueK, VolumeStandard, _timerK);
            mixer.SetFloat("Volume11", _fadeValueK);
        }


        // Mixer L
        if (_mixerActivatedL)
        {
            _highlightColorL = keysHighlightColor;
            _highlightColorL.a = 1f;
            imageMixerL.color = _highlightColorL;

            if (_waitTimerMixerL < timeToWaitMixerL)
            {
                _waitTimerMixerL += Time.deltaTime;
            }

            if (_waitTimerMixerL >= timeToWaitMixerL)
            {
                _timerL += Time.deltaTime * _fadeInConversion;
                _fadeValueL = Mathf.Lerp(_currentValueL, VolumeWhenOn, _timerL);
                //mixer.SetFloat("Volume12", _fadeValueL);
            }
        }

        if (!_mixerActivatedL && _inputCountL > 0)
        {
            _highlightColorL = keysHighlightColor;
            _highlightColorL.a = 0.25f;
            imageMixerL.color = _highlightColorL;
            _timerL += Time.deltaTime * _fadeOutConversion;
            _fadeValueL = Mathf.Lerp(_currentValueL, VolumeStandard, _timerL);
            //mixer.SetFloat("Volume12", _fadeValueL);
        }
    }


    
    // METHODS //////////////////////////////////////////////////////////////////////////////////////////////////////

    private void KeyPressed1()
    {
        if (_inputCount1 == 0)
        {
            _currentValue1 = VolumeStandard;
        }
        else
        {
            _currentValue1 = _fadeValue1;
        }

        _fadeValue1 = 0.0f;
        _timer1 = 0.0f;
        _inputCount1++;
    }


    private void KeyPressed2()
    {
        if (_inputCount2 == 0)
        {
            _currentValue2 = VolumeStandard;
        }
        else
        {
            _currentValue2 = _fadeValue2;
        }

        _fadeValue2 = 0.0f;
        _timer2 = 0.0f;
        _inputCount2++;
    }


    private void KeyPressed3()
    {
        if (_inputCount3 == 0)
        {
            _currentValue3 = VolumeStandard;
        }
        else
        {
            _currentValue3 = _fadeValue3;
        }

        _fadeValue3 = 0.0f;
        _timer3 = 0.0f;
        _inputCount3++;
    }


    private void KeyPressed4()
    {
        if (_inputCount4 == 0)
        {
            _currentValue4 = VolumeStandard;
        }
        else
        {
            _currentValue4 = _fadeValue4;
        }

        _fadeValue4 = 0.0f;
        _timer4 = 0.0f;
        _inputCount4++;
    }


    private void KeyPressed5()
    {
        if (_inputCount5 == 0)
        {
            _currentValue5 = VolumeStandard;
        }
        else
        {
            _currentValue5 = _fadeValue5;
        }

        _fadeValue5 = 0.0f;
        _timer5 = 0.0f;
        _inputCount5++;
    }


    private void KeyPressed6()
    {
        if (_inputCount6 == 0)
        {
            _currentValue6 = VolumeStandard;
        }
        else
        {
            _currentValue6 = _fadeValue6;
        }

        _fadeValue6 = 0.0f;
        _timer6 = 0.0f;
        _inputCount6++;
    }


    private void KeyPressed7()
    {
        if (_inputCount7 == 0)
        {
            _currentValue7 = VolumeStandard;
        }
        else
        {
            _currentValue7 = _fadeValue7;
        }

        _fadeValue7 = 0.0f;
        _timer7 = 0.0f;
        _inputCount7++;
    }


    private void KeyPressed8()
    {
        if (_inputCount8 == 0)
        {
            _currentValue8 = VolumeStandard;
        }
        else
        {
            _currentValue8 = _fadeValue8;
        }

        _fadeValue8 = 0.0f;
        _timer8 = 0.0f;
        _inputCount8++;
    }


    private void KeyPressed9()
    {
        if (_inputCount9 == 0)
        {
            _currentValue9 = VolumeStandard;
        }
        else
        {
            _currentValue9 = _fadeValue9;
        }

        _fadeValue9 = 0.0f;
        _timer9 = 0.0f;
        _inputCount9++;
    }


    private void KeyPressedK()
    {
        if (_inputCountK == 0)
        {
            _currentValueK = VolumeStandard;
        }
        else
        {
            _currentValueK = _fadeValueK;
        }

        _fadeValueK = 0.0f;
        _timerK = 0.0f;
        _inputCountK++;
    }


    private void KeyPressedL()
    {
        if (_inputCountL == 0)
        {
            _currentValueL = VolumeStandard;
        }
        else
        {
            _currentValueL = _fadeValueL;
        }

        _fadeValueL = 0.0f;
        _timerL = 0.0f;
        _waitTimerMixerL = 0.0f;
        _inputCountL++;
    }


    private void KeyPressedUp()
    {
        if (lowPassFilter)
        {
            lowPassText.color = textHighlightColor;
            mixer.SetFloat("LowPassMaster", lowPassRange);
        }
        else
        {
            lowPassText.color = Color.white;
            mixer.SetFloat("LowPassMaster", LowPassStandard);
        }
    }


    private void KeyPressedLeft()
    {
        if (bpmFilter)
        {
            bpmText.color = textHighlightColor;
            mixer.SetFloat("PitchMaster", _calcBpmPitch);
            mixer.SetFloat("PitchShifterMaster", _calcBpmPitchShifter);
        }
        else
        {
            bpmText.color = Color.white;
            mixer.SetFloat("PitchMaster", PitchStandard);
            mixer.SetFloat("PitchShifterMaster", PitchshifterStandard);
        }
    }


    private void KeyPressedRight()
    {
        if (pitchFilter)
        {
            pitchText.color = textHighlightColor;
            mixer.SetFloat("PitchMaster", pitchRange);
        }
        else
        {
            pitchText.color = Color.white;
            mixer.SetFloat("PitchMaster", PitchStandard);
        }
    }
}