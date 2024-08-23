using System;
using System.Collections.Generic;

namespace BankingApp.POCO;

public partial class NewRequest
{
    public int RequestId { get; set; }

    public int? AccNo { get; set; }

    public string? RequestDescription { get; set; }

    public bool Pending { get; set; } =true;

    public bool Approved { get; set; } =false;

    public virtual UsersandAccountInfo? AccNoNavigation { get; set; }
}
