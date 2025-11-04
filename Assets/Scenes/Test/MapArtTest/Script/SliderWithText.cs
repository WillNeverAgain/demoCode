using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace ODG.Test.ArtAssetsTest
{
    public class SliderWithText : MonoBehaviour
    {
        [SerializeField]
        public Text text;
        [SerializeField]
        public Slider slider;
    
        private Action<float> callback;
    
        public float min;
        public float max;
    
        private float defaultValue;
        public void Init(float min, float max,float defaultValue,Action<float> setCallback)
        {
            this.min = min;
            this.max = max;
            this.callback = setCallback;
            this.defaultValue = defaultValue;
            slider.onValueChanged.AddListener(ChangeValue);
            text.text = defaultValue.ToString("0.0");
            slider.SetValueWithoutNotify((defaultValue-min)/(max-min));
        }
        public void ChangeValue(float value)
        {
            var trueValue = min + (max - min) * value;
            text.text = trueValue.ToString("0.0");
            callback?.Invoke(trueValue);
        }
        public void ResetValue()
        {
            slider.SetValueWithoutNotify((defaultValue-min)/(max-min));
            ChangeValue((defaultValue-min)/(max-min));
        }
    }
}
