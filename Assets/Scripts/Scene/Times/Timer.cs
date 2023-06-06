using Block2D.Module.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Block2D.Module.Times
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Slider m_Slider;

        [SerializeField] private float timeRemaining = 10;
        private float localTimer;

        public bool timerIsRunning = false;

        public delegate void OnTimeOut();
        public static OnTimeOut OnGameOver;

        private void OnEnable()
        {
            InputHandler.OnClick += ResetTimer;
        }

        private void OnDisable()
        {
            InputHandler.OnClick -= ResetTimer;
        }

        void Start()
        {
            // Starts the timer automatically
            timerIsRunning = true;
        }

        void Update()
        {
            RunTimer();
        }

        private void ResetTimer()
        {
            timeRemaining = 10;
            timerIsRunning = true;
        }

        private void StopTimer(int score)
        {
            timerIsRunning = false;
        }

        public void RunTimer()
        {
            localTimer = timeRemaining;

            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    m_Slider.value = timeRemaining;
                    //DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    timerIsRunning = false;
                    OnGameOver?.Invoke();
                }
            }
        }
    }
}