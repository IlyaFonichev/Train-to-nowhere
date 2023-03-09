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

    // podpisb1vaems9 na sobb1tie izmeneni9 interfaca zdorovb9
    private void Start() { EventManager.changeHealthInterface += onChangeHealthInterface;
            EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer()); }

    // otpisb1vaems9 ot sobb1ti9 izmeneni9 interfaca zdorovb9
    private void OnDestroy() { EventManager.changeHealthInterface -= onChangeHealthInterface; }

    // function kotora9 vb1zb1vaets9 pri sobb1tii izmeneni9 interfaca zdorovb9
    private void onChangeHealthInterface(HealthOfPlayer health)
    {
        _HealthValueText.text = health.GetHealth().ToString();

        Vector3 parentPos = _healthParentImage.transform.position;
        float _unitŸf–ealth = (_healthChildImage.rectTransform.rect.width / health.GetMaxHaelth()) * (health.GetMaxHaelth() - health.GetHealth());
        Vector3 shfit = new Vector3(0.0185f * (100-health.GetHealth()), 0f, 0f);
        _healthChildImage.transform.position = parentPos - shfit;

    }
}
