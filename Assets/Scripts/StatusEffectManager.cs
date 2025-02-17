using System;
using System.Collections.Generic;
using DefaultNamespace;
using SerializableDictionary.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class StatusEffectManager : MonoBehaviour
{
    [SerializeField]
    private SerializableDictionary<StatusEffectType, StatusEffectSO> statusEffectToApplyDict = new();
    [SerializeField]
    private SerializableDictionary<StatusEffectType,StatusEffectSO> enabledEffects = new();
    private Dictionary<StatusEffectType,StatusEffectSO> statusEffectCacheDict = new();
    
    //
    [SerializeField]
    private float interval = 0.1f;

    private float currentInterval = 0f;
    private float lastInterval    = 0f;
    //
    public UnityAction<StatusEffectSO, float> ActivateStatus;
    public UnityAction<StatusEffectSO>        DeactivateStatusEffect;
    public UnityAction<StatusEffectSO,float,float> UpdateStatusEffect;
    #region Unity Func

    private void Update()
    {
        currentInterval += Time.deltaTime;
        if (currentInterval - lastInterval > interval)
        {
            
            // UpdateEffects(gameObject);
            lastInterval = currentInterval;
        }
        EventManager.onUpdate?.Invoke();
    }

    #endregion
    
    public void OnStatusTriggerBuildup(StatusEffectType effectType, float buildAmount)
    {
        if (!enabledEffects.ContainsKey(effectType))
        {
            var effectToAdd = CreateEffectObject(effectType,statusEffectToApplyDict.Get(effectType));
            enabledEffects.Add(effectType,effectToAdd);
            //
            ActivateStatus?.Invoke(effectToAdd,effectToAdd.GetCurrentDurationNormalized());
        }

        if (!enabledEffects.Get(effectType).isEffectActive)
        {
            enabledEffects.Get(effectType).AddBuildup(buildAmount,gameObject);
            UpdateStatusEffect?.Invoke(enabledEffects.Get(effectType),enabledEffects.Get(effectType).GetCurrentThresholdNormalized(),
                    enabledEffects.Get(effectType).GetCurrentDurationNormalized());
        }
        else
        {
            int tickDamageAmount = (int)Mathf.Ceil(buildAmount / 4);
        }
    }

    private StatusEffectSO CreateEffectObject(StatusEffectType statusEffectType, StatusEffectSO effectSO)
    {
        if (!statusEffectCacheDict.ContainsKey(statusEffectType))
        {
            statusEffectCacheDict[statusEffectType] = Instantiate(effectSO);
        }
        return statusEffectCacheDict[statusEffectType];
    }

    public void UpdateEffects(GameObject target)
    {
        foreach (var effect in enabledEffects.GenerateSerializableArray())
        {
            effect.Value.UpdateCall(target,interval);
            //
            UpdateStatusEffect?.Invoke(effect.Value,effect.Value.GetCurrentThresholdNormalized(),effect.Value.GetCurrentDurationNormalized());
            if (effect.Value.CanStatusVisualBeRemoved())
            {
                RemoveEffect(effect.Key);
            }
        }
    }

    public void RemoveEffect(StatusEffectType effectType)
    {
        if (enabledEffects.ContainsKey(effectType))
        {
            enabledEffects.Get(effectType).RemoveEffect(gameObject);
            //
            DeactivateStatusEffect?.Invoke(enabledEffects.Get(effectType));
            enabledEffects.Remove(effectType);
        }
    }
    [Serializable]
    public static class EventManager
    {
        public static Action onUpdate;
    }
    
}
