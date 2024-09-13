using System.ComponentModel.DataAnnotations;

namespace game_store.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Range(0, 120)] decimal Price,
    DateOnly Release);