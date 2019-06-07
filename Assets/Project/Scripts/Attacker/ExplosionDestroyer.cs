using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace Bomb
{
    public class ExplosionDestroyer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SphereCollider sphereCollider = this.GetComponent<SphereCollider>();

            ParticleSystem particle = this.GetComponent<ParticleSystem>();

            //コライダーのみ消去
            Destroy(sphereCollider, particle.main.duration * 0.8f);

            //Explosionオブジェクトの消去
            Destroy(this.gameObject, particle.main.duration);
        }
    }
}