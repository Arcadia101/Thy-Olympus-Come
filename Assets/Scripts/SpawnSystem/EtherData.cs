using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "EtherData", menuName = "Platformer/EtherData")]
    public class EtherData : EntityData
    {
        public int score;
        //additional properties specific to collectibles
    }
}