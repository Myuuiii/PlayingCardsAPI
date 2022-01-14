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

		public async Task CreateCardDeckAsync(CardDeck deck)
		{
			_decks.Add(deck);
		}

		public async Task DeleteDeckAsync(Guid deckId)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deckId);
			_decks.RemoveAt(index);
		}

		public async Task<CardDeck> GetCardDeckAsync(Guid id)
		{
			return _decks.Where(deck => deck.Id == id).FirstOrDefault();
		}

		public async Task<IEnumerable<CardDeck>> GetCardDecksAsync()
		{
			return _decks;
		}

		public async Task UpdateCardDeck(CardDeck deck)
		{
			int index = _decks.FindIndex(existingDeck => existingDeck.Id == deck.Id);
			deck.Cards.Clear();
			await deck.FillDeckAsync();
			_decks[index] = deck;
		}
	}
}