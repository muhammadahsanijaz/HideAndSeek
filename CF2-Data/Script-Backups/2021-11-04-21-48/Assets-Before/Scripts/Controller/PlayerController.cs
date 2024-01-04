using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum Action {none, jump, Slide}
    public GameObject character;
    private Action actionPerforming;
    private float jumpTime = 1f, slideTime = 1.5f;
    private Animator characterAnimator;
    private Rigidbody characterRigidbody;
    private float JumpingTime;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = character.GetComponent<Animator>();
        characterRigidbody = character.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = ControlFreak2.CF2Input.GetAxis("Horizontal");
        float vertical = ControlFreak2.CF2Input.GetAxis("Vertical");
        bool Jump = Input.GetKeyDown(KeyCode.Space);
        bool Slide = Input.GetKeyDown(KeyCode.C);
        //vertical = (vertical > 0) ? vertical : 0;
        if (Jump && actionPerforming == Action.none)
        {
            
            actionPerforming = Action.jump;
            characterAnimator.SetTrigger("jump");
            StartCoroutine(TurnOffInAction());
        }

        if(Slide && actionPerforming == Action.none)
        {
            actionPerforming = Action.Slide;
            characterAnimator.SetTrigger("slide");
            StartCoroutine(TurnOffInAction());
        }

        if (actionPerforming == Action.jump)
        {
            transform.Translate(GameManager.instance.gameSettings.PlayerMovingSpeed * Time.deltaTime * transform.forward, Space.World);
        }
        else if (actionPerforming == Action.Slide)
        {
            transform.Translate(GameManager.instance.gameSettings.PlayerMovingSpeed * Time.deltaTime * transform.forward, Space.World);
        }
        else
        {
            Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
            characterAnimator.SetFloat("move", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            //movementDirection.Normalize();
            transform.Translate(GameManager.instance.gameSettings.PlayerMovingSpeed * Time.deltaTime * movementDirection, Space.World);

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
