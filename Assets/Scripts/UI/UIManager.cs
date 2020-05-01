using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    public Slider PsySlider = null;
    public Slider MageSlider = null;

    private void Start()
    {
        
    }
    public void SetPsyHp(float _value)
    {
        if(PsySlider != null)
            PsySlider.value = _value;
    }

    public void SetMageHp(float _value)
    {
        if (MageSlider != null)
            MageSlider.value = _value;
    }
}
