using DioLive.Orion.Cards.Classic;
using DioLive.Orion.Cards.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DioLive.Orion.Games.Poker
{
    public class Hand : IComparable<Hand>
    {
        public Hand(HandTypes handType, Card[] cards)
        {
            if (cards.Length > 5)
            {
                throw new ArgumentException("Cards count cannot be greater than 5", nameof(cards));
            }

            HandType = handType;
            Cards = cards;
        }

        public HandTypes HandType { get; }

        public Card[] Cards { get; }

        public static Hand FindBestHand(SuitedCard[] cards)
        {
            var orderedCards = cards
                .OrderByDescending(card => card.Kind)
                .ToArray();

            IEnumerable<SuitedCard> flushCandidate = orderedCards
                .GroupBy(card => card.Suit)
                .OrderByDescending(group => group.Count())
                .First();

            int flushCount = flushCandidate.Count();
            if (flushCount >= 5)
            {
                SuitedCard[] straightFlush = FindStraight(flushCandidate);
                if (straightFlush != null)
                {
                    return new Hand(HandTypes.StraightFlush, straightFlush);
                }
            }

            var byKind = orderedCards
                .GroupBy(card => card.Kind)
                .OrderByDescending(group => group.Count());

            var setCandidate = byKind.First().ToArray();
            var setCount = setCandidate.Length;
            if (setCount >= 4)
            {
                var four = (setCount > 4) ? setCandidate.Take(4).ToArray() : setCandidate;
                return new Hand(HandTypes.FourOfKind, four.Union(orderedCards.Except(four).Take(1)).ToArray());
            }

            var set2Candidate = byKind.ElementAt(1).ToArray();
            var set2Count = set2Candidate.Length;
            if (setCount == 3 && set2Candidate.Count() >= 2)
            {
                return new Hand(HandTypes.FullHouse, setCandidate.Union(set2Candidate.Take(2)).ToArray());
            }

            if (flushCount >= 5)
            {
                return new Hand(HandTypes.Flush, flushCandidate.Take(5).ToArray());
            }

            SuitedCard[] straight = FindStraight(orderedCards);
            if (straight != null)
            {
                return new Hand(HandTypes.Straight, straight);
            }

            if (setCount == 3)
            {
                return new Hand(HandTypes.ThreeOfKind, setCandidate.Union(orderedCards.Except(setCandidate).Take(2)).ToArray());
            }

            if (setCount == 2)
            {
                if (set2Count == 2)
                {
                    return new Hand(HandTypes.TwoPair, setCandidate.Union(set2Candidate).Union(orderedCards.Except(setCandidate.Union(set2Candidate)).Take(1)).ToArray());
                }

                return new Hand(HandTypes.OnePair, setCandidate.Union(orderedCards.Except(setCandidate).Take(3)).ToArray());
            }

            return new Hand(HandTypes.HighCard, Enumerable.Repeat(orderedCards[0], 1).Union(orderedCards.Skip(1).Take(4)).ToArray());
        }

        private static SuitedCard[] FindStraight(IEnumerable<SuitedCard> cards)
        {
            Kind[] straightSequence = new[] { Kind.Ace, Kind.King, Kind.Queen, Kind.Jack, Kind.Ten, Kind.Nine, Kind.Eight, Kind.Seven, Kind.Six, Kind.Five, Kind.Four, Kind.Three, Kind.Two, Kind.Ace };

            ILookup<Kind, SuitedCard> lookup = cards.ToLookup(card => card.Kind);
            int count = 0;
            for (int i = 0; i < straightSequence.Length; i++)
            {
                if (lookup.Contains(straightSequence[i]))
                {
                    count++;
                    if (count == 5)
                    {
                        return Enumerable.Range(-4, 5)
                            .Select(n => lookup[straightSequence[i + n]].First())
                            .ToArray();
                    }
                }
                else
                {
                    count = 0;
                }
            }

            return null;
        }

        public int CompareTo(Hand other)
        {
            if (HandType != other.HandType)
            {
                return HandType.CompareTo(other.HandType);
            }

            for (int i = 0; i < Cards.Length; i++)
            {
                SuitedCard card1 = this.Cards[i] as SuitedCard;
                SuitedCard card2 = other.Cards[i] as SuitedCard;

                int diff = card1?.Kind.CompareTo(card2?.Kind) ?? 0;
                if (diff != 0)
                {
                    return diff;
                }
            }

            return 0;
        }
    }
}