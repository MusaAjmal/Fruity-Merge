//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "FruitDatabase", menuName = "Scriptable Objects/FruitDatabase")]
//public class FruitDatabase : ScriptableObject
//{
//    [SerializeField] private List<FruitSO> fruits;
//    public List<FruitSO> Fruits => fruits;
//    /// <summary>
//    /// Returns the next tier of fruit based on the current fruit ID.
//    /// </summary>
//    public FruitSO GetNextFruit(int currentId)
//    {
//        int nextIndex = currentId + 1;
//        if (nextIndex >= 0 && nextIndex < fruits.Count)
//        {
//            return fruits[nextIndex];
//        }
//        return null; // Maximum tier reached
//    }
//}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit Database", menuName = "Fruit Merge/Fruit Database")]
public class FruitDatabase : ScriptableObject
{
    [SerializeField] private List<FruitSO> fruits;

    public List<FruitSO> Fruits => fruits;

    /// <summary>
    /// Returns the next tier of fruit based on the current fruit ID.
    /// </summary>
    public FruitSO GetNextFruit(int currentId)
    {
        // Since FruitId is 1-indexed (1, 2, 3...) and the list is 0-indexed (0, 1, 2...):
        // Current fruit (ID: currentId) is at list index: currentId - 1
        // Next fruit (ID: currentId + 1) is at list index: currentId
        int nextIndex = currentId;

        if (nextIndex >= 0 && nextIndex < fruits.Count)
        {
            return fruits[nextIndex];
        }
        return null; // Maximum tier reached (e.g. Watermelon + Watermelon)
    }
}