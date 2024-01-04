using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public class CameraController : MonoBehaviour
    {
        public float rotationSpeed;
        private Vector3 offset;
        Transform player;
        // Start is called before the first frame update
        void Start()
        {
            player = GameManager.instance.player.transform;
            offset = transform.position - player.position;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 target = player.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * GameManager.instance.gameSettings.CameraFollowSpeed);
            transform.Rotate(new Vector3(0,rotationSpeed * ControlFreak2.CF2Input.GetAxis("Rotate"), 0));
        }
    }
}
