using _scripts.AbstractPool;
using _scripts.Common.AbstractPool;
using Common.AbstractPool;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubeFactory : AbstractPool<CustomCube>
    {
        [Space]
        [Header("Cube Factory settings")]
        [Space]
        
        [SerializeField] private Transform _attractorTransform;
        [SerializeField] private float _force = 6;
        [SerializeField] private float _lifeTime = 3;
    
        protected override IPoolReleaseData GetReleaseData()
        {
            return null;
        }

        protected override IPoolInitializationData GetInitData()
        {
            return new CubePoolInitializeData(_attractorTransform, _force, _lifeTime);
        }
    }
}
