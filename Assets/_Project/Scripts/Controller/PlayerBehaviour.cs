using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerBehaviour : CharacterBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameManager.instance.OnSeekingStartEvent.AddListener(StartSeeking);
            character = GetComponent<ThirdPersonCharacter>();
        }

        internal override void StartHiding()
        {
            foreach (var view in GameManager.instance.Seeker.FieldOfView)
            {
                view.OnView.AddListener(OnViewEvent);
            }
        }

        internal override void StartSeeking()
        {
            if (GameManager.instance.isPlayerHiding)
            {
                FieldOfView[1].gameObject.SetActive(false);
                FieldOfView[0].gameObject.SetActive(true);
                FieldOfView[0].viewAngle = 360;
                FieldOfView[0].viewRadius = GameManager.instance.gameSettings.HidingViewDistance;
            }
            else
            {
                FieldOfView[1].gameObject.SetActive(true);
                FieldOfView[0].gameObject.SetActive(true);
                FieldOfView[0].viewRadius = GameManager.instance.gameSettings.SeekingViewDistance;
                FieldOfView[1].viewRadius = GameManager.instance.gameSettings.HidingViewDistance;
                FieldOfView[1].viewAngle = GameManager.instance.gameSettings.FieldOfView;
            }
            
            InvokeRepeating(nameof(AIFootPrint), 1, 1);
        }

        void AIFootPrint()
        {
            if (GameManager.instance.gameState != GameState.Seeking)
                return;

            foreach (var AI in AllAIsManager.instance.AIBehaviours)
            {
                if (AI.characterState != CharacterState.Captured && AI.movingStates == AIStates.Move)
                {
                    float distanceBtwAI = Vector3.Distance(transform.position, AI.transform.position);
                    if (distanceBtwAI > GameManager.instance.gameSettings.SeekingViewDistance && distanceBtwAI < GameManager.instance.gameSettings.DistanceToListen)
                    {
                        GameObject prefab = UIManager.instance.footPrintBlackPrefab;
                        if (GameManager.instance.isPlayerHiding)
                        {
                            if (AI.characterState == CharacterState.Hiding)
                            {
                                prefab = UIManager.instance.footPrintGreenPrefab;
                            }
                            else
                            {
                                prefab = UIManager.instance.footPrintRedPrefab;
                            }
                        }

                        Vector3 targetDir = AI.transform.position - transform.position;
                        float angle = Vector3.SignedAngle(targetDir, Vector3.forward, Vector3.up);
                        GameObject footInstance = Instantiate(prefab, UIManager.instance.footPrintContainer);
                        footInstance.transform.localPosition = Quaternion.Euler(0, 0, angle) * Vector3.up * GameManager.instance.gameSettings.FootPintSpawnonUIDistance;
                        footInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
                    }
                }
            }
        }

        internal override void OnViewEvent()
        {
            if (GameManager.instance.gameState != GameState.Seeking || characterState != CharacterState.Captured)
                return;
            if (Physics.Raycast(transform.position, GameManager.instance.Seeker.transform.position - transform.position, out RaycastHit hit, 1000))
            {
                Debug.DrawRay(transform.position, GameManager.instance.Seeker.transform.position - transform.position, Color.green);
                // Debug.LogError("Tag " + hit.collider.tag);
                if (hit.collider.CompareTag(GameManager.instance.gameSettings.SeekTag))
                {
                    character.m_Animator.enabled = false;
                    character.m_Rigidbody.isKinematic = true;
                    enabled = false;
                    characterState = CharacterState.Captured;
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
