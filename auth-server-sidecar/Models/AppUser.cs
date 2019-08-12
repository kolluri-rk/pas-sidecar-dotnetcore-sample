namespace auth_server_sidecar.Models
{
    public class AppUser
    {
        public long Id { get; set; }
        public long AppId { get; set; }
        public string UserName { get; set; }
        public string[] Scopes { get; set; }
    }
}
