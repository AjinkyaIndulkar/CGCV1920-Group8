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
            print("WAKENING BALL");
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            name = other.gameObject.name.Substring(0, 5);
            //print(name);
            if (name == "Brick")
            {
                GameManager.Instance.CollideBrick(other);
                //print("xed  A BRICK");
                
            }
            if (other.gameObject.name == "Water")
            {
               GameManager.Instance.CollideWater(other);
                //print("xed  A WATER");
            }

            if (other.gameObject.name == "Paddle")
            {
                Vector3 force = new Vector3(0, 0.1f, 0);
                _rigidbody.AddForce(force, ForceMode.Impulse);
              
            }
        }

        // set default value of the speed variable
        private void Reset()
        {
            speed = 12;
        }

        // Start is called before the first frame update
        // add an initial force (speed) to the rigid body
        public void Start()
        {
            print("STARTING BALL");
            Vector3 force = new Vector3(Random.Range(-10.0f, 10.0f), -speed, 0);
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}