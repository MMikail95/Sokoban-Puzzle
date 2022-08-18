using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public class Player : MonoBehaviour

    {
        #region Instance
        public static Player instance;
        void InitSingleton()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        #endregion

        private void Awake()
        {
            InitSingleton();
        }
    }
}
