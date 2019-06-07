using UnityEngine;
using UniRx;
using System;
using Zenject;

namespace Bomb
{
    public class BombExploder : MonoBehaviour
    {
        [SerializeField] private GameObject explosionObject;

        // Start is called before the first frame update
        void Start()
        {
            Explode();

            Destroy(this.gameObject, 2.0f);
        }

        private void Explode()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ => Instantiate(explosionObject,this.transform.localPosition,Quaternion.identity));
        }
    }
}