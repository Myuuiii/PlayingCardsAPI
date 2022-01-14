using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayingCardsAPI.Entities
{
	public record CardDeck
	{
		public CardDeck() { }
		public CardDeck(int shoeSize = 1) { this.ShoeDeckCount = shoeSize; }
		public CardDeck(DeckType deckType) { this.DeckType = deckType; }
		public CardDeck(DeckType deckType, int shoeSize) { this.DeckType = deckType; this.ShoeDeckCount = shoeSize; }


		public Guid Id { get; set; }
		public int ShoeDeckCount { get; set; } = 1;
		public DeckType DeckType { get; set; } = DeckType.Regular;
		public List<PlayingCard> Cards { get; set; } = new();

		public Task FillDeckAsync()
		{
			for (int currentDeck = 1; currentDeck <= this.ShoeDeckCount; currentDeck++)
			{
				foreach (SuitValue suit in Enum.GetValues(typeof(SuitValue)))
				{
					if (suit != SuitValue.Joker)
					{
						foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
						{
							// Do not insert Jokers for the suits
							if (value == CardValue.Joker) continue;

							// Skip if Stripped 24 and lower than 9
							// Skip if Stripped 32 and lower than 6
							// Skip if Stripped 48 and lower than 9
							if (
								(this.DeckType == DeckType.Stripped24 && (value < CardValue.Nine)) ||
								 (this.DeckType == DeckType.Stripped32 && (value < CardValue.Six)) ||
								(this.DeckType == DeckType.Stripped48 && (value < CardValue.Nine)))
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
					else if (suit == SuitValue.Joker && this.DeckType == DeckType.Regular)
					{
						Cards.Add(new PlayingCard(suit, CardValue.Joker));
						Cards.Add(new PlayingCard(suit, CardValue.Joker));
					}
				}
			}

			return Task.CompletedTask;
		}
	}
}
