using System;
using System.Linq;
using UnityEngine;

namespace LevelEnvironment.Obstacles
{
    public class ObstacleKiller : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private ObstacleSpawner _spawner;
        [SerializeField] private float _killDistanceZ;


        private void Update()
        {
            var obstacles = _spawner.SpawnedObstacles;

            foreach (var t in obstacles.Where(t => _player.position.z > t.position.z+_killDistanceZ))
            {
                Destroy(t.gameObject);
            }
        }
    }
}