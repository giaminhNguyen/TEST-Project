using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health healthSystem;

    [SerializeField] private float reducedSpeed = 2f;
    [SerializeField] private float _target = 1f;

    private Image _healthBarSprite;
    private Camera _cam;


    private void Awake()
    {
        _healthBarSprite = transform.Find("Fill").GetComponent<Image>();
       
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        healthSystem.OnDied += HealthSystem_OnDied;

        _cam = Camera.main;
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        StartCoroutine(DelayDisable(1));

    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        _healthBarSprite.fillAmount = Mathf.MoveTowards(_healthBarSprite.fillAmount, _target, reducedSpeed * Time.deltaTime);
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar()
    {
        _target = healthSystem.GetHealthAmountNormalized();
        //barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }


    private IEnumerator DelayDisable(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
