using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public GameObject Model { get; private set; }
    [field: SerializeField] public EInteractableName Name { get; private set; }
    [field: SerializeField] public ECookingState CookingState { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
}
public enum EInteractableName
{
    None,
    AyahuascaLeaf,
}
public enum ECookingState
{
    Raw,
    Blendered,
    Beverage,
    Glass,
    Chopped,

}
