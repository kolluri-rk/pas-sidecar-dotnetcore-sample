using System.Collections.Generic;
using System.Linq;

namespace auth_server_sidecar.Models
{
    public static class InMemoryAppUsersRepository
    {
        public static AppUser GetScopesForUserId(long appId, long userId)
        {
            var user = AppUsers().Find(u => u.AppId == appId && u.Id == userId);
            return user;
        }


        private static List<AppUser> AppUsers()
        {
            var users = new List<AppUser>
            {
                new AppUser { Id = 1, AppId = 1, UserName = "stark", Scopes = new string[] {"admin", "todo_read, todo_write"} },

                new AppUser { Id = 1, AppId = 2, UserName = "stark", Scopes = new string[] {"basic_user", "todo_read"} },

                new AppUser { Id = 1, AppId = 3, UserName = "stark", Scopes = new string[] {"admin"} }
            };

            return users;
        }
    }
}
