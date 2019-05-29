namespace CcRep.Components.Auth
{
    public class ActiveDirctoryUser
    {
        public string username;
        public string email;
        public string name;

        public ActiveDirctoryUser(string username, string name, string email)
        {
            this.username = username.Trim().ToLower();
            this.name = name;
            this.email = email;
        }
    }
}