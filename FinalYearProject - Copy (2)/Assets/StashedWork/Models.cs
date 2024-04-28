using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Models
{
    #region - player

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float ViewXSensitivity;
        public float ViewYSensitivity;
        
        public bool ViewInvertedX;
        public bool ViewInvertedY;
    }

    #endregion
}
