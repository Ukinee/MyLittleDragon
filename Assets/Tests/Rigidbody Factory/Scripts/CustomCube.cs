using System.Collections;
using Common.Services.Pools.Interfaces;
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
        private float _lifetime = 1;

        private float _elapsedTime;
        private float _oscillateSpeed;

        public GameObject GameObject => gameObject;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            _rigidbody.velocity = _force * Mathf.Sin(_elapsedTime * _oscillateSpeed) * (_attractorPosition - transform.position);
        }

        /// <summary>
        /// Критические зависимости
        /// </summary>
        public void Construct(float lifeTime)
        {
            _lifetime = lifeTime;
        }

        /// <summary>
        /// Инициализация объекта на одну "жизнь"
        /// </summary>
        public void Init(Vector3 attractorPosition, float force, float oscillateSpeed)
        {
            _oscillateSpeed = oscillateSpeed;
            _attractorPosition = attractorPosition;
            _force = force;

            StartCoroutine(ReturnRoutine());
        }

        public void SetPool(IPool<CustomCube> ownerPool)
        {
            _pool = ownerPool;
        }

        public void Release()
        {
            _rigidbody.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        private IEnumerator ReturnRoutine()
        {
            yield return new WaitForSeconds(_lifetime);

            _pool.Release(this);
        }
    }
}