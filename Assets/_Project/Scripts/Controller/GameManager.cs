using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HideAndSeek
{
    public enum GameState { Waiting, Hiding, Seeking}
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameSettings gameSettings;
        public PlayerBehaviour player;
        public GameObject FogOfWarPlanes;
        public bool isPlayerHiding;

        public GameState gameState;

        public UnityEvent OnSeekingStartEvent;
        public UnityEvent OnHidingStartEvent;

        internal CharacterBehaviour Seeker;
        int capturedHider;
        void Awake()
        {
            gameState = GameState.Waiting;
            instance = this;
            OnSeekingStartEvent.AddListener(OnSeekingStart);
            OnHidingStartEvent.AddListener(OnHidingStart);
        }

        void Update()
        {
            if(gameState == GameState.Seeking)
            {
                if(capturedHider == AllAIsManager.instance.AIBehaviours.Length)
                {
                    //win
                }
            }
        }

        public void HiderCapture()
        {
            capturedHider++;
        }

        void OnSeekingStart()
        {
            gameState = GameState.Seeking;
            foreach (var view in Seeker.FieldOfView)
            {
                view.eventos = true;
            }
        }

        void OnHidingStart()
        {
            gameState = GameState.Hiding;
            FogOfWarPlanes.SetActive(true);

            if (isPlayerHiding)
            {

                ChangeTagsRecursively(player.transform, gameSettings.HideTag);
                int SeekerIndex =  Random.Range(0, AllAIsManager.instance.AIBehaviours.Length);
                Seeker = AllAIsManager.instance.AIBehaviours[SeekerIndex];
                for (int i = 0; i < AllAIsManager.instance.AIBehaviours.Length; i++)
                {
                    if(i == SeekerIndex)
                    {
                        AllAIsManager.instance.AIBehaviours[i].characterState = CharacterState.Seeking;
                        ChangeTagsRecursively(AllAIsManager.instance.AIBehaviours[i].transform, gameSettings.SeekTag);
                    }
                    else
                    {
                        AllAIsManager.instance.AIBehaviours[i].characterState = CharacterState.Hiding;
                        ChangeTagsRecursively(AllAIsManager.instance.AIBehaviours[i].transform, gameSettings.HideTag);
                        AllAIsManager.instance.AIBehaviours[i].StartHiding();
                    }
                }
                player.StartHiding();
            }
            else
            {
                Seeker = player;
                ChangeTagsRecursively(player.transform,gameSettings.SeekTag);
                foreach (var AI in AllAIsManager.instance.AIBehaviours)
                {
                    AI.characterState = CharacterState.Hiding;
                    ChangeTagsRecursively(AI.transform,gameSettings.HideTag);
                    AI.StartHiding();
                }
            }
        }

        public void ChangeTagsRecursively(Transform trans, string tag)
        {
            trans.gameObject.tag = tag;
            foreach (Transform child in trans)
            {
                ChangeTagsRecursively(child, tag);
            }
        }
    }
}