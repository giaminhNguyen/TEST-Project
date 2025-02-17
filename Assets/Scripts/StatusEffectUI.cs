using System.Collections.Generic;
using DefaultNamespace;
using SerializableDictionary.Scripts;
using UnityEngine;
using UnityEngine.UI;


public class StatusEffectUi : MonoBehaviour
{
    [SerializeField]
    private GameObject _statusEffectIconTemplate;
    [SerializeField]
    private SerializableDictionary<StatusEffectType, Sprite>    _statusEffectSpriteDict;
    private Dictionary<StatusEffectSO, StatusEffectIconCache> _statusEffectToIconDict = new();
    
    private Camera                                            _mainCamera;
    private StatusEffectManager                              _statusEffectManagerRef;
    private void Start()
    {
        _mainCamera = Camera.main;
        _statusEffectManagerRef = GetComponentInParent<StatusEffectManager>();
        _statusEffectManagerRef.ActivateStatus += OnActiveStatus;
        _statusEffectManagerRef.UpdateStatusEffect += OnUpdateStatusEffect;
        _statusEffectManagerRef.DeactivateStatusEffect += OnDeactiveStatusEffect;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.parent.position - _mainCamera.transform.position);
    }

    private StatusEffectIconCache CreateStatusIcon(StatusEffectSO statusEffect)
    {
        if (_statusEffectToIconDict.ContainsKey(statusEffect))
        {
            _statusEffectToIconDict[statusEffect].statusIconContainer.SetActive(true);

            return _statusEffectToIconDict[statusEffect];
        }

        GameObject createStatusIcon        = Instantiate(_statusEffectIconTemplate, transform);
        GameObject statusActiveTimer       = createStatusIcon.transform.Find("StatusActiveTimer").gameObject;
        Image      statusBuildupRadialFill = createStatusIcon.GetComponent<Image>();
        statusBuildupRadialFill.fillAmount = 0;

        Image statusActiveTimerRadialFill = statusActiveTimer.GetComponent<Image>();
        statusActiveTimerRadialFill.fillAmount = 0;

        Image statusIcon = statusActiveTimer.transform.Find("Icon").GetComponent<Image>();
        statusIcon.sprite = _statusEffectSpriteDict.Get(statusEffect.statusEffectType);
        _statusEffectSpriteDict.Set(statusEffect.statusEffectType,statusIcon.sprite);
        createStatusIcon.SetActive(true);

        return new StatusEffectIconCache(createStatusIcon, statusBuildupRadialFill, statusActiveTimerRadialFill,
                statusIcon);
    }

    private void OnActiveStatus(StatusEffectSO statusEffect,float buildAmount)
    {
        StatusEffectIconCache statusEffectIconCache = CreateStatusIcon(statusEffect);
        _statusEffectToIconDict[statusEffect] = statusEffectIconCache;
        OnUpdateStatusEffect(statusEffect,buildAmount,0);
    }

    private void OnUpdateStatusEffect(StatusEffectSO statusEffect,float buildAmount, float duration)
    {
        _statusEffectToIconDict[statusEffect].statusBuildupFill.fillAmount = buildAmount;
        _statusEffectToIconDict[statusEffect].statusActiveTimerFill.fillAmount = duration;
    }

    private void OnDeactiveStatusEffect(StatusEffectSO statusEffect)
    {
        _statusEffectToIconDict[statusEffect].statusIconContainer.SetActive(false);
    }


    // Class And Struct
    private class StatusEffectIconCache
    {
        public GameObject statusIconContainer;
        public Image      statusBuildupFill;
        public Image      statusActiveTimerFill;
        public Image      statusIcon;

        public StatusEffectIconCache(GameObject statusIconContainer, Image statusBuildupFill, Image statusActiveTimerFill,
                                     Image      statusIcon)
        {
            this.statusIconContainer   = statusIconContainer;
            this.statusBuildupFill     = statusBuildupFill;
            this.statusActiveTimerFill = statusActiveTimerFill;
            this.statusIcon            = statusIcon;
        }
    }
    
}