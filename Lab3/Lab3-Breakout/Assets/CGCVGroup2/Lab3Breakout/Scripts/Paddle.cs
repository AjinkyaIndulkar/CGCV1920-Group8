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
            Vector3 moveDir = Vector3.zero;
            moveDir.x = Input.GetAxis("Horizontal");
            transform.position += moveDir * speed * Time.deltaTime;   
        }
    }
}
