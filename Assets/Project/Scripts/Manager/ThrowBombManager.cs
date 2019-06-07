using UniRx;
using UnityEngine;

namespace Manager
{
    public class ThrowBombManager : MonoBehaviour
    {
        private ReactiveProperty<bool> isThrow = new ReactiveProperty<bool>(false);

        public IReadOnlyReactiveProperty<bool> IsThrowBomb => isThrow;

        public bool IsThrow => isThrow.Value;

        public void SetBool(bool Bool)
        {
            isThrow.Value = Bool;
        }
    }
}

