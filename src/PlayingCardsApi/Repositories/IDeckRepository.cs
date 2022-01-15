using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayingCardsAPI.Entities;

namespace PlayingCardsAPI.Repositories
{
	public interface IDeckRepository
	{
		/// <summary>
		/// Retrieves a card deck by id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <returns></returns>
		Task<CardDeck> GetCardDeckAsync(Guid id);

		/// <summary>
		/// Retrieves all card decks
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<CardDeck>> GetCardDecksAsync();

		/// <summary>
		/// Creates a new card deck
		/// </summary>
		/// <param name="deck">Deck object</param>
		/// <returns></returns>
		Task CreateCardDeckAsync(CardDeck deck);

		/// <summary>
		/// Shuffles the deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <returns></returns>
		Task ShuffleCardDeckAsync(Guid deckId);

		/// <summary>
		/// Updates the card deck with the given id
		/// </summary>
		/// <param name="deck">Deck id</param>
		/// <returns></returns>
		Task UpdateCardDeckAsync(CardDeck deck);

		/// <summary>
		/// Deletes the card deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <returns></returns>
		Task DeleteDeckAsync(Guid deckId);

		/// <summary>
		/// Retrieves the amount of cards that are still in the deck
		/// </summary>
		/// <param name="deckId"></param>
		/// <returns></returns>
		Task<int> GetAvailableCardCountAsync(Guid deckId);

		/// <summary>
		/// Retrieve a card from the deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <param name="cardId">Card id</param>
		/// <returns></returns>
		Task<PlayingCard> GetCardAsync(Guid deckId, Guid cardId);

		/// <summary>
		/// Retrieve all cards from the deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <returns></returns>
		Task<IEnumerable<PlayingCard>> GetCardsAsync(Guid deckId);

		/// <summary>
		/// Draw sets of cards from a deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <param name="count">Amount of cards per set</param>
		/// <param name="sets">Amount of sets</param>
		/// <returns></returns>
		Task<IEnumerable<IEnumerable<PlayingCard>>> DrawSetsAsync(Guid deckId, int count, int sets);

		/// <summary>
		/// Draw a single card from the deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <returns></returns>
		Task<PlayingCard> DrawCardAsync(Guid deckId);

		/// <summary>
		/// Draw an amount of cards from the deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <param name="count">Amount of cards to draw</param>
		/// <returns></returns>
		Task<IEnumerable<PlayingCard>> DrawCardsAsync(Guid deckId, int count);

		/// <summary>
		/// Add a card to the deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <param name="code">Card code</param>
		/// <returns></returns>
		Task AddCardAsync(Guid deckId, String code);

		/// <summary>
		/// Remove a card from the deck with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <param name="cardId">Card code</param>
		/// <returns></returns>
		Task RemoveCardAsync(Guid deckId, String cardId);

		/// <summary>
		/// Checks if the deck with the given id exists
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <returns></returns>
		Task<Boolean> DeckExistsAsync(Guid deckId);

		/// <summary>
		/// Checks if the deck contains a card with the given id
		/// </summary>
		/// <param name="deckId">Deck id</param>
		/// <param name="cardCode">Card code</param>
		/// <returns></returns>
		Task<Boolean> DeckContainsCardAsync(Guid deckId, String cardCode);
	}
}