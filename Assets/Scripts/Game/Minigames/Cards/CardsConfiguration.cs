using System.Collections.Generic;
using UnityEngine;

namespace Game.Minigames.Cards
{
    [CreateAssetMenu(menuName = "Game/Minigames/Cards/Configuration", fileName = "Cards Configuration")]
    public sealed class CardsConfiguration
        : ScriptableObject
    {
        [SerializeField] private List<Sprite> cardIcons;

        [field: SerializeField]
        public int MatchesCount
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public Vector2Int FieldSize
        {
            get;
            private set;
        }

        [field: SerializeField]
        public CardView ViewPrefab
        {
            get;
            private set;
        }

        public int CardsCount => cardIcons.Count;
        
        public Sprite GetSpriteIcon(int id)
        {
            var result = cardIcons[id % cardIcons.Count];
            return result;
        }
    }
}