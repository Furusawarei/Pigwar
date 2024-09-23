using System.Collections.Generic;
using UnityEngine;

namespace ConveyorSamples
{
    public class Conveyor : MonoBehaviour
    {
        /// <summary>
        /// コンベアが稼働中かどうか
        /// </summary>
        public bool IsOn = false;

        /// <summary>
        /// コンベアの目標駆動速度
        /// </summary>
        public float TargetDriveSpeed = 1.0f;

        /// <summary>
        /// 現在のコンベアの速度
        /// </summary>
        public float CurrentSpeed { get { return _currentSpeed; } }

        /// <summary>
        /// コンベアの駆動方向
        /// </summary>
        public Vector3 DriveDirection = Vector3.forward;

        /// <summary>
        /// 駆動にかかる力
        /// </summary>
        [SerializeField] private float _forcePower = 5f;

        private float _currentSpeed = 0;
        private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

        void Start()
        {
            // 駆動方向を正規化する
            DriveDirection = DriveDirection.normalized;
        }

        void FixedUpdate()
        {
            _currentSpeed = IsOn ? TargetDriveSpeed : 0;

            // NULLのリジッドボディを削除
            _rigidbodies.RemoveAll(r => r == null);

            foreach (var r in _rigidbodies)
            {
                // オブジェクトの速度と駆動方向のドット積を計算
                var objectSpeed = Vector3.Dot(r.velocity, DriveDirection);

                // 目標速度よりも遅い場合、力を加える
                if (objectSpeed < Mathf.Abs(TargetDriveSpeed))
                {
                    r.AddForce(DriveDirection * _forcePower, ForceMode.Acceleration);
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            var rigidBody = collision.gameObject.GetComponent<Rigidbody>();
            _rigidbodies.Add(rigidBody);
        }

        void OnCollisionExit(Collision collision)
        {
            var rigidBody = collision.gameObject.GetComponent<Rigidbody>();
            _rigidbodies.Remove(rigidBody);
        }
    }
}
