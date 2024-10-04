using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyBird
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static bool IsStart {  get; set; }
        public static bool IsEnd { get; set; }
        public static bool IsDeath { get; set; }

        public static int Score { get; set; }
        public static int BestScore { get; set; }  //저장데이터

        //public static bool IsPaused = false;

        //게임ui
        public TextMeshProUGUI scoreText;
        #endregion

        private void Start()
        {
            //초기화
            IsStart = false;
            IsEnd = false;
            IsDeath = false;
            Score = 0;
        }
        private void Update()
        {
            //Score UI
            scoreText.text = Score.ToString();
        }

    }
}
