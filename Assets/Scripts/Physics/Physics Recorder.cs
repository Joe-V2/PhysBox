using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsRecorder : MonoBehaviour
    {
        private List<PhysicsFrame> _frames = new List<PhysicsFrame>();
        private Rigidbody _body;
        private enum RecorderState
        {
            recording,
            rewinding,
            playing,
            stopped
        }

        private RecorderState _state;
        private void Start()
        {
            _body = GetComponent<Rigidbody>();
            _state = RecorderState.recording;
        }

        private void OnCollisionEnter(Collision other)
        {
            ProcessFrame();
        }

        private void OnCollisionExit(Collision other)
        {
            ProcessFrame();
        }

        private void FixedUpdate()
        {
            ProcessFrame();
        }

        private void ProcessFrame()
        {
            switch (_state)
            {
                case RecorderState.recording:
                    Record();
                    break;
                case RecorderState.rewinding:
                    StartCoroutine(Rewind());
                    break;
                case RecorderState.stopped:
                    Stop();
                    break;
                case RecorderState.playing:
                    Play();
                    break;
                default:
                    break;
            }
        }
        
        private void Record()
        {
            _frames.Add(new PhysicsFrame(_body));
        }

        private IEnumerator Rewind()
        {
            Stop();
            while (_state == RecorderState.rewinding)
            {
                PhysicsFrame thisFrame = _frames.Last();
                PhysicsFrame nextFrame = _frames[_frames.Count - 2];
                if (_body.position != nextFrame.Position)
                {
                    _body.velocity = -thisFrame.Velocity;
                    _body.angularVelocity = -thisFrame.AngularVelocity;
                }

                yield return new WaitWhile(() => !new PhysicsFrame(_body).Equals(nextFrame));
                
                _frames.RemoveAt(_frames.Count - 1);
            }
        }

        private void Stop()
        {
            _frames.Add(new PhysicsFrame(_body));
            
        }

        private void Play()
        {
        }


    }
}