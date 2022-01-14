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

			String suitName = Enum.GetName(typeof(SuitValue), suit);
			Char? valueName = ' ';
			switch (value)
			{
				case CardValue.Two: valueName = '2'; break;
				case CardValue.Three: valueName = '3'; break;
				case CardValue.Four: valueName = '4'; break;
				case CardValue.Five: valueName = '5'; break;
				case CardValue.Six: valueName = '6'; break;
				case CardValue.Seven: valueName = '7'; break;
				case CardValue.Eight: valueName = '8'; break;
				case CardValue.Nine: valueName = '9'; break;
				case CardValue.Ten: valueName = 'T'; break;
				case CardValue.Jack: valueName = 'J'; break;
				case CardValue.Queen: valueName = 'Q'; break;
				case CardValue.King: valueName = 'K'; break;
				case CardValue.Ace: valueName = 'A'; break;
				case CardValue.Joker: valueName = null; break;
			}

			this.Code = $"{suitName[0]}{valueName}";
		}
		public Guid Id { get; init; } = Guid.NewGuid();
		public SuitValue Suit { get; init; }
		public CardValue Value { get; init; }
		public String Code { get; init; }
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