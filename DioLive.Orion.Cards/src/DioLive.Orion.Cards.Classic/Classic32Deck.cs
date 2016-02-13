namespace DioLive.Orion.Cards.Classic
{
    public class Classic32Deck : SuitedDeck
    {
        private readonly static Suit[] _suits;
        private readonly static Kind[] _kinds;

        static Classic32Deck()
        {
            _suits = new[] { Suit.Spades, Suit.Clubs, Suit.Diamonds, Suit.Hearts };
            _kinds = new[] { Kind.Seven, Kind.Eight, Kind.Nine, Kind.Ten, Kind.Jack, Kind.Queen, Kind.King, Kind.Ace };
        }

        public Classic32Deck()
            : base(_suits, _kinds)
        {
        }
    }
}