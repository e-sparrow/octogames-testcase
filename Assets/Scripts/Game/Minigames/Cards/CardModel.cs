namespace Game.Minigames.Cards
{
    public sealed class CardModel
    {
        public CardModel(int id)
        {
            Id = id;
        }

        public int Id
        {
            get;
        }

        public bool IsMatched
        {
            get;
            set;
        } = false;
    }
}