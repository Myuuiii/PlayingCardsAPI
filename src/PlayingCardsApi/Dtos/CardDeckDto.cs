using System;
using System.Collections.Generic;
using PlayingCardsAPI.Entities;

namespace PlayingCardsAPI.Dtos
{
	public record CardDeckDto
	{
		public Guid Id { get; init; }
		public int ShoeDeckCount { get; init; }

		public int CardsInDeck { get; init; }
		public String DeckType { get; init; }

		public List<PlayingCardDto> Cards { get; init; }
	}
}