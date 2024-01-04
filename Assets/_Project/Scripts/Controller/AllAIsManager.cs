using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HideAndSeek
{

    public class AllAIsManager : MonoBehaviour
    {
        public static AllAIsManager instance;
        public List<Transform> hidePoints;
        public AIBehaviour[] AIBehaviours;
        private void Awake()
        {
            instance = this;
            AIBehaviours = FindObjectsOfType<AIBehaviour>();
        }

        internal Transform GetHidePoint()
        {
            return hidePoints[Random.Range(0, hidePoints.Count)];
        }


        void OnDrawGizmos()
        {
            hidePoints = new List<Transform>();
            foreach (Transform child in transform)
            {
                hidePoints.Add(child);
            }

            for (int i = 0; i < hidePoints.Count; i++)
            {
                Gizmos.DrawSphere(hidePoints[i].position, 0.4f);
                //Color col = Color.green;
                //if (i < hidePoints.Count - 1)
                //{
                //    Debug.DrawLine(hidePoints[i].position, hidePoints[i + 1].position, col, 0.1f, false);
                //}
                //else
                //{
                //    Debug.DrawLine(hidePoints[i].position, hidePoints[0].position, col, 0.1f, false);
                //}
            }
        }

    }
}