using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HideAndSeek
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public Transform footPrintContainer;
        public GameObject StartPanel;
        public GameObject StartButton;
        public GameObject RestartButton;
        public GameObject Panel321;
        public Text Text321;

        public bool Skip321;
        [Header("Prefabs")]
        public GameObject footPrintBlackPrefab;
        public GameObject footPrintRedPrefab;
        public GameObject footPrintGreenPrefab;
        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            StartPanel.SetActive(true);
            if (Skip321)
            {
                GameManager.instance.OnHidingStartEvent.Invoke();
                StartPanel.SetActive(false);
                RestartButton.SetActive(true);
                Panel321.gameObject.SetActive(false);
                
                GameManager.instance.OnSeekingStartEvent.Invoke();
            }
        }

        public void TapToStart(bool isHiding)
        {
            GameManager.instance.isPlayerHiding = isHiding;
            GameManager.instance.OnHidingStartEvent.Invoke();
            StartPanel.SetActive(false);
            RestartButton.SetActive(true);
            Panel321.SetActive(true);
            if (isHiding)
            {
                Panel321.GetComponent<Image>().enabled = false;
            }

            foreach (var AI in AllAIsManager.instance.AIBehaviours)
            {
                AI.StartHiding();
            }

            StartCoroutine(StartGameNow());
        }

        public IEnumerator StartGameNow()
        {
            
            for (int i = 3; i > 0; i--)
            {
                Text321.text = i + "";
                yield return new WaitForSeconds(1);
            }
            Panel321.SetActive(false);
           
            GameManager.instance.OnSeekingStartEvent.Invoke();
        }

        public void Restart()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
