using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerController : MonoBehaviour
    {
        enum Action { none, jump, Slide }
        private Action actionPerforming;
        private float jumpTime = 1f, slideTime = 1.5f;
        private Animator characterAnimator;
        private Rigidbody characterRigidbody;
        // Start is called before the first frame update
        void Start()
        {
            characterAnimator = GetComponent<Animator>();
            characterRigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = ControlFreak2.CF2Input.GetAxis("Horizontal");
            float vertical = ControlFreak2.CF2Input.GetAxis("Vertical");
            bool Jump = ControlFreak2.CF2Input.GetKeyDown(KeyCode.Space);
            bool Slide = ControlFreak2.CF2Input.GetKeyDown(KeyCode.C);
            //vertical = (vertical > 0) ? vertical : 0;
            if (Jump && actionPerforming == Action.none)
            {

                actionPerforming = Action.jump;
                characterAnimator.SetTrigger("jump");
                StartCoroutine(TurnOffInAction());
            }

            if (Slide && actionPerforming == Action.none)
            {
                actionPerforming = Action.Slide;
                characterAnimator.SetTrigger("slide");
                StartCoroutine(TurnOffInAction());
            }

            if (actionPerforming == Action.jump)
            {
                characterRigidbody.MovePosition(transform.position + transform.forward * GameManager.instance.gameSettings.PlayerMovingSpeed);
                characterRigidbody.MovePosition(transform.position + transform.up * GameManager.instance.gameSettings.PlayerMovingSpeed);
            }
            else if (actionPerforming == Action.Slide)
            {
                characterRigidbody.MovePosition(transform.position + transform.forward * GameManager.instance.gameSettings.PlayerMovingSpeed);
            }
            else
            {
                Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
                characterAnimator.SetFloat("move", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
                //movementDirection.Normalize();
                characterRigidbody.MovePosition(transform.position + movementDirection * GameManager.instance.gameSettings.PlayerMovingSpeed);
                //transform.Translate(GameManager.instance.gameSettings.PlayerMovingSpeed * Time.deltaTime * movementDirection, Space.World);

                if (movementDirection != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, GameManager.instance.gameSettings.PlayerRotatingSpeed * Time.deltaTime);
                }
            }
        }


        public IEnumerator TurnOffInAction()
        {
            if (actionPerforming == Action.jump)
            {

                yield return new WaitForSeconds(jumpTime);
            }
            else
            {
                yield return new WaitForSeconds(slideTime);
            }
            actionPerforming = Action.none;
        }
    }
}
