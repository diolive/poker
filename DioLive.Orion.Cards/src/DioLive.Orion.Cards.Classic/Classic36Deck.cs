namespace DioLive.Orion.Cards.Classic
{
    public class Classic36Deck : SuitedDeck
    {
        private readonly static Suit[] _suits;
        private readonly static Kind[] _kinds;

        static Classic36Deck()
        {
            _suits = new[] { Suit.Spades, Suit.Clubs, Suit.Diamonds, Suit.Hearts };
            _kinds = new[] { Kind.Six, Kind.Seven, Kind.Eight, Kind.Nine, Kind.Ten, Kind.Jack, Kind.Queen, Kind.King, Kind.Ace };
        }

        public Classic36Deck()
            : base(_suits, _kinds)
        {
        }
    }
}