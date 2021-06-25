using CC.Yi.ViewModel;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CC.Yi.Common
{
    public class ChatHub : Hub
    {
        public Task SendMsg(chat my)
        {
            //这里的Show代表是客户端的方法，具体可以细看SignalR的说明

            my.time = DateTime.Now.ToString();
            return Clients.All.SendAsync("Show", my);
        }
    }

}