using UniRx;

namespace Manager
{
    public class GameStateManager
    {
        //GameState.Homeで初期化
        private ReactiveProperty<GameState> currentGameState = new ReactiveProperty<GameState>(GameState.Play);

        public IReadOnlyReactiveProperty<GameState> CurrentGameState => currentGameState;

        public GameState GameState => currentGameState.Value;

        //状態を変更
        public void SetGameState(GameState state)
        {
            currentGameState.Value = state;
        }
    }
}
