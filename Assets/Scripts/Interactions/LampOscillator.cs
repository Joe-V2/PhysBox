using System;
using UnityEditor.Rendering.BuiltIn;
using UnityEngine;

namespace Interactions
{
    public class LampOscillator : Oscillator
    {
        private float _lampIntensity;
        private Color _propertyColour;
        private int _property;
        private Material _shader;

        private void Start()
        {
            _shader = GetComponent<Renderer>().material;
            _property = Shader.PropertyToID("_EmissionColor");
            _propertyColour = _shader.GetColor(_property);
        }

        private void LateUpdate()
        {
            _lampIntensity = _out;

            _shader.SetColor(_property, _propertyColour * _lampIntensity);
        }
    }
}