using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkapp
{
    public static class AuthorizationParameters
    {
        public static readonly Uri requestUri = new Uri("https://oauth.vk.com/authorize?client_id=4328474&scope=friends,groups,photos,audio,video,status,wall,notifications,offline&display=popup&v=5.21&response_type=token");
        public static readonly Uri callbackUri = new Uri("https://oauth.vk.com/blank.html");
    }
}
