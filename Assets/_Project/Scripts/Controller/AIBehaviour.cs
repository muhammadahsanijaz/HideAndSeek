using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public enum AIStates { Move, Hide, Stop }
    public enum StateSwitching { Switching, ReadyToSwitch, WaitingToSwitch}
    public class AIBehaviour : CharacterBehaviour
    {
        public AIStates movingStates = AIStates.Stop;

        public float minWaitToSwitchHideState = 4,maxWaitToSwitchHideState = 10;
        public float minWaitToSwitchSeekState = 4,maxWaitToSwitchSeekState = 10;


        private Transform target;
        private UnityEngine.AI.NavMeshAgent agent;
        private StateSwitching stateSwitching = StateSwitching.Switching;
        private bool WaitingToReSearch;
        void Start()
        {
            character = GetComponent<ThirdPersonCharacter>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.updateRotation = false;
            agent.updatePosition = true;
        }

        internal override void StartHiding() 
        {
            if (characterState == CharacterState.Hiding)
            {
                target = AllAIsManager.instance.GetHidePoint();
                agent.SetDestination(target.position);
                movingStates = AIStates.Move;
                foreach (var view in GameManager.instance.Seeker.FieldOfView)
                {
                    view.OnView.AddListener(OnViewEvent);
                }
            }
        }

        internal override void StartSeeking()
        {
            target = AllAIsManager.instance.GetHidePoint();
            agent.SetDestination(target.position);
            movingStates = AIStates.Move;
        }

        // Update is called once per frame
        void Update()
        {
            if (characterState == CharacterState.Hiding && GameManager.instance.gameState == GameState.Hiding)
            {
                switch (movingStates)
                {
                    case AIStates.Move:
                        character.Move(agent.desiredVelocity, false, false);
                        if (agent.remainingDistance < agent.stoppingDistance)
                            movingStates = (AIStates)Random.Range(1, 3);
                        stateSwitching = StateSwitching.Switching;
                        break;
                    case AIStates.Hide:
                        character.Move(Vector3.zero, true, false);
                        if (agent.remainingDistance > agent.stoppingDistance)
                            movingStates = AIStates.Move;
                        
                        if(stateSwitching == StateSwitching.Switching)
                        {
                            stateSwitching = StateSwitching.WaitingToSwitch;
                            StartCoroutine(WaitForSwitching());
                        }else if(stateSwitching == StateSwitching.ReadyToSwitch)
                        {
                            CheckSeekerComing();
                        }

                        break;
                    case AIStates.Stop:
                        character.Move(Vector3.zero, false, false);
                        if (agent.remainingDistance > agent.stoppingDistance)
                            movingStates = AIStates.Move;

                        if (stateSwitching == StateSwitching.Switching)
                        {
                            stateSwitching = StateSwitching.WaitingToSwitch;
                            StartCoroutine(WaitForSwitching());
                        }
                        else if (stateSwitching == StateSwitching.ReadyToSwitch)
                        {
                            CheckSeekerComing();
                        }
                        break;
                    default:
                        break;
                }

            }

            if(characterState == CharacterState.Seeking && GameManager.instance.gameState == GameState.Seeking)
            {
                
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    character.Move(Vector3.zero, false, false);
                    if (!WaitingToReSearch)
                    {
                        WaitingToReSearch = true;
                        Invoke(nameof(ChangeTargetInvoke), Random.Range(minWaitToSwitchSeekState, maxWaitToSwitchSeekState));
                    }
                }
                else
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
            }
        }

        void ChangeTargetInvoke()
        {
            WaitingToReSearch = false;
            target = AllAIsManager.instance.GetHidePoint();
            agent.SetDestination(target.position);
            movingStates = AIStates.Move;
        }

        IEnumerator WaitForSwitching()
        {
            yield return new WaitForSeconds(Random.Range(minWaitToSwitchHideState,maxWaitToSwitchHideState));
            stateSwitching = StateSwitching.ReadyToSwitch;
        }

        void CheckSeekerComing()
        {
            float distanceBtwSeeker = Vector3.Distance(transform.position, GameManager.instance.Seeker.transform.position);
            
            if (distanceBtwSeeker < GameManager.instance.gameSettings.DistanceToListen)
            {
                if (movingStates != AIStates.Move)
                {
                    movingStates = (AIStates)Random.Range(0, 3);
                    if (movingStates == AIStates.Move)
                    {
                        target = AllAIsManager.instance.GetHidePoint();
                        agent.SetDestination(target.position);
                    }
                }
            }
        }

        internal override void OnViewEvent()
        {
            if (GameManager.instance.gameState != GameState.Seeking || characterState == CharacterState.Captured)
                return;

            if (Physics.Raycast(transform.position, GameManager.instance.Seeker.transform.position - transform.position, out RaycastHit hit, 1000))
            {
                Debug.DrawRay(transform.position, GameManager.instance.Seeker.transform.position - transform.position, Color.green);
                // Debug.LogError("Tag " + hit.collider.tag);
                if (hit.collider.CompareTag(GameManager.instance.gameSettings.SeekTag))
                {
                    characterState = CharacterState.Captured;
                    character.m_Animator.enabled = false;
                    character.m_Rigidbody.isKinematic = true;
                    agent.isStopped = true;
                    enabled = false;
                    foreach (var bodypart in AllBodyPart)
                    {
                        bodypart.material = GameManager.instance.gameSettings.captureMaterial;
                    }
                }
            }
            else
            {
                Debug.DrawRay(transform.position, GameManager.instance.Seeker.transform.position - transform.position, Color.white);
            }
        }

    }
}