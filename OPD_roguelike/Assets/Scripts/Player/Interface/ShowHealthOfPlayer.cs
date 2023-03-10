using UnityEngine;
using UnityEngine.UI;

public class ShowHealthOfPlayer : MonoBehaviour
{
    [SerializeField] private Text _HealthValueText;
    [SerializeField] private Image _healthParentImage;
    [SerializeField] private Image _healthChildImage;


    private void Start() 
    {
        StaticEventsOfPlayer.changeHealthInterface += onChangeHealthInterface;
        StaticEventsOfPlayer.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer()); 
    }

    private void OnDestroy() 
    { 
        StaticEventsOfPlayer.changeHealthInterface -= onChangeHealthInterface; 
    }


    private void onChangeHealthInterface(HealthOfPlayer health)
    {
        _HealthValueText.text = health.GetHealth().ToString();

        Vector3 parentPos = _healthParentImage.transform.position;
        float _unitŸf–ealth = (_healthChildImage.rectTransform.rect.width / health.GetMaxHaelth()) * (health.GetMaxHaelth() - health.GetHealth());
        Vector3 shfit = new Vector3(0.0185f * (100-health.GetHealth()), 0f, 0f);
        _healthChildImage.transform.position = parentPos - shfit;

    }
}
