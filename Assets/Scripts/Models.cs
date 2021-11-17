
using System;
using UnityEngine;

public static class Models
{
    #region - Player -

    public enum PlayerStance
    {
        Stand,
        Crouch,
        Prone
    }

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("ViewSettings")]
        public float viewXSensitivity;
        public float viewYSensitivity;
        public bool viewXInverted;
        public bool viewYInverted;

        [Header("Movement - Running")]
        public float runningForwardSpeed;
        public float runningStrafeSpeed;

        [Header("Movement - Walking")]
        public float walkingForwardSpeed;
        public float walkingBackwardSpeed;
        public float walkingStrafeSpeed;

        [Header("Jumping")]
        public float jumpingHeight;
        public float jumpingFallof;
    }

    [Serializable]
    public class CharacterStance
    {
        public float cameraHeight;
        public CapsuleCollider stanceCollider;
    }

    #endregion
}
