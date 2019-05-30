using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



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



public class AudioMixerController : MonoBehaviour
{

    [Header("Basic Settings:")]
    public AudioMixer MyMixer;
    
    [Space(5)]
    public bool pitchFilter;
    [Range(0.5f, 1.5f)] public float pitchRange;
    [Space(5)]
    public bool lowPassFilter;
    [Range(250f, 5000f)] public float lowPassRange;
    [Space(5)]
    public bool bpmFilter;
    [Range(-0.5f, 1.5f)] public float bpmRange;
    private float calcBpmPitch;
    private float calcBpmPitchShifter;

    
    private const float lowPassStandard = 5000f;
    private const float pitchStandard = 1f;
    private const float bpmStandard = 1f;
    private const float pitchshifterStandard = 1f;


    [Header("Active Groups:")]
    public bool volumeMixer1 = false;
    public bool volumeMixer2 = false;
    public bool volumeMixer3 = false;
    public bool volumeMixer4 = false;
    public bool volumeMixer5 = false;
    public bool volumeMixer6 = false;
    public bool volumeMixer7 = false;
    public bool volumeMixer8 = false;
    public bool volumeMixer9 = false;
    public bool volumeMixerK = false;
    public bool volumeMixerL = false;


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
        // Berechnung BPM
        calcBpmPitch = bpmStandard * bpmRange;
        calcBpmPitchShifter = bpmStandard / bpmRange;
        
        
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lowPassFilter = !lowPassFilter;
            
            if (lowPassFilter)
            {
                MyMixer.SetFloat("LowPassMaster", lowPassRange);
            }
            else
            {
                MyMixer.SetFloat("LowPassMaster", lowPassStandard);
            }
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pitchFilter = !pitchFilter;
            
            if (pitchFilter)
            {
                MyMixer.SetFloat("PitchMaster", pitchRange);
            }
            else
            {
                MyMixer.SetFloat("PitchMaster", pitchStandard);
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bpmFilter = !bpmFilter;
            
            if (bpmFilter)
            {
                MyMixer.SetFloat("PitchMaster", calcBpmPitch);
                MyMixer.SetFloat("PitchShifterMaster", calcBpmPitchShifter);
            }
            else
            {
                MyMixer.SetFloat("PitchMaster", pitchStandard);
                MyMixer.SetFloat("PitchShifterMaster", pitchshifterStandard);
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            volumeMixer1 = !volumeMixer1;
            
            if (volumeMixer1)
            {
                MyMixer.SetFloat("Volume1", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume1", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            volumeMixer2 = !volumeMixer2;
            
            if (volumeMixer2)
            {
                MyMixer.SetFloat("Volume2", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume2", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            volumeMixer3 = !volumeMixer3;
            
            if (volumeMixer3)
            {
                MyMixer.SetFloat("Volume3", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume3", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            volumeMixer4 = !volumeMixer4;
            
            if (volumeMixer4)
            {
                MyMixer.SetFloat("Volume4", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume4", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            volumeMixer5 = !volumeMixer5;
            
            if (volumeMixer5)
            {
                MyMixer.SetFloat("Volume5", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume5", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            volumeMixer6 = !volumeMixer6;
            
            if (volumeMixer6)
            {
                MyMixer.SetFloat("Volume6", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume6", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            volumeMixer7 = !volumeMixer7;
            
            if (volumeMixer7)
            {
                MyMixer.SetFloat("Volume7", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume7", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            volumeMixer8 = !volumeMixer8;
            
            if (volumeMixer8)
            {
                MyMixer.SetFloat("Volume8", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume8", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            volumeMixer9 = !volumeMixer9;
            
            if (volumeMixer9)
            {
                MyMixer.SetFloat("Volume9", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume9", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            volumeMixerK = !volumeMixerK;
            
            if (volumeMixerK)
            {
                MyMixer.SetFloat("Volume11", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume11", -80f);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            volumeMixerL = !volumeMixerL;
            
            if (volumeMixerL)
            {
                MyMixer.SetFloat("Volume12", 0f);
            }
            else
            {
                MyMixer.SetFloat("Volume12", -80f);
            }
            
        }


        
    }
    
}
