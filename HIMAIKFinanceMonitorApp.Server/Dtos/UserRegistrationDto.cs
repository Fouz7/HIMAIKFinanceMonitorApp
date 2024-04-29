namespace HIMAIKFinanceMonitorApp.Server.Dtos
{
    public class UserRegistrationDto
    {
        public string Username { get; set; } = "";
        public string Fullname { get; set; } = "";
        public string NIM { get; set; } = "";
        public string Pic { get; set; } = "";
        public string Password { get; set; } = "";

        public string ConfirmPassword { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
