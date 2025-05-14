using System;
using System.Collections.Generic;
using System.Linq;
using Birdhouse.Common.Extensions;
using Birdhouse.Tools.Coroutines.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Minigames.Cards
{
    public sealed class CardsService
        : IInitializable, IDisposable
    {
        public CardsService(CardsConfiguration configuration, CardsServiceParameters parameters)
        {
            _configuration = configuration;
            _parameters = parameters;
        }

        private readonly CardsConfiguration _configuration;
        private readonly CardsServiceParameters _parameters;

        private IList<CardModel> _models = new List<CardModel>();
        private IList<CardModel> _selectedCards = new List<CardModel>();
        private bool _isLocked;

        public void Initialize()
        {
            var isColumnsConstraint = _parameters.Grid.constraint == GridLayoutGroup.Constraint.FixedColumnCount;
            _parameters.Grid.constraintCount = isColumnsConstraint ? _configuration.FieldSize.x : _configuration.FieldSize.y;

            var count = _configuration.FieldSize.x * _configuration.FieldSize.y;
            var idsCount = count / _configuration.MatchesCount;

            var ids = Enumerable.Range(0, _configuration.CardsCount).ToList();
            ids.FisherYatesShuffle();
            ids = ids.Take(idsCount).ToList();
            
            if (count % _configuration.MatchesCount != 0)
            {
                throw new ArgumentException();
            }
            
            for (var i = 0; i < count; i++)
            {
                var model = new CardModel(i / _configuration.MatchesCount);
                _models.Add(model);
            }

            _models.FisherYatesShuffle();
            
            for (var x = 0; x < _configuration.FieldSize.x; x++)
            {
                for (var y = 0; y < _configuration.FieldSize.y; y++)
                {
                    var index = x + y * _configuration.FieldSize.x;
                    
                    var view = Object.Instantiate(_configuration.ViewPrefab, _parameters.CardsParent);
                    view.Flip(false, true);

                    var presenter = new CardPresenter(_models[y * _configuration.FieldSize.x + x], view);
                    presenter.Initialize();

                    var sprite = _configuration.GetSpriteIcon(ids[_models[index].Id]);
                    view.SetIcon(sprite);
                }
            }
        }

        public void Dispose()
        {
        }

        private IEnumerator<ICoroutineInstruction> Pipeline()
        {
            yield break;
        }
    }

    [Serializable]
    public struct CardsServiceParameters
    {
        [field: SerializeField]
        public Transform CardsParent
        {
            get;
            private set;
        }

        [field: SerializeField]
        public GridLayoutGroup Grid
        {
            get;
            private set;
        }
    }
}