namespace HIMAIKFinanceMonitorApp.Server.Dtos
{
    public class ResetPasswordDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
    }
}
