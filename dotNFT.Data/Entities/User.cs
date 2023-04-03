﻿using System.ComponentModel.DataAnnotations;

namespace dotNFT.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Wallet Wallet { get; set; }
        public int WalletId { get; set; }
    }
}
