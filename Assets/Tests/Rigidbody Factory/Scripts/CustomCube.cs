using System.Collections;
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

        /// <summary>
        /// Критические зависимости
        /// </summary>
        public void Construct(float lifeTime)
        {
            _lifetTime = lifeTime;
        }

        /// <summary>
        /// Инициализация объекта на одну "жизнь"
        /// </summary>
        public void Init(Vector3 attractorPosition, float force)
        {
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
            yield return new WaitForSeconds(_lifetTime);

            _pool.Release(this);
        }
    }
}