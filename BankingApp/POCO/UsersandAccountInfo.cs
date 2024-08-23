using System;
using System.Collections.Generic;

namespace BankingApp.POCO;

public partial class UsersandAccountInfo
{
    public int AccNo { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public string? AccName { get; set; }

    public string? AccType { get; set; }

    public double? AccBalance { get; set; }

    public bool IsAccActive { get; set; } = true;

    public virtual ICollection<NewRequest> NewRequests { get; set; } = new List<NewRequest>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
