using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
#pragma warning disable



 /*@@@@ @@@@@@@  @@@@@@  @@@@@@@   @@@@@@ @@@@@@@@ @@@@@@@@ @@@@@@@ 
!@@       @@!   @@!  @@@ @@!  @@@ !@@     @@!      @@!      @@!  @@@
 !@@!!    @!!   @!@!@!@! @!@!!@!   !@@!!  @!!!:!   @!!!:!   @!@  !@!
    !:!   !!:   !!:  !!! !!: :!!      !:! !!:      !!:      !!:  !!!
::.: :     :     :   : :  :   : : ::.: :  : :: ::: : :: ::: :: :  : 
             _                           
            / | _  __´/_  /_ /_  _ _  _   _ _  _    ._/_
           /_.'/_// //   /_///_|/ / //_' / / //_', / /                                
            _  _/_ _  _  _   _ _      _ _  _  _ /_ ._  _ 
       |/|//_///\_\  /_// / / / //_/ / / //_|/_ / /// //_'
                                 _/                                
                                                                   
.. ..: :.: :.: :.: :.: www.alex.kielwein.com :.: :.: :.: :.: :.. */



public class AudioMixerSnapController : MonoBehaviour
{
    private float calcBpmPitch;
    private float calcBpmPitchShifter;
    private const float lowPassStandard = 5000f;
    private const float pitchStandard = 1f;
    private const float bpmStandard = 1f;
    private const float pitchshifterStandard = 1f;


    [Header("Basic Settings:")]
    [SerializeField] private float FadeInTime = 0.25f;
    [SerializeField] private float FadeOutTime = 0.25f;

    [Space(10)]
    public bool lowPassFilter;
    [Range(250f, 5000f)] public float lowPassRange;
    [Space(10)]
    public bool pitchFilter;
    [Range(0.5f, 1.5f)] public float pitchRange;
    [Space(10)]
    public bool bpmFilter;
    [Range(-0.5f, 1.5f)] public float bpmRange;



    [Header("Active Groups:")]
    [Space(10)]
    public bool Mixer1 = false;
    public bool Mixer2 = false;
    public bool Mixer3 = false;
    public bool Mixer4 = false;
    public bool Mixer5 = false;
    public bool Mixer6 = false;
    public bool Mixer7 = false;
    public bool Mixer8 = false;
    public bool Mixer9 = false;
    public bool MixerK = false;
    public bool MixerL = false;
    
    
 
    [Header("Mixer & Snapshots:")] 
    [Space(10)]
    public AudioMixer Mixer;
    public AudioMixerSnapshot SnapMaster;
    public AudioMixerSnapshot Snap1;
    public AudioMixerSnapshot Snap2;
    public AudioMixerSnapshot Snap3;
    public AudioMixerSnapshot Snap4;
    public AudioMixerSnapshot Snap5;
    public AudioMixerSnapshot Snap6;
    public AudioMixerSnapshot Snap7;
    public AudioMixerSnapshot Snap8;
    public AudioMixerSnapshot Snap9;
    public AudioMixerSnapshot Snap10;
    public AudioMixerSnapshot Snap11;
    public AudioMixerSnapshot Snap12;

    



    public void Start()
    {
        lowPassRange = 1000f;
        lowPassFilter = false;
        pitchRange = 1.2f;
        pitchFilter = false;
        bpmRange = 1.3f;
    }


    public void Update()
    {
        // BERECHNUNG BPM
        calcBpmPitch = bpmStandard * bpmRange;
        calcBpmPitchShifter = bpmStandard / bpmRange;
        
        
        
        // EFFEKTE
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lowPassFilter = !lowPassFilter;
            
            if (lowPassFilter)
            {
                Mixer.SetFloat("LowPassMaster", lowPassRange);
            }
            else
            {
                Mixer.SetFloat("LowPassMaster", lowPassStandard);
            }
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pitchFilter = !pitchFilter;
            
            if (pitchFilter)
            {
                Mixer.SetFloat("PitchMaster", pitchRange);
            }
            else
            {
                Mixer.SetFloat("PitchMaster", pitchStandard);
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bpmFilter = !bpmFilter;
            
            if (bpmFilter)
            {
                Mixer.SetFloat("PitchMaster", calcBpmPitch);
                Mixer.SetFloat("PitchShifterMaster", calcBpmPitchShifter);
            }
            else
            {
                Mixer.SetFloat("PitchMaster", pitchStandard);
                Mixer.SetFloat("PitchShifterMaster", pitchshifterStandard);
            }
        }
        
        
        
        // SNAPSHOTS
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Mixer1 = !Mixer1;
            
            if (Mixer1)
            {
                Snap1.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Mixer2 = !Mixer2;
            
            if (Mixer2)
            {
                Snap2.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Mixer3 = !Mixer3;
            
            if (Mixer3)
            {
                Snap3.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Mixer4 = !Mixer4;
            
            if (Mixer4)
            {
                Snap4.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Mixer5 = !Mixer5;
            
            if (Mixer5)
            {
                Snap5.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Mixer6 = !Mixer6;
            
            if (Mixer6)
            {
                Snap6.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Mixer7 = !Mixer7;
            
            if (Mixer7)
            {
                Snap7.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Mixer8 = !Mixer8;
            
            if (Mixer8)
            {
                Snap8.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Mixer9 = !Mixer9;
            
            if (Mixer9)
            {
                Snap9.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            MixerK = !MixerK;
            
            if (MixerK)
            {
                Snap11.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            MixerL = !MixerL;
            
            if (MixerL)
            {
                Snap12.TransitionTo(FadeInTime);
            }
            else
            {
                SnapMaster.TransitionTo(FadeOutTime);
            }
            
        }


        
    }
    
}
