using System.ComponentModel.DataAnnotations;

namespace game_store.Dtos;

public record class CreateGameDto(
    [Required][StringLength(50)] string Name,
    int GenreId,
    [Range(0, 120)] decimal Price,
    DateOnly Release);