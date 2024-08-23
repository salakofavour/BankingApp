using BankingApp.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

public class  AdminOperations{

BankingAppContext DB = new BankingAppContext();
NewRequest Request = new NewRequest();

public bool isAdmin(string userName, string password){
    var AdminExists =  (from a in DB.Admins where a.AdminName == userName && a.AdminPassword==password
    select a).SingleOrDefault();
    
    if (AdminExists!= null){
        return true;
    }else{
        return false;
    }
}

public void AdminMenuOptions()
{
    Console.WriteLine("Choose an Action to execute");
    Console.WriteLine("1. Create New Account");
    Console.WriteLine("2. Delete Account");
    Console.WriteLine("3. Edit Account Details");
    Console.WriteLine("4. Display Summary");
    Console.WriteLine("5. Reset Customer Password");
    Console.WriteLine("6. Approve Cheque book request");
    Console.WriteLine("7. Exit");
    Console.WriteLine();
}

    // var AdminExists =  (from a in DB.Admins where a.AdminName == userName && a.AdminPassword==password
    // select a).Single


public void CreateNewUser(string userName, string password, int ACCNO, string ACCNAME, string ACCTYPE, double ACCBALANCE, bool ISACCACTIVE){

    var userExistsAlready = (from a in DB.UsersandAccountInfos where a.UserName == userName select a).SingleOrDefault(); // this does not exist yet
    UsersandAccountInfo User = new UsersandAccountInfo();

    if(userExistsAlready != null){
        // Console.WriteLine("userExists");
        throw new Exception("UserName already exists in the System");
    }else{
        // Console.WriteLine("Create User");
    User.UserName = userName;
    User.UserPassword = password;
    User.AccNo = ACCNO;
    User.AccName = ACCNAME;
    User.AccType = ACCTYPE;
    User.AccBalance = ACCBALANCE;
    User.IsAccActive = ISACCACTIVE;

    DB.UsersandAccountInfos.Add(User);
    DB.SaveChanges();
    Console.WriteLine("The user has been created");
    Console.WriteLine();
    }
}

public void DeleteUser(int ACCNO){// as a parameter
    
    var AccountExists = (from a in DB.UsersandAccountInfos where a.AccNo == ACCNO select a).SingleOrDefault();
    if(AccountExists != null){
        // AccountExists.AccNo = ACCNO;
        DB.UsersandAccountInfos.Remove(AccountExists);
        DB.SaveChanges();
        Console.WriteLine("The Account has been deleted");
        Console.WriteLine();
    }else{
        throw new Exception("The Account does not exist to delete");
    }
}

public void EditAccountDetails( int ACCNO, string ACCNAME, string ACCTYPE, double ACCBALANCE, bool ISACCACTIVE){

    var AccountExists = (from a in DB.UsersandAccountInfos where a.AccNo == ACCNO select a).SingleOrDefault();
    if(AccountExists != null){
        AccountExists.AccNo = ACCNO;
        AccountExists.AccName = ACCNAME;
        AccountExists.AccType = ACCTYPE;
        AccountExists.AccBalance = ACCBALANCE;
        AccountExists.IsAccActive = ISACCACTIVE;

        DB.UsersandAccountInfos.Update(AccountExists);
        DB.SaveChanges();
        Console.WriteLine("Account Edit Successful");
        Console.WriteLine();
    }else{
        throw new Exception("The Account does not Exist

public void DisplaySummary(int ACCNO){
    var AccountExists = (from a in DB.UsersandAccountInfos where a.AccNo == ACCNO select a).SingleOrDefault();
    if(AccountExists != null){
        // var user = (from a in DB.UsersandAccountInfos where a.AccNo == ACCNO select a).Single
        Console.WriteLine($"User {AccountExists.AccName} has a {AccountExists.AccType} account with a balance of {AccountExists.AccBalance} and an Active Status of {AccountExists.IsAccActive}");
        Console.WriteLine();
    }else{
        throw new Exception("The Account does not Exist");
    }
}

public void ResetPassword(int ACCNO){
    var AccountExists = (from a in DB.UsersandAccountInfos where a.AccNo == ACCNO select a).SingleOrDefault();
    if(AccountExists != null){
        AccountExists.UserPassword = "111"; //might come for a more complex default password later
            
        DB.UsersandAccountInfos.Update(AccountExists);
        DB.SaveChanges();
        Console.WriteLine("Password has been Reset");
        Console.WriteLine();
    }else{
        throw new Exception("The Account does not Exist");
    }
}

public void ApproveChequeBookRequest(int ACCNO, int RequestID){
    var ChequeBookRequestExists = (from a in DB.NewRequests where a.AccNo == ACCNO && a.RequestId == RequestID select a).SingleOrDefault();
    if(ChequeBookRequestExists != null){
        ChequeBookRequestExists.Pending = false;
        ChequeBookRequestExists.Approved = true;

        DB.NewRequests.Update(ChequeBookRequestExists);
        DB.SaveChanges();
        Console.WriteLine("Cheque book request approved");
        Console.WriteLine();
    }else{
        throw new Exception("Account or Request does not Exist");
    }
}


}