using Common.AbstractPool;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubePool : AbstractPool<CustomCube>
    {
        public CubePool(CubeFactory poolAbstractFactory, Transform parent, int initialPoolSize)
            : base(poolAbstractFactory, parent, initialPoolSize)
        {
        }

        public CustomCube GetCube(Vector3 at, Vector3 attractorPosition, float force, float lifetime)
        {
            CustomCube poolable = GetObject();
            poolable.Init(attractorPosition, force, lifetime);
            poolable.transform.position = at;

            return poolable;
        }
    }
}