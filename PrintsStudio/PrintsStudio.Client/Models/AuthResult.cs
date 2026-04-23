namespace PrintsStudio.Client.Models
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
    }
}
