using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{

    public class CameraControl : MonoBehaviour
    {
        //public Camera Camera { get; private set; }
        //public GameObject bird;
        public Transform player;
        [SerializeField] private float offset = 1.5f;
        //[SerializeField] private float cameraSpeed = 5f;
        //private Vector3 target;

        private void FixedUpdate()
        {
            /*target = new Vector3(bird.transform.position +);*/
        }
        private void LateUpdate()
        {
            FollowPlayer();
        }
        void FollowPlayer()
        {
            transform.position = new Vector3(player.position.x + offset, transform.position.y, transform.position.z);
        }
    }
   

}
