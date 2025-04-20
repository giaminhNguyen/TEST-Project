using System;
using UnityEngine;
using UnityEngine.Events;

namespace UltimateHelper
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class ParticleJobMoveBase : MonoBehaviour
    {
        public UnityEvent<int> onParticleCountFinish;
        public Action<int>     onParticleCountFinishAction;
        [Space(3)]
        [SerializeField]
        protected ParticleSystem _particleSystem;

        private void OnValidate()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
    }
}