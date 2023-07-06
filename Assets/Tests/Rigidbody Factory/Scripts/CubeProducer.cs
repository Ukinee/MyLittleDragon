using _scripts.Common.Utils;
using UnityEngine;

namespace Tests.Rigidbody_Factory.Scripts
{
    public class CubeProducer : MonoBehaviour
    {
        [SerializeField] private CubeFactory _cubeFactory;

        public void CreateCubeAtMyPosition()
        {
            if (_cubeFactory.IsDisabled)
            {
                Alert.Warning("Factory is disabled");
                return;
            }

            CustomCube cube = _cubeFactory.GetObject();
            
            cube.transform.position = transform.position;
            cube.transform.SetParent(transform);
        }

        public void DisableFactory()
        {
            _cubeFactory.Disable();
        }
    }
}
