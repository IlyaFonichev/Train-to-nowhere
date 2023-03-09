using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealthOfEnemy : MonoBehaviour
{

    [SerializeField] private EnemyController enemy;
    [SerializeField] private Image _parentHealthImage;
    [SerializeField] private Image _childHealthImage;



    //подписка на событие изменения здоровья моба
    private void Start() 
    {
        enemy.eme.changeHealthInterface += onChangeHealthInterface;
    }

    //отписка от события изменения здоровья моба
    private void OnDestroy() { enemy.eme.changeHealthInterface -= onChangeHealthInterface; }

    //функция по подписке на событие изменение здоровья моба
    private void onChangeHealthInterface(HealthOfEnemy health)
    {
        Vector3 parentPos = _parentHealthImage.transform.position;
        float _unitЩfРealth = (_childHealthImage.rectTransform.rect.width / health.GetMaxHaelth()) * (health.GetMaxHaelth() - health.GetHealth());
        Vector3 shfit = new Vector3(_unitЩfРealth, 0f, 0f);
        _childHealthImage.transform.position = parentPos - shfit;
    }
}
