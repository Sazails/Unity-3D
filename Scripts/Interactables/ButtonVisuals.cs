using System;
using UnityEngine;

namespace _Project.Scripts._Interactable
{
    [Serializable]
    public class ButtonVisuals
    {
        public Material buttonScreen;
        public Color onColor;
        public Color offColor;

        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        public void Toggle(bool isOn)
        {
            buttonScreen.EnableKeyword("_EMISSION");
            buttonScreen.SetColor(EmissionColor, (isOn ? onColor : offColor) * 2);
        }
    }
}
