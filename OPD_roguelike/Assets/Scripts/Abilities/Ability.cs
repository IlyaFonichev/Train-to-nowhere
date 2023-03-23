using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "ScriptableObject/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField]
    private Color color; //��� ������ �� ���� �����, ����� ����� ��������
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private AbilityType abilityType;
    [SerializeField, TextArea]
    private string description;
    public enum AbilityType
    {
        None,
        SpeedUp,
        HigherRateOfFire,
        HealthUp,
        AbilityType5,
        AbilityType6,
        AbilityType7,
        AbilityType8
    }
    public string getDescription
    {
        get { return description; }
    }
    public AbilityType getType
    {
        get { return abilityType; }
    }
    public Sprite getSprite
    {
        get { return sprite; }
    }
    public Color getColor
    {
        get { return color; }
    }
}
