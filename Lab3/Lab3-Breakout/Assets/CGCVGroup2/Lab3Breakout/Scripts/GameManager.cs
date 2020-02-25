using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CGCVGroup2.Lab3Breakout.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        private int _BrickCount, _LifeCount, _Score;
        private bool _GameIsOver;
        [SerializeField] private Transform bricks;
        [SerializeField] private Toggle[] lives;
        [SerializeField] private RectTransform loseScreen, winScreen;
        [SerializeField] private TextMeshPro score;
        [SerializeField] private float time;

        public GameObject Ball_Ob;
        public GameObject Paddle_Ob;

        public Rigidbody Ball_RB;
        public Rigidbody Paddle_RB;

        public void CollideBrick(Collision other)
        {
            if (_GameIsOver) return;
            _BrickCount--;
            other.gameObject.SetActive(false);
            score.text = $"Score: {++_Score}";
            if (_BrickCount > 0) return;

            _GameIsOver = true;
            winScreen.gameObject.SetActive(true);
            Invoke(nameof(Restart), time);
        }
        // Start is called before the first frame update
        void Start()
        {

            _BrickCount = 32;
            _LifeCount = 5;
            _Score = 0;
            time = 5;

            loseScreen.gameObject.SetActive(false);
            winScreen.gameObject.SetActive(false);
            Ball_Ob = GameObject.Find("Ball");
            Paddle_Ob = GameObject.Find("Paddle");
            Ball_RB = Ball_Ob.GetComponent<Rigidbody>();
            Paddle_RB = Paddle_Ob.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update()
        {

            if (_GameIsOver == true)
            {

            }
            else
            {
                if (Input.GetKey("r"))
                {
                    print("restarting mr.game");
                    Restart();
                }

                if (Input.GetKey(KeyCode.Escape))
                {
                    Time.timeScale = 0;
                }

                if (Input.GetKey(KeyCode.Return))
                {
                    Time.timeScale = 1;
            
                    print($"Ball {Ball_Ob}");
                    print($"RB {Ball_RB}");
   
                }
            }

        }

        public void CollideWater(Collision other)
        {
            if (_GameIsOver) return;
            _LifeCount--;
            //other.gameObject.SetActive(false);
            //score.text = $"Score: {++_Score}";
            lives[_LifeCount].gameObject.SetActive(false);
            Ball_RB.position = new Vector3(0, 2.5f, 0);
            Paddle_RB.position = new Vector3(0, 0.75f, 0);

            Ball_RB.velocity = new Vector3(UnityEngine.Random.Range(-10.0f,10.0f), 10.0f, 0);

            
            if (_LifeCount > 0) return;

            _GameIsOver = true;
            
            //print($"LOSE : {loseScreen.gameObject} poes");
            loseScreen.gameObject.SetActive(true);
            winScreen.gameObject.SetActive(false);
            Invoke(nameof(Restart), time);
        }

        void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void Reset()
        {
            bricks = null;
            lives = null;
            //loseScreen = null;
            score = null;
            time = 5;
        }
    }

}
