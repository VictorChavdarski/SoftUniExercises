﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }


    }
}
