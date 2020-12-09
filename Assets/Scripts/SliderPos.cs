using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPos : MonoBehaviour
{
    private Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = GameObject.Find("Title Screen/Option Screen/OptionScreenCanvas/OptionWindow/Slider").GetComponent<Slider>();
        if(volumeSlider != null)
        {
            volumeSlider.normalizedValue = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
