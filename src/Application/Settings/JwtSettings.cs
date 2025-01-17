namespace Application.Settings;
public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int ExpirationInMinutes { get; set; }
}
