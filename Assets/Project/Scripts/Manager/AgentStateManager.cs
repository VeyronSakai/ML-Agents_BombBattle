using UniRx;
using UnityEngine;

namespace Manager
{
    public class AgentStateManager : MonoBehaviour
    {
        //PlayerState.Idleで初期化
        private ReactiveProperty<AgentState> currentPlayerState = new ReactiveProperty<AgentState>(AgentState.Idle);

        public IReadOnlyReactiveProperty<AgentState> CurrentPlayerState => currentPlayerState;

        public AgentState PlayerState => currentPlayerState.Value;

        //状態を変更
        public void SetPlayerState(AgentState state)
        {
            currentPlayerState.Value = state;
        }
    }
}
