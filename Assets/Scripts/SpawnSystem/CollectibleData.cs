using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "CollectibleData", menuName = "Platformer/CollectibleData")]
    public class CollectibleData : EntityData
    {
        public int score;
        //additional properties specific to collectibles
    }
}