using UnityEngine;
using Manager;
using Zenject;
using UniRx;
using UniRx.Triggers;

namespace Player
{
    public class BlastHitter : MonoBehaviour
    {
        private AgentStateManager agentStateManager;

        private void Start()
        {
            agentStateManager = this.GetComponent<AgentStateManager>();

            this.OnTriggerEnterAsObservable()
                .Where(collision => collision.tag == "Explosion")
                .Subscribe(_ => agentStateManager.SetPlayerState(AgentState.Collapse));
        }
    }
}