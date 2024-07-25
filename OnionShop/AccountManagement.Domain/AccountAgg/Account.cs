using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string FullName { get;private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public long RoleId { get; private set; }
        public string ProfilePicture { get; private set; }

        public Account(string fullName, string username, string password, string mobile, long roleId, string profilePicture)
        {
            FullName = fullName;
            Username = username;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;
            ProfilePicture = profilePicture;
        }
        public void Edit(string fullName, string username, string mobile, long roleId, string profilePicture)
        {
            FullName = fullName;
            Username = username;
            Mobile = mobile;
            RoleId = roleId;
            if (!string.IsNullOrWhiteSpace(profilePicture))
            {
                ProfilePicture = profilePicture;
            }
        }
        public void ChanagePassword(string password)
        {
            Password = password;
        }
    }

}
