using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Manager;
using Zenject;

namespace Player
{
    public class PlayerHP : MonoBehaviour
    {
        public ReactiveProperty<float> hp = new ReactiveProperty<float>(100.0f);

        private AgentStateManager agentStateManager;

        // Start is called before the first frame update
        void Start()
        {
            agentStateManager = this.GetComponent<AgentStateManager>();

            this.agentStateManager
                .CurrentPlayerState
                .Where(state => state == AgentState.Collapse)
                .Subscribe(_ => this.hp.Value -= 100.0f)
                .AddTo(this);
        }

        public void ResetHP()
        {
            hp.Value = 100.0f;
        }
    }
}