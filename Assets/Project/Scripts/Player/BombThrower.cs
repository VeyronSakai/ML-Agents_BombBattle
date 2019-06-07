using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Manager;
using UniRx;

namespace Player
{
    public class BombThrower : MonoBehaviour
    {
        [SerializeField] private GameObject bomb;

        [SerializeField] private Vector3 direction;

        [SerializeField] private float force;

        private RobotAgent robotAgent;

        private float time;

        private void Start()
        {
            robotAgent = this.GetComponent<RobotAgent>();
        }

        private void Update()
        {
            time += Time.deltaTime;

            if (robotAgent.isThrown & time > 2.0f)
            {
                ThrowBomb(direction, force);
                time = 0.0f;
            }
        }


        public void ThrowBomb(Vector3 dir, float force)
        {
            //ボムを生成
            GameObject bombThrowed = Instantiate(bomb, this.transform.localPosition + this.transform.forward * 0.3f, this.transform.localRotation);

            //生成したボムのRigidbodyを取得
            Rigidbody bombRb = bombThrowed.GetComponent<Rigidbody>();

            //投げるボムの方向をプレイヤーの方向に合わせる
            //y軸の初期値が90なので、-90をしている
            dir = Quaternion.Euler(0, this.transform.localRotation.eulerAngles.y - 90, 0) * dir;

            //ボムに力を加える
            bombRb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}
