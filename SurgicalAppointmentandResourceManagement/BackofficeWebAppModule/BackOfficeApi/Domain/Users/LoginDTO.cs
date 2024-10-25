namespace BackOfficeApi.Domain.Users
{
    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty; // Definir como string vazia por padrão
        public string Password { get; set; } = string.Empty; 
    }
}
