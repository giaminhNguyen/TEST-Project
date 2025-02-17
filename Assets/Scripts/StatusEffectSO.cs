using UnityEngine;

namespace DefaultNamespace
{
    public enum StatusEffectType
    {
        Fire,
        Ice
    }

    [CreateAssetMenu(fileName = "StatusEffectSO", menuName = "DataSO/StatusEffectSO")]
    public class StatusEffectSO : ScriptableObject
    {
        public StatusEffectType statusEffectType;

        public float activationThreshold;
        public float thresholdReductionMultiplier  = 1f;
        public float thresholdReductionEverySecond = 1f;

        public float activeDuration;

        public GameObject visualEffectPrefab;

        private float      currentThreshold;
        private float      remainingDuration;
        private GameObject vfxPlaying;

        [HideInInspector]
        public bool isBuildUpOnlyShow;

        [HideInInspector]
        public bool isEffectActive;

        public    float  tickInterval = 0.5f;
        private   float  tickIntervalCD;
        protected Health health;
        protected GameObject target;
        public virtual void AddBuildup(float buildUpAmount, GameObject target)
        {
            isBuildUpOnlyShow =  true;
            currentThreshold  += buildUpAmount;

            if (currentThreshold >= activationThreshold)
            {
                ApplyEffect(target);
            }
            this.target = target;
            StatusEffectManager.EventManager.onUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            UpdateCall(target, Time.deltaTime);

            if (CanStatusVisualBeRemoved())
            {
                RemoveEffect(target);
                StatusEffectManager.EventManager.onUpdate -= OnUpdate;
            }
        }

        public virtual void ApplyEffect(GameObject target)
        {
            isEffectActive    = true;
            remainingDuration = activeDuration;
            SetTargetData(target);

            if (visualEffectPrefab != null)
            {
                vfxPlaying = Instantiate(visualEffectPrefab, target.transform.position, Quaternion.identity,
                        target.transform);
            }
        }
        
        private void SetTargetData(GameObject target)
        {
            health = target.GetComponent<Health>();
        }

        public void UpdateCall(GameObject target, float tickAmount)
        {
            if (isEffectActive)
            {
                isBuildUpOnlyShow =  false;
                remainingDuration -= tickAmount;

                if (remainingDuration <= 0)
                {
                    isEffectActive = false;
                }
            }
            else
            {
                currentThreshold -= (tickAmount * thresholdReductionEverySecond) * thresholdReductionMultiplier;

                if (currentThreshold <= 0)
                {
                    isBuildUpOnlyShow = false;
                }
            }

            tickIntervalCD += tickAmount;

            if (tickIntervalCD >= tickInterval)
            {
                UpdateEffect(target);
                tickIntervalCD = 0;
            }
        }

        public virtual void UpdateEffect(GameObject target)
        {
        }

        public virtual void RemoveEffect(GameObject target)
        {
            isEffectActive    = false;
            currentThreshold  = 0;
            remainingDuration = 0;

            if (vfxPlaying != null)
            {
                Destroy(vfxPlaying);
            }
        }

        public virtual bool CanStatusVisualBeRemoved()
        {
            return !(isEffectActive || isBuildUpOnlyShow);
        }

        public float GetCurrentThresholdNormalized()
        {
            return currentThreshold / activationThreshold;
        }

        public float GetCurrentDurationNormalized()
        {
            return remainingDuration / activeDuration;
        }
    }
}