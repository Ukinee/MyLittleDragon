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
        /// Данные только для этой жизни объекта
        /// </summary>
        /// <param name="attractorPosition"></param>
        /// <param name="force"></param>
        /// <param name="lifeTime"></param>
        public void Init(Vector3 attractorPosition, float force, float lifeTime)
        {
            _attractorPosition = attractorPosition;
            _force = force;
            _lifetTime = lifeTime;
            
            StartCoroutine(ReturnRoutine());
        }

        /// <summary>
        /// Зависимости на всю жизнь объекта
        /// </summary>
        public void Construct()
        {
            
        }

        /// <summary>
        /// Пул куда объект сам себя будет возвращать
        /// </summary>
        /// <param name="ownerPool"></param>
        public void SetPool(IPool<CustomCube> ownerPool)
        {
            _pool = ownerPool;
        }

        /// <summary>
        /// Что нажно сделать объекту, чтобы вернуться в изначальное состояние
        /// </summary>
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