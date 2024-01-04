using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HideAndSeek
{
    public enum CharacterState { Waiting, Hiding, Captured, Seeking}
    public class CharacterBehaviour : MonoBehaviour
    {
        public CharacterState characterState;
        internal ThirdPersonCharacter character;
        public SkinnedMeshRenderer[] AllBodyPart;

        public FieldOfView3D[] FieldOfView;
        internal virtual void StartHiding()
        {

        }

        internal virtual void StartSeeking()
        {

        }

        internal virtual void OnViewEvent()
        {

        }
    }
}