using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability")]
public class Ability : ScriptableObject
{
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
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public AbilityType Type
    {
        get { return abilityType; }
        set { abilityType = value; }
    }
    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }
}
