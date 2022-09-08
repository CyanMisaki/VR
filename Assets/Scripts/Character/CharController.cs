using System;
using UnityEngine;

namespace Character
{
        public class CharController : MonoBehaviour
        {
                [SerializeField] private GameObject _camera;
                [SerializeField] private float _speed = 3;
                [SerializeField] private float _sideSpeed = 2;
                [SerializeField] private float _deadZoneRotation = 10;

                [SerializeField] private float _timeToWin = 60f;

                private Rigidbody _player;

                private void Start()
                {
                        _player = GetComponent<Rigidbody>();
                }

                private void Update()
                {
                        var dir = _player.velocity;

                        if (_camera.transform.rotation.eulerAngles.z > _deadZoneRotation
                            && _camera.transform.rotation.eulerAngles.z <= 180)
                        {
                                dir.x = _camera.transform.rotation.eulerAngles.z * -1 * Time.deltaTime * _sideSpeed;
                        }

                        if (_camera.transform.rotation.eulerAngles.z > 180
                            && _camera.transform.rotation.eulerAngles.z <= 360 - _deadZoneRotation)
                        {
                                dir.x = (360 - _camera.transform.rotation.eulerAngles.z) * Time.deltaTime * _sideSpeed;
                        }
#if UNITY_EDITOR
                        dir.x = Input.GetAxis("Horizontal") * _sideSpeed;
#endif
                        dir.z = _speed;
                
                        _player.velocity = dir;
                }

                private void LateUpdate()
                {
                        _timeToWin -= Time.deltaTime;
                        if (!(_timeToWin <= 0)) return;
                        
                        Time.timeScale = 0;
                        Debug.Log("You WIN!!!");
                }
        }
}