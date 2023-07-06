using _scripts.Common.AbstractPool;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubePoolInitializeData : IPoolInitializationData
    {
        public CubePoolInitializeData(Transform attractorTransform, float force, float lifeTime)
        {
            AttractorTransform = attractorTransform;
            Force = force;
            LifeTime = lifeTime;
        }

        public Transform AttractorTransform { get; }
        public float Force { get; }
        public float LifeTime { get; }
    }
}