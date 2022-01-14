using System;

namespace PlayingCardsAPI.Dtos
{
	public record PlayingCardDto
	{
		public Guid Id { get; init; }
		public String Suit { get; init; }
		public String Value { get; init; }
		public String Code { get; init; }
		public Boolean CardPlayed { get; set; }
	}
}