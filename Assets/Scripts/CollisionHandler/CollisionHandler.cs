
using System;
using GameControllers;
using UnityEngine;

namespace CollisionHandler
{
    public class CollisionHandler : MonoBehaviour
    {
        private TimeScaleController _tsc;

        private void Start()
        {
            _tsc = new TimeScaleController();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<EnemyMarker>(out var marker)) return;
            
            _tsc.StopGame();
            Debug.Log("You Looooooose");
        }
    }
}