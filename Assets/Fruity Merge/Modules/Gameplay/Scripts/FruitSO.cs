using UnityEngine;

[CreateAssetMenu(fileName = "FruitSO", menuName = "Scriptable Objects/FruitSO")]
public class FruitSO : ScriptableObject
{
    [SerializeField] private int fruitId;
    [SerializeField] private string fruitName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float fruitScale;
    public int FruitId => fruitId;
    public string FruitName => fruitName;
    public float FruitScale => fruitScale;
    public GameObject Prefab => prefab;
}
