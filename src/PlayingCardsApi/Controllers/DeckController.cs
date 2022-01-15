using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlayingCardsAPI.Dtos;
using PlayingCardsAPI.Entities;
using PlayingCardsAPI.Repositories;

namespace PlayingCardsAPI.Controllers
{
	[ApiController]
	[Route("deck")]
	public class DeckController : ControllerBase
	{
		private readonly IDeckRepository _repository;

		public DeckController(IDeckRepository repository)
		{
			this._repository = repository;
		}

		/// <summary>
		/// Retieve all decks
		/// </summary>
		/// <returns></returns>
		[HttpGet(Name = "Get All Decks")]
		public async Task<ActionResult<IEnumerable<CardDeckDto>>> GetDecksAsync()
		{
			IEnumerable<CardDeckDto> decks = (await _repository.GetCardDecksAsync()).Select(deck => deck.AsDto());
			return Ok(decks);
		}

		/// <summary>
		/// Retrieve the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <returns></returns>
		[HttpGet("{id}", Name = "Get Deck")]
		public async Task<ActionResult<CardDeckDto>> GetDeckAsync(Guid id)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			return Ok((await _repository.GetCardDeckAsync(id)).AsDto());
		}

		/// <summary>
		/// Create a new deck
		/// </summary>
		/// <param name="deckDto">Deck DTO</param>
		/// <returns></returns>
		[HttpPost(Name = "Create Deck")]
		public async Task<ActionResult<CardDeckDto>> CreateDeckAsync([FromBody] CreateDeckDto deckDto)
		{
			CardDeck deck = new()
			{
				Id = Guid.NewGuid(),
				DeckType = deckDto.DeckType,
				ShoeDeckCount = deckDto.ShoeDeckCount
			};
			deck.FillDeck();
			await _repository.CreateCardDeckAsync(deck);
			return CreatedAtRoute("Get Deck", new { id = deck.Id }, deck.AsDto());
		}

		/// <summary>
		/// Update the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <param name="deckDto">Deck DTO</param>
		/// <returns></returns>
		[HttpPut("{id}", Name = "Update Deck")]
		public async Task<ActionResult> UpdateDeckAsync(Guid id, [FromBody] UpdateDeckDto deckDto)
		{
			CardDeck existingDeck = await _repository.GetCardDeckAsync(id);

			if (existingDeck is null)
				return NotFound();

			CardDeck updatedDeck = existingDeck with
			{
				DeckType = deckDto.DeckType,
				ShoeDeckCount = deckDto.ShoeDeckCount
			};

			await _repository.UpdateCardDeckAsync(updatedDeck);
			return NoContent();
		}

		/// <summary>
		/// Delete the deck with the given id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}", Name = "Delete Deck")]
		public async Task<ActionResult> DeleteDeckAsync(Guid id)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			await _repository.DeleteDeckAsync(id);
			return NoContent();
		}

		/// <summary>
		/// Draw a random card from the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <returns></returns>
		[HttpGet("draw/{id}", Name = "Draw Card")]
		public async Task<ActionResult<PlayingCardDto>> DrawCardAsync(Guid id)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			if (await _repository.GetAvailableCardCountAsync(id) == 0)
				return BadRequest($"Not enough cards in deck ({await _repository.GetAvailableCardCountAsync(id)} available, 1 requested)");

			PlayingCard card = await _repository.DrawCardAsync(id);
			if (card is null)
				return NoContent();

			return Ok(card.AsDto());
		}

		/// <summary>
		/// Draw the given amount of cards from the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <param name="count">Amount of cards to draw</param>
		/// <returns></returns>
		[HttpGet("draw/{id}/{count}", Name = "Draw Cards")]
		public async Task<ActionResult<IEnumerable<PlayingCardDto>>> DrawCardsAsync(Guid id, int count)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			if (count > await _repository.GetAvailableCardCountAsync(id))
				return BadRequest($"Not enough cards in deck ({await _repository.GetAvailableCardCountAsync(id)} available, {count} requested)");

			IEnumerable<PlayingCard> cards = await _repository.DrawCardsAsync(id, count);
			if (cards is null)
				return NoContent();

			return Ok(cards.Select(card => card.AsDto()));
		}

		/// <summary>
		/// Draw a given amount of sets of a given amount of cards from the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <param name="count">Amount of cards per set</param>
		/// <param name="sets">Amount of sets</param>
		/// <returns></returns>
		[HttpGet("draw/{id}/{count}/{sets}", Name = "Draw sets of cards")]
		public async Task<ActionResult<IEnumerable<PlayingCardDto>>> DrawSetsAsync(Guid id, int count, int sets)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			if (sets * count > await _repository.GetAvailableCardCountAsync(id))
				return BadRequest($"Not enough cards in deck ({await _repository.GetAvailableCardCountAsync(id)} available, {sets * count} requested)");

			IEnumerable<IEnumerable<PlayingCardDto>> cardSets = (await _repository.DrawSetsAsync(id, count, sets)).Select(cards => cards.Select(card => card.AsDto()));
			if (cardSets is null)
				return NoContent();

			return Ok(cardSets);
		}

		/// <summary>
		/// Retrieve the card with the given id from the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <param name="cardId">Card id</param>
		/// <returns></returns>
		[HttpGet("{id}/card/{cardId}", Name = "Get Card From Deck")]
		public async Task<ActionResult<PlayingCardDto>> GetCardAsync(Guid id, Guid cardId)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			PlayingCard card = await _repository.GetCardAsync(id, cardId);
			if (card is null)
				return NotFound("Card not found in deck");

			return Ok(card.AsDto());
		}

		/// <summary>
		/// Shuffle the cards in the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <returns></returns>
		[HttpPatch("{id}/shuffle", Name = "Shuffle Deck")]
		public async Task<ActionResult> ShuffleDeckAsync(Guid id)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			await _repository.ShuffleCardDeckAsync(id);
			return NoContent();
		}

		/// <summary>
		/// Add a card with the given code to the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <param name="code">Card code</param>
		/// <returns></returns>
		[HttpPatch("{id}/add/card/{code}", Name = "Add single card to deck")]
		public async Task<ActionResult> AddCardToDeckAsync(Guid id, string code)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			await _repository.AddCardAsync(id, code);
			return NoContent();
		}

		/// <summary>
		/// Remove a card with the given code from the deck with the given id
		/// </summary>
		/// <param name="id">Deck id</param>
		/// <param name="code">Card code</param>
		/// <returns></returns>
		[HttpDelete("{id}/remove/card/{code}", Name = "Remove single card from deck")]
		public async Task<ActionResult> RemoveCardFromDeckAsync(Guid id, string code)
		{
			if (await _repository.DeckExistsAsync(id) == false)
				return NotFound();

			if (await _repository.DeckContainsCardAsync(id, code))
			{
				await _repository.RemoveCardAsync(id, code);
				return NoContent();
			}

			return NotFound("No card with that code found in deck");
		}
	}
}