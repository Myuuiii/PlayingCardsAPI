using System.ComponentModel.DataAnnotations;
using PlayingCardsAPI.Entities;

namespace PlayingCardsAPI.Dtos
{
	public record CreateDeckDto
	{
		[Required]
		public DeckType DeckType { get; init; }

		[Required]
		[Range(1, 64)]
		public int ShoeDeckCount { get; init; }
	}
}