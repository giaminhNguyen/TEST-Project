using UnityEngine;

namespace UltimateHelper
{
    public static class ParticleExt
    {
        public static void Restart(this ParticleSystem particleSystem)
        {
            particleSystem.Stop();
            particleSystem.Clear();
            particleSystem.Play();
        }
    }
}