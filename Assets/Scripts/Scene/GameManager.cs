using Block2D.Module.Times;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Block2D.Module
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public bool isGameOver { get; private set; }
        public int score { get; private set; }

        private void OnEnable()
        {
            Timer.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            Timer.OnGameOver -= GameOver;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            score = 0;
            isGameOver = false;
            Time.timeScale = 1;
        }

        public void GameOver()
        {
            isGameOver = true;
            Debug.Log("GameOverr");
        }

        public void AddScore (int scr)
        {
            score += scr;
            Debug.Log("Total " + score);
        }
    }
}