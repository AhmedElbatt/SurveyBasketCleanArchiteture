using System.ComponentModel.DataAnnotations;

namespace Application.Settings;
public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    [Required]
    public string Key { get; set; } = default!;

    [Required]
    public string Issuer { get; set; } = default!;

    [Required]
    public string Audience { get; set; } = default!;

    [Range(1, int.MaxValue)]
    public int ExpirationInMinutes { get; set; }
}
