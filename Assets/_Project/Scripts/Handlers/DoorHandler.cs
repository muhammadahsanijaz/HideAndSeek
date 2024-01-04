using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public enum DoorState { Closed, Inside, Outside, BothSide}

    public class DoorHandler : MonoBehaviour
    {
        public DoorState doorState = DoorState.Closed;
        public Animator doorAnimator;

        internal void ChangeDoorState(DoorSide myDoorSide,bool action)
        {
            switch (doorState)
            {
                case DoorState.Closed:
                    if (action)
                    {
                        if(myDoorSide == DoorSide.Inside)
                        {
                            doorState = DoorState.Inside;
                            doorAnimator.SetInteger("Door", 1);
                        }
                        else
                        {
                            doorState = DoorState.Outside;
                            doorAnimator.SetInteger("Door", 2);
                        }
                    }
                    else
                    {
                        doorState = DoorState.Closed;
                        doorAnimator.SetInteger("Door", 0);
                    }
                    break;
                case DoorState.Inside:
                    if (action)
                    {
                        if(myDoorSide == DoorSide.Outside)
                        {
                            doorState = DoorState.BothSide;
                        }
                        else
                        {
                            doorState = DoorState.Inside;
                            doorAnimator.SetInteger("Door", 1);
                        }
                    }
                    else
                    {
                        doorState = DoorState.Closed;
                        doorAnimator.SetInteger("Door", 0);
                    }
                    break;
                case DoorState.Outside:
                    if (action)
                    {
                        if (myDoorSide == DoorSide.Inside)
                        {
                            doorState = DoorState.BothSide;
                        }
                        else
                        {
                            doorState = DoorState.Outside;
                            doorAnimator.SetInteger("Door", 2);
                        }
                    }
                    else
                    {
                        doorState = DoorState.Closed;
                        doorAnimator.SetInteger("Door", 0);
                    }
                    break;
                case DoorState.BothSide:
                    if (!action)
                    {
                        if (myDoorSide == DoorSide.Outside)
                        {
                            doorState = DoorState.Inside;
                            doorAnimator.SetInteger("Door", 1);
                        }
                        else
                        {
                            doorState = DoorState.Outside;
                            doorAnimator.SetInteger("Door", 2);
                        }
                    }
                    break;
                default:
                    doorState = DoorState.Closed;
                    doorAnimator.SetInteger("Door", 0);
                    break;
            }
        }
    }
}
