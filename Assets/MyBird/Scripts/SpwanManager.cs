using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class SpwanManager : MonoBehaviour
    {
        #region Variables
        public GameObject spwanPrefab;
        //스폰타이머
        [SerializeField] private float spwanTimer = 1.0f;
        private float count = 0f;

        [SerializeField] private float spwanTimerMax = 1.05f;
        [SerializeField] private float spwanTimerMin = 0.8f;

        //스폰 위치
        [SerializeField] private float spwanMaxY = 3.5f;
        [SerializeField] private float spwanMinY = 1.5f;
        #endregion

        void Start ()
        {
            //초기화
            count = spwanTimer;
        }
        private void Update()
        {
            //게임 진행 체크
            if(GameManager.IsStart == false || GameManager.IsDeath == true)
            {
                return;
            }
            //스폰 타이머 
            if(count <= 0f)
            {
                //스폰
                SpwanPipe();

                //초기화
                //count = spwanTimer;
                count = Random.Range(spwanTimerMin, spwanTimerMax);
            }
            count -= Time.deltaTime;    
        }
        void SpwanPipe()
        {
            float spwanY = this.transform.position.y + Random.Range(spwanMinY, spwanMaxY);
            Vector3 spwanPosition = new Vector3(this.transform.position.x, spwanY, 0f);
            Instantiate(spwanPrefab, spwanPosition, Quaternion.identity);
        }

       /* IEnumerator Spwan()
        {

            yield return null;
        }*/
    }
    
}
