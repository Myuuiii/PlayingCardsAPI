using System;
using System.Linq;
using PlayingCardsAPI.Dtos;
using PlayingCardsAPI.Entities;

namespace PlayingCardsAPI
{
	public static class Extensions
	{
		public static CardDeckDto AsDto(this CardDeck deck)
		{
			if (deck is null) return null;
			return new CardDeckDto
			{
				Id = deck.Id,
				ShoeDeckCount = deck.ShoeDeckCount,
				DeckType = Enum.GetName(typeof(DeckType), deck.DeckType),
				CardsInDeck = deck.Cards.Count,
				Cards = deck.Cards.Select(card => card.AsDto()).ToList()
			};
		}

		public static PlayingCardDto AsDto(this PlayingCard playingCard)
		{
			if (playingCard is null) return null;
			return new PlayingCardDto
			{
				Id = playingCard.Id,
				Suit = Enum.GetName(typeof(SuitValue), playingCard.Suit),
				Value = Enum.GetName(typeof(CardValue), playingCard.Value),
				Code = playingCard.Code,
				CardPlayed = playingCard.CardPlayed
			};
		}
	}
}