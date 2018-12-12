﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;

namespace StockGameService.Models
{
    public class ReadyUsers
    {
        List<UserItem> _readyUsers = new List<UserItem>();
        public ReadyUsers(List<UserItem> users)
        {
            _readyUsers = users;
        }
        public List<UserItem> UsersThatAreReady
        {
            get
            {
                return _readyUsers;
            } 
        }
    }
}