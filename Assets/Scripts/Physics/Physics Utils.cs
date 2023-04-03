using System;
using UnityEngine;

namespace Physics
{
    public class PhysicsFrame
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Velocity;
        public Vector3 AngularVelocity;

        public PhysicsFrame(Rigidbody body)
        {
            Position = body.position;
            Rotation = body.rotation;
            Velocity = body.velocity;
            AngularVelocity = body.angularVelocity;
        }

        public bool Equals(PhysicsFrame other)
        {
            return (Vector3Utils.WithinTolerance(this.Position, other.Position) && 
                    Vector3Utils.WithinTolerance(this.Rotation.eulerAngles, other.Rotation.eulerAngles));
        }
    }

    public static class Vector3Utils
    {
        public static bool WithinTolerance(Vector3 a, Vector3 b, float tolerance = 0.01f)
        {
            return Math.Abs((b-a).magnitude) <= tolerance;
        }
    }
}