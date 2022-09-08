using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelEnvironment.Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _obstacles;
        [SerializeField] private float _spawnStep;
        [SerializeField] private float _spawnDistance;
        
        [SerializeField] private Vector2 _segmentWidth;

        private Transform _myTransform;
        private Vector3 _lastSpawnPos;

        private List<Transform> _spawnedObstacles = new List<Transform>();

        public List<Transform> SpawnedObstacles
        {
            get
            {
                _spawnedObstacles.RemoveAll(IsTransformNull);
                return _spawnedObstacles;
            }
        }

        private bool IsTransformNull(Transform obj)
        {
            return obj == null;
        }

        private void Start()
        {
            _myTransform = transform;
            _lastSpawnPos = _myTransform.position;
        }

        private void Update()
        {
            if (!(_myTransform.position.z > _lastSpawnPos.z + _spawnStep)) return;
            _lastSpawnPos.z += _spawnStep;

            var newObstacle = _obstacles[Random.Range(0, _obstacles.Length)];
                
            _spawnedObstacles.Add(Instantiate(newObstacle,
                new Vector3(Random.Range(_segmentWidth.x,_segmentWidth.y),
                    0, _lastSpawnPos.z+_spawnDistance),
                Quaternion.identity));
        }
    }
}