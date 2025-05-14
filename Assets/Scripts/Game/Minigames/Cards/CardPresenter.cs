using System;
using Zenject;

namespace Game.Minigames.Cards
{
    public sealed class CardPresenter
        : IInitializable, IDisposable
    {
        public CardPresenter(CardModel model, CardView view)
        {
            _model = model;
            _view = view;
        }

        private readonly CardModel _model;
        private readonly CardView _view;

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}