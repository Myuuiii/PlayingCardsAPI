using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayingCardsAPI.Entities
{
	public class CardDeck
	{
		public CardDeck() { }
		public CardDeck(int shoeSize = 1) { this.ShoeDeckSize = shoeSize; }
		public CardDeck(DeckType deckType) { this.DeckType = deckType; }
		public CardDeck(DeckType deckType, int shoeSize) { this.DeckType = deckType; this.ShoeDeckSize = shoeSize; }

		public int ShoeDeckSize { get; set; } = 1;
		public DeckType DeckType { get; set; } = DeckType.Regular;
		public List<PlayingCard> Cards { get; set; } = new();

		public Task FillDeck()
		{
			for (int currentDeck = 1; currentDeck <= this.ShoeDeckSize; currentDeck++)
			{
				foreach (SuitValue suit in Enum.GetValues(typeof(SuitValue)))
				{
					foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
					{
						// Skip if Stripped 24 and lower than 9
						// Skip if Stripped 32 and lower than 6
						// Skip if JokerLess and value is joker
						if (
							(this.DeckType == DeckType.Stripped24 && (value < CardValue.Nine || value == CardValue.Joker)) ||
						 	(this.DeckType == DeckType.Stripped32 && (value < CardValue.Six || value == CardValue.Joker)) ||
							(this.DeckType == DeckType.NoJokers && value == CardValue.Joker))
						{
							continue;
						}

						// Duplicate if Stripped 48 and higher than or equal to 9 but not a joker
						if (this.DeckType == DeckType.Stripped48 && (value >= CardValue.Nine && value != CardValue.Joker))
						{
							Cards.Add(new PlayingCard(suit, value));
						}

						Cards.Add(new PlayingCard(suit, value));
					}
				}
			}

			return Task.CompletedTask;
		}
	}
}
