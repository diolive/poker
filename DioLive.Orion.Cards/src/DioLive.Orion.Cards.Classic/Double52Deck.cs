namespace DioLive.Orion.Cards.Classic
{
    public class Double52Deck : SuitedDeck
    {
        private readonly static Suit[] _suits;
        private readonly static Kind[] _kinds;

        static Double52Deck()
        {
            _suits = new[] { Suit.Spades, Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades, Suit.Clubs, Suit.Diamonds, Suit.Hearts };
            _kinds = new[] { Kind.Ace, Kind.Two, Kind.Three, Kind.Four, Kind.Five, Kind.Six, Kind.Seven, Kind.Eight, Kind.Nine, Kind.Ten, Kind.Jack, Kind.Queen, Kind.King };
        }

        public Double52Deck()
            : base(_suits, _kinds)
        {
        }
    }
}