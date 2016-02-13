using DioLive.Common.Extensions;
using DioLive.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DioLive.Orion.Cards.Core
{
    public class Deck<TCard> where TCard : Card
    {
        private Queue<TCard> _cards;

        public Deck(IEnumerable<TCard> cards)
        {
            _cards = new Queue<TCard>(cards);
        }

        public int CardsLeft
            => _cards.Count;

        public bool IsEmpty
            => _cards.Count == 0;

        public void Randomize()
        {
            Random rnd = RandomHelper.Instance;
            List<TCard> oldDeck = _cards.ToList();
            _cards = new Queue<TCard>(oldDeck.Count);
            while (oldDeck.Count > 0)
            {
                int index = rnd.Next(oldDeck.Count);
                _cards.Enqueue(oldDeck[index]);
                oldDeck.RemoveAt(index);
            }
        }

        public TCard Take()
        {
            if (_cards.Count == 0)
            {
                return null;
            }

            return _cards.Dequeue();
        }

        public TCard[] Take(int count)
        {
            TCard[] cards;
            if (_cards.Count <= count)
            {
                cards = _cards.ToArray();
                _cards.Clear();
            }
            else
            {
                cards = _cards.Dequeue(count).ToArray();
            }
            return cards;
        }
    }
}