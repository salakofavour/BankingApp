using System;
using System.Collections.Generic;

namespace BankingApp.POCO;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? AccNo { get; set; }

    public string? TransactionType { get; set; }

    public string? TransactionDetails { get; set; }

    public string? TransactionTime { get; set; }

    public virtual UsersandAccountInfo? AccNoNavigation { get; set; }
}
