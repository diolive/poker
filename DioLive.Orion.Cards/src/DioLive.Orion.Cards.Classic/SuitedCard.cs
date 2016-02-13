using DioLive.Orion.Cards.Core;
using System.Collections.Generic;

namespace DioLive.Orion.Cards.Classic
{
    public class SuitedCard : Card
    {
        private static readonly Dictionary<Kind, string> _kinds;

        private static readonly Dictionary<Suit, string> _suits;

        static SuitedCard()
        {
            _kinds = new Dictionary<Kind, string>
            {
                [Kind.Two] = "2",
                [Kind.Three] = "3",
                [Kind.Four] = "4",
                [Kind.Five] = "5",
                [Kind.Six] = "6",
                [Kind.Seven] = "7",
                [Kind.Eight] = "8",
                [Kind.Nine] = "9",
                [Kind.Ten] = "10",
                [Kind.Jack] = "J",
                [Kind.Queen] = "Q",
                [Kind.King] = "K",
                [Kind.Ace] = "A",
            };

            _suits = new Dictionary<Suit, string>
            {
                [Suit.Spades] = "S",
                [Suit.Clubs] = "C",
                [Suit.Diamonds] = "D",
                [Suit.Hearts] = "H",
            };
        }

        public SuitedCard(Kind value, Suit suit)
        {
            Kind = value;
            Suit = suit;
        }

        public Kind Kind { get; }

        public Suit Suit { get; }

        public override string ToString()
        {
            return _kinds[Kind] + _suits[Suit];
        }
    }
}