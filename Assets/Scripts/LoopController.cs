using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
#pragma warning disable


public class LoopController : MonoBehaviour
{
    private float _calcBpmPitch;
    private float _calcBpmPitchShifter;
    private float _fadeInConversion;
    private float _fadeOutConversion;
    
    private float _timer = 0.0f;
    private float _fadeValue = 0.0f;
    private float _currentValue = -80f;
    private int _inputCount = 0;

    private const float LowPassStandard = 5000f;
    private const float PitchStandard = 1f;
    private const float PitchshifterStandard = 1f;
    private const float BpmStandard = 1f;
    private const float VolumeStandard = -80f;
    private const float VolumeWhenOn = 0.0f;
    
    [Space(5)]
    [Header("Inherit Settings:")]
    public GameObject inputs;
    private InputController _inputController;
    
    [Space(5)]
    [Header("Loop Settings:")] 
    public Animator animator;
    public int animationFps = 30;
    public int lengthPerLoop = 641;
    public int offsetAnimationStart = 20;
    [Space(10)]
    public double currentDspTime;
    public double currentRealTime;
    private double _dspStartTime;
    private bool _soundActivated;
    
    [Space(5)]
    [Header("FF Animator Settings (je Loop):")]
    [Space(20)]
    [Tooltip("Dieser Wert wird pro Loop-Cycle abgezogen")]
    public int frameFixAnimParameter = -1;
    public int _frameFixAnimPara = 0;
    public int currentLoopCycle = 0;
    public float currentLoopFrame;
    [Space(10)]
    [Tooltip("Je höher der Wert, desto langsamer wirkt sich der Frame Fix Animator Parameter über die Zeit aus. Standard 4 => frameFixAnimParameter / 4")]
    public int frameFixAnimSpeedDivider = 4;
    public float frameFixAnimMultiplier;
    public float currentAnimatorSpeed;
    
    [Space(5)]
    [Header("Audio Settings:")]
    [Space(20)]
    //public AudioMixer mixer;
    public AudioSource sound;
    public float fadeInTime = 0.15f;
    public float fadeOutTime = 0.75f;
    public float timeBeforePlay = 0.5f;
    private float _timeToWaitTimer = 0.0f;

    [Space(10)] 
    public bool pitchFilterActive = false;
    [Range(0.5f, 1.5f)] public float pitchRange = 1.2f;
    [Space(10)] 
    public bool lowPassFilterActive = false;
    [Range(250f, 5000f)] public float lowPassRange = 1500f;
    [Space(10)] 
    public bool bpmFilterActive = false;
    [Range(-0.5f, 1.5f)] public float bpmRange = 1.2f;
    


    private void Awake()
    {
        _inputController = inputs.GetComponent<InputController>();
    }

    

    private void Start()
    {
        // dspTime für AudioSource
        _dspStartTime = AudioSettings.dspTime;
        sound.PlayScheduled(_dspStartTime);
        

        // Berechnung BPM
        _calcBpmPitch = BpmStandard * bpmRange;
        _calcBpmPitchShifter = BpmStandard / bpmRange;


        // Berechnung Fade-In/Out
        _fadeInConversion = 1f / fadeInTime;
        _fadeOutConversion = 1f / fadeOutTime;
    }

    
    
    public void Update()
    {
        _soundActivated = _inputController.Activated1;

        // Animator Parameters
        animator.SetBool("Activated", _soundActivated);
        animator.SetInteger("FrameHit", Mathf.FloorToInt(currentLoopFrame) + offsetAnimationStart);
        
        
        // dspTime Berechnung + FrameFix für Animator Parameters
        currentDspTime = AudioSettings.dspTime - _dspStartTime;
        currentLoopFrame = ((float)currentDspTime * (float)animationFps) - ((float)currentLoopCycle * (float)lengthPerLoop) + ((float)_frameFixAnimPara * (float)frameFixAnimParameter);

        if (lengthPerLoop <= currentLoopFrame)
        {
            currentLoopCycle++;
            _frameFixAnimPara++;
        }
        

        // Animator Video Speed Fix
        frameFixAnimMultiplier = ((((float)_frameFixAnimPara * (float)frameFixAnimParameter) / (float)frameFixAnimSpeedDivider) / (float)animationFps) / 100f;
        animator.speed = 1f + frameFixAnimMultiplier;
        
        currentAnimatorSpeed = animator.speed;

        
        // realTime basierend auf Time.deltaTime
        currentRealTime += Time.deltaTime;
        
        
        
        if (_soundActivated)
        {
            FadeInOut();
            
            if (_timeToWaitTimer < timeBeforePlay)
            {
                _timeToWaitTimer += Time.deltaTime;
            }

            if (_timeToWaitTimer >= timeBeforePlay)
            {
                _timer += Time.deltaTime * _fadeInConversion;
                _fadeValue = Mathf.Lerp(_currentValue, VolumeWhenOn, _timer);
                //mixer.SetFloat("Volume12", _fadeValue);
            }
        }

        if (!_soundActivated && _inputCount > 0)
        {
            _timer += Time.deltaTime * _fadeOutConversion;
            _fadeValue = Mathf.Lerp(_currentValue, VolumeStandard, _timer);
            //mixer.SetFloat("Volume12", _fadeValue);
        }
    }
    

    
    private void FadeInOut()
    {
        if (_inputCount == 0)
        {
            _currentValue = VolumeStandard;
        }
        else
        {
            _currentValue = _fadeValue;
        }

        _fadeValue = 0.0f;
        _timer = 0.0f;
        _timeToWaitTimer = 0.0f;
        _inputCount++;
    }
    
    
}