using UnityEngine;

namespace Interactions
{
    public class Oscillator : MonoBehaviour
    {
        [SerializeField] private float _baseLine = 0;
        [SerializeField] private float _rate = 1;
        [SerializeField] private float _depth = 1;

        protected float _out;
        
        private void Update()
        { 
            Oscillate();
        }

        private void Oscillate()
        {
            _out = _baseLine + Mathf.Sin(Time.time * _rate) * _depth;
        }
    }
}