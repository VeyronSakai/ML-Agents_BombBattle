using UnityEngine;
using MLAgents;
using Manager;
using Zenject;
using UniRx;
using UniRx.Triggers;
using System;

namespace Player
{
    public class RobotAgent : Agent
    {
        private Rigidbody rb;

        private Vector3 MoveDirection;

        [SerializeField] private float force;

        [Inject] private GameStateManager gameStateManager;

        public GameObject Enemy;

        private Vector3 agentPosition;

        private Quaternion agentQuaternion;

        private PlayerHP playerHP;

        private bool isRightRotate;

        private bool isLeftRotate;

        public bool isThrown;


        private float beforeDistance;
        private float currentDistance;


        private float currentInnerProduct;
        private Vector3 lookAtEnemyVector;

        private void Start()
        {
            rb = this.GetComponent<Rigidbody>();

            agentPosition = this.transform.localPosition;

            agentQuaternion = this.transform.localRotation;

            playerHP = this.GetComponent<PlayerHP>();

            beforeDistance = (this.transform.localPosition - Enemy.transform.localPosition).magnitude;
            currentDistance = (this.transform.localPosition - Enemy.transform.localPosition).magnitude;

            lookAtEnemyVector = (Enemy.transform.position - this.transform.position).normalized;
            currentInnerProduct = Vector3.Dot(lookAtEnemyVector, this.transform.forward);

            Walk();

            Rotate();
        }

        public override void CollectObservations()
        {
            AddVectorObs(this.transform.localPosition.x);
            AddVectorObs(this.transform.localPosition.z);
            AddVectorObs(this.transform.localRotation.y);
            AddVectorObs(this.rb.velocity.x);
            AddVectorObs(this.rb.velocity.z);
            AddVectorObs(Enemy.transform.localPosition.x);
            AddVectorObs(Enemy.transform.localPosition.z);
            AddVectorObs(Enemy.transform.localRotation.y);
            AddVectorObs(Enemy.GetComponent<Rigidbody>().velocity.x);
            AddVectorObs(Enemy.GetComponent<Rigidbody>().velocity.z);
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            if (gameStateManager.GameState != GameState.Play)
                return;

            //プレイヤーの移動
            MoveDirection.x = Mathf.Clamp(vectorAction[0],-1.0f,1.0f);
            MoveDirection.z = Mathf.Clamp(vectorAction[1],-1.0f,1.0f);

            //移動ベクトルの方向を調整する
            MoveDirection = Quaternion.Euler(0, this.transform.localRotation.eulerAngles.y - 90, 0) * MoveDirection;

            //ボムを投げる
            isThrown = Mathf.Clamp(vectorAction[2],-1.0f,1.0f) > 0.0f;

            //カメラの回転
            isRightRotate = (Mathf.Clamp(vectorAction[3],-1.0f,1.0f) > 0);
            isLeftRotate = (Mathf.Clamp(vectorAction[4],-1.0f,1.0f) > 0);


            currentDistance = (this.transform.localPosition - Enemy.transform.localPosition).magnitude;

            //近づいたら褒める
            if(currentDistance < beforeDistance)
            {
                AddReward(0.001f);
            }

            //近づき過ぎたら罰を与える
            if(currentDistance < 0.5f)
            {
                AddReward(-0.001f);
            }

            beforeDistance = currentDistance;

            lookAtEnemyVector = (Enemy.transform.position - this.transform.position).normalized;

            AddReward(Vector3.Dot(this.transform.forward, lookAtEnemyVector) * 0.001f);


            AddReward(-0.0001f);
        }

        public override void AgentReset()
        {
            this.transform.localPosition = agentPosition;
            this.transform.localRotation = agentQuaternion;
            playerHP.ResetHP();
        }

        private void Walk()
        {
            this.FixedUpdateAsObservable()
                .Where(_ => gameStateManager.GameState == GameState.Play)
                .Subscribe(_ => rb.AddForce(MoveDirection.normalized * force));
        }

        private void Rotate()
        {
            this.FixedUpdateAsObservable()
                .Where(_ => gameStateManager.GameState == GameState.Play)
                .Where(_ => isRightRotate)
                .Subscribe(_ => this.transform.Rotate(0, 5, 0));

            this.FixedUpdateAsObservable()
                .Where(_ => gameStateManager.GameState == GameState.Play)
                .Where(_ => isLeftRotate)
                .Subscribe(_ => this.transform.Rotate(0, -5, 0));
        }
    }
}