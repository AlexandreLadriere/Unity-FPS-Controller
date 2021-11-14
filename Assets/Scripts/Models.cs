
using System;
using UnityEngine;

public static class Models
{
    #region - Player -
    [Serializable]
    public class PlayerSettingsModel {
        [Header("ViewSettings")]
        public float viewXSensitivity;
        public float viewYSensitivity;
        public bool viewXInverted;
        public bool viewYInverted;

        [Header("Movement")]
        public float walkingForwardSpeed;
        public float walkingBackwardSpeed;
        public float walkingStrafeSpeed;

        [Header("Jumping")]
        public float jumpingHeight;
        public float jumpingFallof;
    }
    #endregion
}
