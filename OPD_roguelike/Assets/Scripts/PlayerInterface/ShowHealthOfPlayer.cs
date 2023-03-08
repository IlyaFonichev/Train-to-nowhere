using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EventManager;

public class ShowHealthOfPlayer : MonoBehaviour
{
    [SerializeField] private Text _HealthValueText;
    [SerializeField] private Image _healthParentImage;
    [SerializeField] private Image _healthChildImage;

    // на старте подписываемся
    private void Start() { EventManager.changeHealthInterface += onChangeHealthInterface;
            EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer()); }

    // при уничтожении объета отписываемся
    private void OnDestroy() { EventManager.changeHealthInterface -= onChangeHealthInterface; }

    private void onChangeHealthInterface(Health health)
    {
        _HealthValueText.text = health.GetHealth().ToString();

        Vector3 parentPos = _healthParentImage.transform.position;
        Vector3 shfit = new Vector3(0.0185f * (100-health.GetHealth()), 0f, 0f);
        _healthChildImage.transform.position = parentPos - shfit;

    }
}
