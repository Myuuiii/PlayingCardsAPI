using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayingCardsAPI.Entities;

namespace PlayingCardsAPI.Repositories
{
	public class InMemDeckRepository : IDeckRepository
	{
		private List<CardDeck> _decks = new List<CardDeck>();

		/// <inheritdoc />
		public async Task AddCardAsync(Guid deckId, string code)
		{
			int index = _decks.FindIndex(d => d.Id == deckId);
			CardDeck deck = _decks[index];
			deck.AddCard(code);
		}

		/// <inheritdoc />
		public async Task CreateCardDeckAsync(CardDeck deck)
		{
			_decks.Add(deck);
		}

		/// <inheritdoc />
		public async Task<bool> DeckContainsCardAsync(Guid deckId, string cardCode)
		{
			int index = _decks.FindIndex(d => d.Id == deckId);
			CardDeck deck = _decks[index];
			return deck.ContainsCard(cardCode);
		}

		/// <inheritdoc />
		public async Task<bool> DeckExistsAsync(Guid deckId)
		{
			return _decks.Any(d => d.Id == deckId);
		}

		/// <inheritdoc />
		public async Task DeleteDeckAsync(Guid deckId)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deckId);
			_decks.RemoveAt(index);
		}

		/// <inheritdoc />
		public async Task<PlayingCard> DrawCardAsync(Guid deckId)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deckId);
			CardDeck deck = _decks[index];
			PlayingCard card = deck.DrawCard();
			return card;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<PlayingCard>> DrawCardsAsync(Guid deckId, int count)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deckId);
			CardDeck deck = _decks[index];
			IEnumerable<PlayingCard> cards = deck.DrawCards(count);
			return cards;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<IEnumerable<PlayingCard>>> DrawSetsAsync(Guid deckId, int count, int sets)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deckId);
			CardDeck deck = _decks[index];
			IEnumerable<IEnumerable<PlayingCard>> cardSets = deck.DrawSets(count, sets);
			return cardSets;
		}

		/// <inheritdoc />
		public Task<int> GetAvailableCardCountAsync(Guid deckId)
		{
			CardDeck deck = _decks.FirstOrDefault(existingDeck => existingDeck.Id == deckId);
			return Task.FromResult(deck.Cards.Count(card => !card.CardPlayed));
		}

		/// <inheritdoc />
		public async Task<PlayingCard> GetCardAsync(Guid deckId, Guid cardId)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deckId);
			CardDeck deck = _decks[index];
			PlayingCard card = deck.Cards.FirstOrDefault(existingCard => existingCard.Id == cardId);
			return card;
		}

		/// <inheritdoc />
		public async Task<CardDeck> GetCardDeckAsync(Guid id)
		{
			return _decks.Where(deck => deck.Id == id).FirstOrDefault();
		}

		/// <inheritdoc />
		public async Task<IEnumerable<CardDeck>> GetCardDecksAsync()
		{
			return _decks;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<PlayingCard>> GetCardsAsync(Guid deckId)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deckId);
			CardDeck deck = _decks[index];
			return deck.Cards;
		}

		/// <inheritdoc />
		public async Task RemoveCardAsync(Guid deckId, string cardId)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deckId);
			CardDeck deck = _decks[index];
			deck.RemoveCard(cardId);
		}

		/// <inheritdoc />
		public async Task ShuffleCardDeckAsync(Guid id)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == id);
			CardDeck deck = _decks[index];
			deck.Shuffle();
		}

		/// <inheritdoc />
		public async Task UpdateCardDeckAsync(CardDeck deck)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deck.Id);
			_decks[index] = deck;
		}
	}
}