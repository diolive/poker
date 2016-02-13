using DioLive.Orion.Cards.Core;
using System.Collections.Generic;

namespace DioLive.Orion.Cards.Classic
{
    public abstract class SuitedDeck : Deck<SuitedCard>
    {
        protected SuitedDeck(Suit[] suits, Kind[] kinds)
            : base(GenerateCards(suits, kinds))
        {
        }

        private static IEnumerable<SuitedCard> GenerateCards(Suit[] suits, Kind[] kinds)
        {
            foreach (Suit suit in suits)
            {
                foreach (Kind value in kinds)
                {
                    yield return new SuitedCard(value, suit);
                }
            }
        }
    }
}