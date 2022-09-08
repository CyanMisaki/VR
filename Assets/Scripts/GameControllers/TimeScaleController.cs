using UnityEngine;

namespace GameControllers
{
    public class TimeScaleController
    {
        public void StopGame()
        {
            Time.timeScale = 0f;
            Debug.Log("Time.Stoped");
        }
        
        public void StartGame()
        {
            Time.timeScale = 1f;
            Debug.Log("Time.Started");
        }
    }
}