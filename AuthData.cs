using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace vkapp
{
    class AuthData
    {
        public AuthData()
        {
            this.firstname = "firstname";
            this.lastname = "lastname";
            this.profile_picture = "Assets/Account.png";
        }

        public AuthData(Int32 user_id, String token, Boolean isActive)
        {
            this.user_id = user_id;
            this.token = token;
            this.isActive = isActive;

            this.firstname = "firstname";
            this.lastname = "lastname";
            this.profile_picture = "Assets/Account.png";
        }

        public AuthData(Int32 user_id, String firstname, String lastname, String profile_picture, String token, Boolean isActive)
        {
            this.user_id = user_id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.profile_picture = profile_picture;
            this.token = token;
            this.isActive = isActive;
        }

        [PrimaryKey, MaxLength(10)]
        public Int32 user_id { get; set; }

        [MaxLength(20)]
        public String firstname { get; set; }

        [MaxLength(20)]
        public String lastname { get; set; }

        [MaxLength(150)]
        public String profile_picture { get; set; }

        [MaxLength(86)]
        public String token { get; set; }

        [MaxLength(1)]
        public Boolean isActive { get; set; }
    }
}
