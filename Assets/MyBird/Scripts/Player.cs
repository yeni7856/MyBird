using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Rigidbody2D rb2D;
        //[SerializeField] private float rotationAngle = 5.0f;

        [SerializeField] private float jumpForce = 5.0f;
        private bool KeyJump = false;                           //점프 키입력 체크

        //회전
        private Vector3 birdRotation;
        [SerializeField] private float rotationSpeed= 5.0f;

        //이동
        [SerializeField] private float moveSpeed = 5.0f;

        //대기
        [SerializeField] private float readyForce = 1.0f;

        //UI
        public GameObject readyUI;
        public GameObject gameoverUI;

        //사운드
        private AudioSource audioSource;    
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            // 게임이 일시 정지된 상태라면 입력을 받지 않음
            /*if (GameManager.IsPaused)
                return;*/

            //키입력
            InputBird();
            /*if(Input.GetKeyDown(KeyCode.Space))
            {
                rb2D.velocity = new Vector3(rb2D.velocity.x, speed, rb2D.velocity.y);
            }*/

            //버드대기
            ReadyBird();

            // 버드 회전
            RotataeBird();

            // 버드 이동
            Move();
        }

        private void FixedUpdate()
        {
            
            //점프
            if (KeyJump)
            {
                //Debug.Log("점프");
                JumpBird();
                KeyJump = false;
            }
        }
        //키입력
        void InputBird()
        {
            if (GameManager.IsDeath)
                return; //죽으면 입력 그만 
#if UNITY_EDITOR
            //점프키 입력 
            //스페이스바 또는 마우스 왼클릭 //누적
            KeyJump |= Input.GetKeyDown(KeyCode.Space);
            KeyJump |= Input.GetMouseButtonDown(0);
#else
            //터치 인풋 처리
            if(Input.touchCount > 0)
            {
               Touch touch = Input.GetTouch(0); //맨처음 터치
                if(touch.phase == TouchPhase.Began)
                {
                    KeyJump |= true;
                }
            }
#endif
            if (GameManager.IsStart == false && KeyJump)
            {
                MoveStartBird();
            }

        }

        //버드 점프
        void JumpBird()
        {
            //힘을 이용해서 위로 
            //rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb2D.velocity = Vector2.up * jumpForce;
            /*Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + rotationAngle);
            transform.rotation = targetRotation;*/
        }

        //버드 회전
        void RotataeBird()
        {
            // 게임이 시작되지 않았으면 회전하지 않음
            if (!GameManager.IsStart)
                return;
            //up + 30, down -90
            float degree = 0;
            if(rb2D.velocity.y > 0)
            {
                degree = rotationSpeed;
            }
            else
            {
                degree = -rotationSpeed;
            }
            float rotZ = Mathf.Clamp(birdRotation.z + degree, -90f, 30f); //하나씩 누적
            birdRotation = new Vector3 (0f, 0f, rotZ);
            transform.eulerAngles = birdRotation;
        }

        //움직이기
        void Move()
        {
            if (GameManager.IsStart == false || GameManager.IsDeath == true)
                return;

            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }
        //레디
        void ReadyBird()
        {
            if (GameManager.IsStart) //시작하면 래디 안함.
                return;
            //위쪽으로 힘을 주어 제자리에 있기
            if(rb2D.velocity.y < 0f)
            {
                rb2D.velocity = Vector2.up * readyForce;
            }
        }

        //버드 죽기
        void DeathBird()
        {
            // 2번 죽음 방지
            if(GameManager.IsDeath)
                return;
            //Debug.Log("죽음 처리");
            GameManager.IsDeath = true;
            gameoverUI.SetActive(true);
        }

        //점수획득
        void GetPoint()
        {
            if (GameManager.IsDeath)
                return;
            //Debug.Log("점수 획득");
            GameManager.Score++;

            //포인트 획득 사운드 플레이
            audioSource.Play();

            //기둥을 10개 통과할때마다 - 10점, 20점, 30점, 난이도 증가
            if(GameManager.Score % 10 == 0)
            {
                SpwanManager.levelTime += 0.05f;
            }
        }

        void MoveStartBird()
        {
            GameManager.IsStart = true;
            readyUI.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag == "Pipe")
            {
                DeathBird();
            }
            else if(collider.tag == "Point")
            {
                GetPoint();
            }
           //collision.gameObject.SetActive(false);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                DeathBird();
            }
        }
    }
}
