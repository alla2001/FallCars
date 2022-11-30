using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    /// <summary>
    /// A basic keyboard implementation of the IInput interface for all the input information a kart needs.
    /// </summary>
    public class KeyboardInput : MonoBehaviour, IInput
    {
        public float Acceleration
        {
            get { return m_Acceleration; }
        }

        public float Steering
        {
            get { return m_Steering; }
        }

        public bool BoostPressed
        {
            get { return m_BoostPressed; }
        }

        public bool FirePressed
        {
            get { return m_FirePressed; }
        }

        public bool HopPressed
        {
            get { return m_HopPressed; }
        }

        public bool HopHeld
        {
            get { return m_HopHeld; }
        }

        private float m_Acceleration;
        private float m_Steering;
        private bool m_HopPressed;
        private bool m_HopHeld;
        private bool m_BoostPressed;
        private bool m_FirePressed;

        private bool m_FixedUpdateHappened;

        private void Start()
        {
            GetComponent<KartMovement>().EnableControl();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                m_Acceleration = 1f;
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                m_Acceleration = -1f;
            else
                m_Acceleration = 0f;

            if ((Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
                m_Steering = -1f;
            else if (!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) || (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
                m_Steering = 1f;
            else
                m_Steering = 0f;

            m_HopHeld = Input.GetKey(KeyCode.Space);

            if (m_FixedUpdateHappened)
            {
                m_FixedUpdateHappened = false;

                m_HopPressed = false;
                m_BoostPressed = false;
                m_FirePressed = false;
            }

            m_HopPressed |= Input.GetKeyDown(KeyCode.Space);
            m_BoostPressed |= Input.GetKeyDown(KeyCode.RightShift);
            m_FirePressed |= Input.GetKeyDown(KeyCode.RightControl);
        }

        private void FixedUpdate()
        {
            m_FixedUpdateHappened = true;
        }
    }
}