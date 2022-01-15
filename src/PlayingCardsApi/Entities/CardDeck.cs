using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayingCardsAPI.Dtos;

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

		/// <summary>
		/// Initially fills the deck with the amount of cards in the deck type mulitplied by the shoe deck count
		/// </summary>
		public void FillDeck()
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
		}

		/// <summary>
		/// Adds a card to the deck with the specific card code
		/// </summary>
		/// <param name="cardCode"></param>
		public void AddCard(String cardCode)
		{
			Cards.Add(new PlayingCard(cardCode));
		}

		/// <summary>
		/// Removes a card from the deck with the specific card code
		/// </summary>
		/// <param name="cardCode">Card code</param>
		public void RemoveCard(string cardCode)
		{
			Cards.Remove(Cards.FirstOrDefault(c => c.Code == cardCode));
		}

		/// <summary>
		/// Retrieves the card with the given id from the deck
		/// </summary>
		/// <param name="id">Card id</param>
		/// <returns></returns>
		public PlayingCard GetCard(Guid id)
		{
			return Cards.Where(card => card.Id == id).FirstOrDefault();
		}

		/// <summary>
		/// Draw a single card from the deck
		/// </summary>
		/// <returns></returns>
		public PlayingCard DrawCard()
		{
			List<PlayingCard> unplayedCards = Cards.Where(card => !card.CardPlayed).ToList();
			PlayingCard card = unplayedCards[new Random().Next(0, unplayedCards.Count)];
			card.Play();
			return card;
		}

		/// <summary>
		/// Draw count amount of cards
		/// </summary>
		/// <param name="count">Cards to draw from the deck</param>
		/// <returns></returns>
		public IEnumerable<PlayingCard> DrawCards(int count)
		{
			List<PlayingCard> cards = new();
			for (int i = 0; i < count; i++)
			{
				cards.Add(this.DrawCard());
			}

			return cards;
		}

		/// <summary>
		/// Draws setCount groups of count cards from the deck
		/// </summary>
		/// <param name="count">Amount of cards to draw per set</param>
		/// <param name="setCount">Amount of sets to draw</param>
		/// <returns></returns>
		public IEnumerable<IEnumerable<PlayingCard>> DrawSets(int count, int setCount)
		{
			List<List<PlayingCard>> sets = new();
			for (int i = 0; i < setCount; i++)
			{
				sets.Add(this.DrawCards(count).ToList());
			}

			return sets;
		}

		/// <summary>
		/// Returns all the cards to the deck
		/// </summary>
		public void Shuffle()
		{
			foreach (PlayingCard card in Cards.Where(card => card.CardPlayed))
			{
				card.ReturnToDeck();
			}
		}

		/// <summary>
		/// Returns wether the deck contains a card with the given code
		/// </summary>
		/// <param name="cardCode">Card code</param>
		/// <returns></returns>
		public Boolean ContainsCard(string cardCode)
		{
			return Cards.Any(card => card.Code == cardCode);
		}
	}
}
