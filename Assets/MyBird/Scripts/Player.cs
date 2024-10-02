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
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //키입력
            InputBird();
            /*if(Input.GetKeyDown(KeyCode.Space))
            {
                rb2D.velocity = new Vector3(rb2D.velocity.x, speed, rb2D.velocity.y);
            }*/

            //버드대기
            ReadyBird();

            //버드 회전
            RotataeBird();

            //버드 이동
            move();
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
        void InputBird()
        {
            //점프키 입력 
            //스페이스바 또는 마우스 왼클릭 //누적
            KeyJump |= Input.GetKeyDown(KeyCode.Space);
            KeyJump |= Input.GetMouseButtonDown(0);
            if (GameManager.IsStart == false && KeyJump)
            {
                GameManager.IsStart = true;
            }
        }
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
        void move()
        {
            if (GameManager.IsStart == false)
                return;

            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }
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
    }
}
