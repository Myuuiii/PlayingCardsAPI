using System;

namespace PlayingCardsAPI.Entities
{
	public class PlayingCard
	{

		public PlayingCard() { }
		public PlayingCard(SuitValue suit, CardValue value)
		{
			this.Suit = suit;
			this.Value = value;
		}
		public Guid Id { get; init; } = Guid.NewGuid();
		public SuitValue Suit { get; init; }
		public CardValue Value { get; init; }
		public Boolean CardPlayed { get; private set; } = false;

		public void Play()
		{
			this.CardPlayed = true;
		}

		public void ReturnToDeck()
		{
			this.CardPlayed = false;
		}
	}
}