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
        [PrimaryKey, MaxLength(10)]
        public Int32 user_id { get; set; }
        [MaxLength(86)]
        public String token { get; set; }
        [MaxLength(1)]
        public Boolean isActive { get; set; }
    }
}
