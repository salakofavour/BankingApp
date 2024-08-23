using BankingApp.POCO;

public class UsersOperation{

    BankingAppContext DB = new BankingAppContext();
    DateTime currentTime = new DateTime();

    int? VerifiedUserAccNo  = 0;//0 is just a placeholder incase there is no AccNo which is not posible because the AccNo
    // is the primary key in the db, and as such a user cannot be created without an AccNo.

    //with the variable above there is no need for the verifying if statement in each method to confirm that account exists 
    //because we are sure it exists if the user is able to sign in

public bool isUser(string USERNAME, string PASSWORD){
    var UserExists = (from a in DB.UsersandAccountInfos where a.UserName == USERNAME && a.UserPassword == PASSWORD select a).SingleOrDefault();
    
    if(UserExists != null){
        if(UserExists.IsAccActive){
            VerifiedUserAccNo = UserExists.AccNo;
            return true;
        }else{
            throw new Exception("Your Account exists but is disabled, Reach out to us on a business day if you want to reactivate it");
        }
    }else{
        return false;
    }
}

public void DisplayUserMenu(){
    Console.WriteLine("Choose an Action to execute");
    Console.WriteLine();
    Console.WriteLine("1. Check Account Details");
    Console.WriteLine("2. WithDraw");
    Console.WriteLine("3. Deposit");
    Console.WriteLine("4. Transfer");
    Console.WriteLine("5. Last 5 Transactions");
    Console.WriteLine("6. Request Cheque Book");
    Console.WriteLine("7. Change Password");
    Console.WriteLine("8. Exit");
    Console.WriteLine();
}

public void CheckAccountDetails(){
    var accountDetails = (from a in DB.UsersandAccountInfos where a.AccNo == VerifiedUserAccNo select a).SingleOrDefault();
    Console.WriteLine($"Hi {accountDetails.AccName},");
    Console.WriteLine();
    Console.WriteLine($"You have an Active {accountDetails.AccType} account with a balance of{accountDetails.AccBalance}");
    Console.WriteLine();
}

public void WithDraw(double amount){
    var accountDetails = (from a in DB.UsersandAccountInfos where a.AccNo == VerifiedUserAccNo select a).SingleOrDefault();

    if(amount > accountDetails.AccBalance){
        throw new Exception($"You have {accountDetails.AccBalance}, you cannot withdraw more than you have");
    }else{
        accountDetails.AccBalance -= amount;
        DB.UsersandAccountInfos.Update(accountDetails);
        //DB.SaveChanges();
        string type = "Withdrawal";
        string detail = "The amount of " + amount +" was withdrawn";

    addTransactionToDB(type, detail);
    Console.WriteLine();
    }
}

public void Deposit(double amount){
    var accountDetails = (from a in DB.UsersandAccountInfos where a.AccNo == VerifiedUserAccNo select a).SingleOrDefault();

    if(amount > 0){
        accountDetails.AccBalance += amount;
        //User.AccBalance = accountDetails.AccBalance;

        DB.UsersandAccountInfos.Update(accountDetails);
        DB.SaveChanges();
        string type = "Deposit";
        string detail = "The amount of " + amount +" was deposited into your account";
        addTransactionToDB(type, detail);
        Console.WriteLine();
    }else{
        throw new Exception("You cannot deposit 0 or a negative amount");
        // Will add transaction to transaction table
    }
}

public void Transfer(double amount, int toAccountNo){
    var accountDetails = (from a in DB.UsersandAccountInfos where a.AccNo == VerifiedUserAccNo select a).SingleOrDefault();
    var accountDetails2 = (from a in DB.UsersandAccountInfos where a.AccNo == toAccountNo select a).SingleOrDefault();

    if(amount > accountDetails.AccBalance){
        throw new Exception("You cannot send more than you have in your account");
    }else if(accountDetails2 == null){
        throw new Exception("The recepient account does not exist");
    }else if(!accountDetails2.IsAccActive){
        throw new Exception("The recepient account exists, but is not active, and so cannot receive funds");
    }else{
        accountDetails.AccBalance -= amount; //remove from your account
        //User.AccBalance = accountDetails.AccBalance;
        DB.UsersandAccountInfos.Update(accountDetails);
        //will add to the transaction table here and at the other places

        accountDetails2.AccBalance += amount; //add to recipients account
        //User.AccBalance = accountDetails.AccBalance;
        DB.UsersandAccountInfos.Update(accountDetails);
        string type = "Transfer";
        string detailFrom = "You made a transfer of " + amount + " to account " + toAccountNo;
        string detailTo = "You received a transfer of " + amount + " from account " + VerifiedUserAccNo;

        addTransactionToDB(type, detailFrom);
        
        // var addTransaction = (from a in DB.Transactions where a.AccNo == toAccountNo select a).ToList();
        Transaction Trs = new Transaction();
        Trs.AccNo = toAccountNo;
        Trs.TransactionType = type;
        Trs.TransactionDetails = detailTo;
        Trs.TransactionTime = currentTime.ToString();
        DB.Transactions.Add(Trs);

        DB.SaveChanges();
        Console.WriteLine();
    }

}

public void Last5Transactions(){
    var transactions = (from a in DB.Transactions where a.AccNo == VerifiedUserAccNo orderby a.TransactionId descending select a).ToList();
    if(transactions.Count >= 5){
        for (int i=0; i<5; i++){

            string TNtype = transactions[i].TransactionType;
            string TNDetails = transactions[i].TransactionDetails;
            Console.WriteLine($"Transaction Type: {TNtype}.");
            Console.WriteLine($"Transaction Details: {TNDetails}.");
            Console.WriteLine();

        }
    }else{
        foreach( var transact in transactions){

            string TNtype = transact.TransactionType;
            string TNDetails = transact.TransactionDetails;
            Console.WriteLine($"Transaction Type: {TNtype}.");
            Console.WriteLine($"Transaction Details: {TNDetails}.");
            Console.WriteLine();
        }
    }

}

public void RequestChequeBook(string requestDescription){
    //the idea is when pending is true approved is false and when the admin approves it, then pending is false
    var request = (from a in DB.NewRequests where a.AccNo == VerifiedUserAccNo orderby a.RequestId select a).LastOrDefault();

    NewRequest NR = new NewRequest();


    if(request != null){
        if(request.Pending){
        throw new Exception("You have a cheque book request already pending");
        }
    }else{
        NR.AccNo = VerifiedUserAccNo;
        NR.RequestDescription = requestDescription;
        NR.Pending = true;
        NR.Approved = false;

        DB.NewRequests.Add(NR);
        DB.SaveChanges();
        Console.WriteLine("Your chequebook request was received and is pending");

    var request2 = (from a in DB.NewRequests where a.AccNo == VerifiedUserAccNo orderby a.RequestId select a).LastOrDefault();
    Console.WriteLine($"Your request ID is {request2.RequestId}, Make sure to note it for future use");
    Console.WriteLine();


}
}


public void ChangePassword(string newPassword){
    var accountDetails = (from a in DB.UsersandAccountInfos where a.AccNo == VerifiedUserAccNo select a).SingleOrDefault();

    if(accountDetails.UserPassword == newPassword){
        throw new Exception("You cannot reuse the same password");

    }else{
        accountDetails.UserPassword = newPassword;
        DB.UsersandAccountInfos.Update(accountDetails);
        DB.SaveChanges();
        Console.WriteLine();
    }
}


//general helper function to use in all functions so that I can just call it
public void addTransactionToDB(string transactionType, string transactionDetails){
    var addTransaction = (from a in DB.Transactions where a.AccNo == VerifiedUserAccNo select a).ToList();

        Transaction Trs = new Transaction();
        Trs.AccNo = VerifiedUserAccNo;
        Trs.TransactionType = transactionType;
        Trs.TransactionDetails = transactionDetails;
        Trs.TransactionTime = currentTime.ToString();
        DB.Transactions.Add(Trs);
        DB.SaveChanges();
    // if(addTransaction.Count <1){
    //     Transaction Trs = new Transaction();
    //     Trs.AccNo = VerifiedUserAccNo;
    //     Trs.TransactionType = transactionType;
    //     Trs.TransactionDetails = transactionDetails;
    //     DB.Transaction.Add(Trs);
    //     DB.SaveChanges();
    // }else{
    //     Trs.AccNo = VerifiedUserAccNo;
    //     Trs.TransactionType = transactionType;
    //     Trs.TransactionDetails = transactionDetails;
    //     DB.Transaction.Add(Trs);
    //     DB.SaveChanges();

    
    // }


}



}