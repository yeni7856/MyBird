using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class PipeKill : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag == "Pipe")
            {
                Destroy(collider.gameObject);
            }
        }
    }

}
