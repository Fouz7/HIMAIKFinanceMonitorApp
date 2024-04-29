namespace HIMAIKFinanceMonitorApp.Server.Dtos
{
    public class UserDto
    {
        public string Name { get; set; } = "";
        public string Fullname { get; set; } = "";
        public string NIM { get; set; } = "";
        public string Pic { get; set; } = "";
        public string Role { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
