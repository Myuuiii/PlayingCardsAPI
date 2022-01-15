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

			this.Code = $"{valueName}{suitName[0]}";
		}

		public PlayingCard(String cardCode)
		{
			this.Code = cardCode;

			if (cardCode.Length == 2)
			{
				switch (cardCode[1])
				{
					case 'C': this.Suit = SuitValue.Clubs; break;
					case 'D': this.Suit = SuitValue.Diamonds; break;
					case 'H': this.Suit = SuitValue.Hearts; break;
					case 'S': this.Suit = SuitValue.Spades; break;
					case 'J': this.Suit = SuitValue.Joker; break;
				}
			}
			else
			{
				this.Value = CardValue.Joker;
			}

			switch (cardCode[0])
			{
				case '2': this.Value = CardValue.Two; break;
				case '3': this.Value = CardValue.Three; break;
				case '4': this.Value = CardValue.Four; break;
				case '5': this.Value = CardValue.Five; break;
				case '6': this.Value = CardValue.Six; break;
				case '7': this.Value = CardValue.Seven; break;
				case '8': this.Value = CardValue.Eight; break;
				case '9': this.Value = CardValue.Nine; break;
				case 'T': this.Value = CardValue.Ten; break;
				case 'J': this.Value = CardValue.Jack; break;
				case 'Q': this.Value = CardValue.Queen; break;
				case 'K': this.Value = CardValue.King; break;
				case 'A': this.Value = CardValue.Ace; break;
			}
		}

		public Guid Id { get; init; } = Guid.NewGuid();
		public SuitValue Suit { get; init; }
		public CardValue Value { get; init; }
		public String Code { get; init; }
		public Boolean CardPlayed { get; private set; } = false;

		/// <summary>
		/// Set the card as drawn from the deck
		/// </summary>
		public void Play()
		{
			this.CardPlayed = true;
		}

		/// <summary>
		/// Return the card to the deck
		/// </summary>
		public void ReturnToDeck()
		{
			this.CardPlayed = false;
		}
	}
}