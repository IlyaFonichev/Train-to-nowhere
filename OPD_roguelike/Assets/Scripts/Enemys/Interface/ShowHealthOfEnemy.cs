using UnityEngine;
using UnityEngine.UI;

public class ShowHealthOfEnemy : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;
    [SerializeField] private Image _parentHealthImage;
    [SerializeField] private Image _childHealthImage;

    private void Start() 
    {
        enemy.eme.changeHealthInterface += onChangeHealthInterface;
    }

    private void OnDestroy() 
    { 
        enemy.eme.changeHealthInterface -= onChangeHealthInterface; 
    }

    private void onChangeHealthInterface(HealthOfEnemy health)
    {
        Vector3 parentPos = _parentHealthImage.transform.position;
        float _unitOfHealth = (_childHealthImage.rectTransform.rect.width / health.GetMaxHaelth()) * (health.GetMaxHaelth() - health.GetHealth());
        Vector3 shfit = new Vector3(_unitOfHealth, 0f, 0f);
        _childHealthImage.transform.position = parentPos - shfit;
    }
}
