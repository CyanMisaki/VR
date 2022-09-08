using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private List<Transform> _segments;
        [SerializeField] private float _minDiatance;
        [SerializeField] private Transform _player;


        private void Update()
        {
            var lastObj = _segments[_segments.Count - 1];
            var dis = Vector3.Distance(lastObj.position, _player.position);

            if (!(dis < _minDiatance)) return;
            
            var firstObj = _segments[0];
            firstObj.position = lastObj.position;

            var offset = lastObj.GetComponent<Collider>().bounds.extents +
                         firstObj.GetComponent<Collider>().bounds.extents;
                
            firstObj.position += Vector3.forward * offset.z;

            _segments.Remove(firstObj);
            _segments.Add(firstObj);
        }
    }
}