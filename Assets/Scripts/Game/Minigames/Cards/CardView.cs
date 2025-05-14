using System;
using Birdhouse.Common.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Minigames.Cards
{
    public sealed class CardView 
        : MonoBehaviour
    {
        public event Action OnClick = () => { };

        [SerializeField] private Button button;
        [SerializeField] private Image icon;
        
        [SerializeField] private GameObject front;
        [SerializeField] private GameObject back;

        private IDisposable _buttonToken;

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }

        public void Flip(bool faceUp, bool immediately = false)
        {
            if (immediately)    
            {
                front.SetActive(faceUp);
                back.SetActive(!faceUp);
            }
            else
            {
                // transform.DOPunchScale(Vector3.one, 0.5f, 10, 1f);
                
                transform
                    .DORotate(new Vector3(0, 90, 0), 0.15f)
                    .OnComplete(() =>
                    {
                        front.SetActive(faceUp);
                        back.SetActive(!faceUp);
                        transform.DORotate(new Vector3(0, 0, 0), 0.15f);
                    });
            }
        }

        public void SetInteractable(bool isInteractable)
        {
            button.interactable = isInteractable;
        }

        private bool _isFlipped = false;
        private void HandleClick()
        {
            _isFlipped = !_isFlipped;
            Flip(_isFlipped);
        }

        private void OnEnable()
        {
            _buttonToken = button.AddDisposableListener(HandleClick);
        }

        private void OnDisable()
        {
            _buttonToken.Dispose();
        }
    }
}
