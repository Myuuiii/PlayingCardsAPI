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

		[HttpGet(Name = "Get All Decks")]
		public async Task<ActionResult<IEnumerable<CardDeckDto>>> GetDecksAsync()
		{
			IEnumerable<CardDeckDto> decks = (await _repository.GetCardDecksAsync()).Select(deck => deck.AsDto());
			return Ok(decks);
		}

		[HttpGet("{id}", Name = "Get Deck")]
		public async Task<ActionResult<CardDeckDto>> GetDeckAsync(Guid id)
		{
			CardDeckDto deck = (await _repository.GetCardDeckAsync(id)).AsDto();
			if (deck is null)
				return NotFound();
			return Ok(deck);
		}

		[HttpPost(Name = "Create Deck")]
		public async Task<ActionResult<CardDeckDto>> CreateDeckAsync([FromBody] CreateDeckDto deckDto)
		{
			CardDeck deck = new()
			{
				Id = Guid.NewGuid(),
				DeckType = deckDto.DeckType,
				ShoeDeckCount = deckDto.ShoeDeckCount
			};
			await deck.FillDeckAsync();
			await _repository.CreateCardDeckAsync(deck);
			return CreatedAtRoute("Get Deck", new { id = deck.Id }, deck.AsDto());
		}

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

			await _repository.UpdateCardDeck(updatedDeck);
			return NoContent();
		}

		[HttpDelete("{id}", Name = "Delete Deck")]
		public async Task<ActionResult> DeleteDeckAsync(Guid id)
		{
			CardDeck existingDeck = await _repository.GetCardDeckAsync(id);
			if (existingDeck is null)
				return NotFound();
			await _repository.DeleteDeckAsync(id);
			return NoContent();
		}
	}
}