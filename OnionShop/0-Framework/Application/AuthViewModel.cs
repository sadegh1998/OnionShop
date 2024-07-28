namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public AuthViewModel(long id, long roleId, string userName, string fullName)
        {
            Id = id;
            RoleId = roleId;
            UserName = userName;
            FullName = fullName;
        }
    }
}
