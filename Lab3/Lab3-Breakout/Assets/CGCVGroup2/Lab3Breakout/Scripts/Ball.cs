using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGCVGroup2.Lab3Breakout.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        [SerializeField] private float speed;

        // assign a reference to the Rigidbody component
        // to the _rigidbody variable
        private void Awake()
		{
            _rigidbody = GetComponent<Rigidbody>();
		}

        private void OnCollisionEnter(Collision other)
        {


            //throw new NotImplementedException();
        }

        // set default value of the speed variable
        private void Reset()
        {
            speed = 12;
        }

        // Start is called before the first frame update
        // add an initial force (speed) to the rigid body
        void Start()
        {
            Vector3 force = new Vector3(Random.Range(-10.0f, 10.0f), -speed, 0);
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
