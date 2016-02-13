using DioLive.Orion.Cards.Classic;
using System;
using System.Linq;

namespace DioLive.Orion.Games.Poker
{
    public class Table
    {
        private const int PRE_FLOP_SIZE = 2;
        private const int RIVER_SIZE = 5;
        private const int BURNED_COUNT = RIVER_SIZE - PRE_FLOP_SIZE; // 3
        private const int HAND_SIZE = 2;
        private const int DECK_SIZE = 52;

        private const int MAX_PLAYER_COUNT = (DECK_SIZE - RIVER_SIZE - BURNED_COUNT) / HAND_SIZE; // 22

        private readonly SuitedCard[][] _players;
        private readonly SuitedCard[] _river;
        private readonly Hand[] _hands;
        private readonly Hand _bestHand;

        public Table(int playerCount)
        {
            if (playerCount < 1)
            {
                throw new ArgumentException("Wrong player count", nameof(playerCount));
            }

            if (playerCount > MAX_PLAYER_COUNT)
            {
                throw new ArgumentException($"Player count cannot exceed {MAX_PLAYER_COUNT}", nameof(playerCount));
            }

            SuitedDeck deck = new Classic52Deck();
            deck.Randomize();

            _players = Enumerable.Range(0, playerCount)
                    .Select(_ => new SuitedCard[HAND_SIZE])
                    .ToArray();

            for (int i = 0; i < HAND_SIZE; i++)
            {
                for (int j = 0; j < playerCount; j++)
                {
                    _players[j][i] = deck.Take();
                }
            }

            _river = new SuitedCard[RIVER_SIZE];

            for (int i = 0; i < PRE_FLOP_SIZE; i++)
            {
                _river[i] = deck.Take();
            }

            for (int i = PRE_FLOP_SIZE; i < RIVER_SIZE; i++)
            {
                deck.Take(); // burn
                _river[i] = deck.Take();
            }

            _hands = Enumerable.Range(0, playerCount)
                    .Select(i => Hand.FindBestHand(_river.Union(_players[i]).ToArray()))
                    .ToArray();

            _bestHand = _hands.Max();
        }
    }
}