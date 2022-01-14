using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayingCardsAPI.Entities;

namespace PlayingCardsAPI.Repositories
{
	public interface IDeckRepository
	{
		Task<CardDeck> GetCardDeckAsync(Guid id);
		Task<IEnumerable<CardDeck>> GetCardDecksAsync();
		Task CreateCardDeckAsync(CardDeck deck);
		Task UpdateCardDeck(CardDeck deck);
		Task DeleteDeckAsync(Guid deckId);
	}
}