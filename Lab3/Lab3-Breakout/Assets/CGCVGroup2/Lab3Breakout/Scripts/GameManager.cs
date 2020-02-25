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
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private float time;

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
            loseScreen = null;
            winScreen = null;
            _Score = 0;
            time = 5;
            Time.timeScale = 0; 
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
                    print("ESCAPE");
                    while ((Input.GetKey(KeyCode.Return)) == false)
                    {
                        print("x");
                    }
                    print("DE_ESCAPE");
                }
            }

        }

        void CollideWater()
        {
            if (_GameIsOver) return;
            _LifeCount--;
            //other.gameObject.SetActive(false);
            //score.text = $"Score: {++_Score}";
            if (_LifeCount > 0) return;

            _GameIsOver = true;
            loseScreen.gameObject.SetActive(true);
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
            loseScreen = null;
            score = null;
            time = 5;
        }
    }

}
