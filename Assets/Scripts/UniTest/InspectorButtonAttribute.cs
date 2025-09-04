namespace UniTest
{
    using UnityEngine;

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class InspectorButtonAttribute : PropertyAttribute
    {
        public string ButtonText { get; private set; }

        public InspectorButtonAttribute() 
        {
            ButtonText = null; // 使用默认方法名
        }

        public InspectorButtonAttribute(string customName)
        {
            ButtonText = customName;
        }
    }
}