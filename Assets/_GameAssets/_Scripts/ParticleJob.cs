using System;
using UnityEngine;

namespace _GameAssets._Scripts
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class ParticleJob : MonoBehaviour
    {
        [SerializeField]
        protected ParticleSystem _particleSystem;

        private void OnValidate()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
    }
}