using UnityEngine;
using UniRx;
using Zenject;
using Manager;
using System;

namespace Player
{
    public class CollapseToIdle : MonoBehaviour
    {
        private  AgentStateManager agentStateManager;

        // Start is called before the first frame update
        void Start()
        {
            agentStateManager = this.GetComponent<AgentStateManager>();

            agentStateManager
                .CurrentPlayerState
                .Where(state => state == AgentState.Collapse)
                .Delay(TimeSpan.FromSeconds(1.0f))
                .Subscribe(_ => agentStateManager.SetPlayerState(AgentState.Idle))
                .AddTo(this);
        }
    }
}