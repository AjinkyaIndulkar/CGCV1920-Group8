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

        public static GameManager instance = null;

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

        public void CollideWater()
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

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Reset()
        {
            bricks = null;
            lives = null;
            loseScreen = null;
            score = null;
            time = 5;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _BrickCount = 32;
            _LifeCount = 5;
            loseScreen = null;
            winScreen = null;
            _Score = 0;
            time = 5;
            throw new DivideByZeroException();
        }

        // Update is called once per frame
        private void Update()
        {
            throw new NotImplementedException();
        }
    }

}
