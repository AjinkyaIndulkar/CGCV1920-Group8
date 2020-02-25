using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace CGCVGroup2.Lab3Breakout.Scripts

{
    [RequireComponent(typeof(Rigidbody))]
    public class Paddle : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        [SerializeField] private float left, right, speed;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.isKinematic = true;
            _rigidBody.useGravity = false;

        }

        private void Reset()
        {
            left = -3;
            right = 3;
            speed = 12;
        }

        private void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < 3.5)
                    transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
 
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(transform.position.x >-3.5)
                    transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

            }
        }
    }
}