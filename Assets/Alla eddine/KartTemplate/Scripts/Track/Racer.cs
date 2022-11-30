using System.Collections.Generic;
using KartGame.KartSystems;
using UnityEngine;

namespace KartGame.Track
{
    /// <summary>
    /// The default implementation of the IRacer interface.  This is a representation of all the timing information for a particular kart as it goes through a race.
    /// </summary>
    public class Racer : MonoBehaviour, IRacer
    {
        [Tooltip("A reference to the IControllable for the kart.  Normally this is the KartMovement script.")]
        [RequireInterface(typeof(IControllable))]
        public Object kartMovement;

        private IControllable m_KartMovement;
        public IMovable movable;
        private bool m_IsTimerPaused = true;
        private float m_Timer = 0f;
        private int m_CurrentLap = 0;
        private List<float> m_LapTimes = new List<float>(9);
        public Checkpoint lastCheckPoint;

        private void Awake()
        {
            m_KartMovement = kartMovement as IControllable;
            movable = GetComponent<IMovable>();
        }

        private void Update()
        {
            if (m_CurrentLap > 0 && !m_IsTimerPaused)
                m_Timer += Time.deltaTime;
        }

        public void PauseTimer()
        {
            m_IsTimerPaused = true;
        }

        public void UnpauseTimer()
        {
            m_IsTimerPaused = false;
        }

        public void HitStartFinishLine()
        {
            if (m_CurrentLap > 0)
            {
                m_LapTimes.Add(m_Timer);
                m_Timer = 0f;
            }

            m_CurrentLap++;
        }

        public int GetCurrentLap()
        {
            return m_CurrentLap;
        }

        public List<float> GetLapTimes()
        {
            return m_LapTimes;
        }

        public float GetLapTime()
        {
            return m_Timer;
        }

        public float GetRaceTime()
        {
            float raceTime = m_Timer;
            for (int i = 0; i < m_LapTimes.Count; i++)
            {
                raceTime += m_LapTimes[i];
            }

            return raceTime;
        }

        public void EnableControl()
        {
            m_KartMovement.EnableControl();
        }

        public void DisableControl()
        {
            m_KartMovement.DisableControl();
        }

        public bool IsControlled()
        {
            return m_KartMovement.IsControlled();
        }

        public string GetName()
        {
            return name;
        }

        public void Respawn()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            movable.DisableControl();
            if (lastCheckPoint == null) return;
            IKartInfo kartInfo = movable.GetKartInfo();

            if (kartInfo == null)
            {
                return;
            }

            Vector3 kartToResetPosition = lastCheckPoint.ResetPosition - kartInfo.Position;
            Quaternion kartToResetRotation = lastCheckPoint.ResetRotation * Quaternion.Inverse(kartInfo.Rotation);
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<KartMovement>().ClearKartModifier();
            movable.ForceMove(kartToResetPosition, kartToResetRotation);
            movable.EnableControl();
        }
    }
}