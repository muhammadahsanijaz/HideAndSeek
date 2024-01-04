using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public enum DoorSide { Inside, Outside}
    public class DoorCollider : MonoBehaviour
    {
        public DoorHandler mydoorHandler;
        public DoorSide myDoorSide;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameManager.instance.gameSettings.SeekTag) || other.CompareTag(GameManager.instance.gameSettings.HideTag))
            {
                mydoorHandler.ChangeDoorState(myDoorSide,true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameManager.instance.gameSettings.SeekTag) || other.CompareTag(GameManager.instance.gameSettings.HideTag))
            {
                mydoorHandler.ChangeDoorState(myDoorSide,false);
            }
        }


    }
}
