using System.Collections;
using _scripts.AbstractPool;
using _scripts.Common.AbstractPool;
using Common.AbstractPool;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class CustomCube : MonoBehaviour, IPoolable<CustomCube>
    {
        private Rigidbody _rigidbody;

        private IPool<CustomCube> _pool;
        private Vector3 _attractorPosition;
        private float _force = 1;
        private float _lifetTime = 1;
        
        public GameObject GameObject => gameObject;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _rigidbody.velocity += _force * Time.deltaTime * (_attractorPosition - transform.position);
        }

        public void Release(IPoolReleaseData data)
        {
            _attractorPosition = transform.position;
            _rigidbody.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        public void Init(IPool<CustomCube> ownerPool, IPoolInitializationData data)
        {
            _pool = ownerPool;
            var initData = (CubePoolInitializationData)data;
            
            _attractorPosition = initData.AttractorTransform.position;
            _force = initData.Force;
            _lifetTime = initData.LifeTime;
            
            StartCoroutine(ReturnRoutine());
        }

        private IEnumerator ReturnRoutine()
        {
            yield return new WaitForSeconds(_lifetTime);
            _pool.Release(this);
        }
    }
}