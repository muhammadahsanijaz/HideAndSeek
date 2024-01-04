using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptables/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public string HideTag,SeekTag;
        [Header("Player")]
        public float PlayerMovingSpeed;
        public float PlayerRotatingSpeed;
        public float JumpForce;
        public float SeekingViewDistance;
        public float HidingViewDistance;
        public float FootPintSpawnonUIDistance;
        [Range(0, 360)]
        public float FieldOfView;
        [Space]
        [Header("Camera")]
        public float CameraFollowSpeed;
        [Range(0, 1.0f)]
        public float FogOfWarRefadeOut = 0.7f;
        [Header("AI")]
        public float DistanceToListen;
        public Material captureMaterial;

    }
}
