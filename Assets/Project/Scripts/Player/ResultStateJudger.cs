using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using Manager;

namespace Player
{
    public class ResultStateJudger : MonoBehaviour
    {
        private PlayerHP playerHP;

        private RobotAgent robotAgent;

        private PlayerHP enemyHP;

        private RobotAgent enemyAgent;

        // Start is called before the first frame update
        private void Start()
        {
            playerHP = this.GetComponent<PlayerHP>();

            robotAgent = this.GetComponent<RobotAgent>();

            enemyHP = robotAgent.Enemy.GetComponent<PlayerHP>();

            enemyAgent = robotAgent.Enemy.GetComponent<RobotAgent>();

            JudgeResult();
        }

        private void JudgeResult()
        {
            playerHP
                .hp
                .Where(x => x <= 0.0f)
                .Subscribe(_ =>
                {
                    robotAgent.AddReward(-1.0f);
                    robotAgent.Done();
                })
                .AddTo(this);

            enemyHP
                .hp
                .Where(x => x <= 0.0f)
                .Subscribe(_ =>
                {
                    robotAgent.AddReward(1.0f);
                    robotAgent.Done();
                })
                .AddTo(this);
        }
    }
}

