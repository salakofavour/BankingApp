// // See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

AdminOperations AO = new AdminOperations();
UsersOperation UO = new UsersOperation();
WelcomePage firstPage = new WelcomePage();

firstPage.DisplayWelcome();

int UserType = 0;

try{
while(UserType !=3){

    UserType  = Convert.ToInt32(Console.ReadLine());
    switch(UserType){

        case 1: //cx
            Console.WriteLine("Enter your Username");
            string UserN = Console.ReadLine();
            Console.WriteLine("Enter your Password");
            string PassW = Console.ReadLine();
            
            bool userExists = UO.isUser(UserN, PassW);

            if(userExists){
                int UserAction = 0;
                while(UserAction != 8){
                    UO.DisplayUserMenu();

                    UserAction  = Convert.ToInt32(Console.ReadLine());
                    switch(UserAction){
                        case 1:
                            UO.CheckAccountDetails();
                            break;
                        case 2:
                            Console.WriteLine("Enter the amount you would like to withdraw");
                            double withdrawAmount= Convert.ToDouble(Console.ReadLine());
                            UO.WithDraw(withdrawAmount);
                            break;
                        case 3:
                            Console.WriteLine("Enter the amount you would like to Deposit");
                            double depositAmount= Convert.ToDouble(Console.ReadLine());
                            UO.Deposit(depositAmount);
                            break;
                        case 4:
                            Console.WriteLine("Enter the account number you would like to Transfer to");
                            int toAccount= Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter the amount you would like to Transfer");
                            double transferAmount= Convert.ToDouble(Console.ReadLine());
                            UO.Transfer(transferAmount, toAccount);
                            break;
                        case 5:
                            UO.Last5Transactions();
                            break;
                        case 6:
                            Console.WriteLine("Describe your request in 190 characters");
                            string requestDesc = Console.ReadLine();
                            UO.RequestChequeBook(requestDesc);
                            break;
                        case 7:
                            Console.WriteLine("Enter your new Password");
                            string newPass = Console.ReadLine();
                            UO.ChangePassword(newPass);
                            break;
                        case 8://exit this Menu
                            break;
                        default:
                            Console.WriteLine("Invalid Input, please choose from the provided options.");
                            break;
                    }
                }

            }else{
                Console.WriteLine("The provided credentials are invalid");
            }

            break;
        case 2://admin
            Console.WriteLine("Enter your Username");
            string AdminN = Console.ReadLine();
            Console.WriteLine("Enter your Password");
            string AdminPassW = Console.ReadLine();
            
            bool adminExists = AO.isAdmin(AdminN, AdminPassW);

            if(adminExists){

                int adminAction = 0;
                while(adminAction != 7){
                    AO.AdminMenuOptions();

                    adminAction = Convert.ToInt32(Console.ReadLine());

                    switch(adminAction){
                        case 1:
                            Console.WriteLine("Enter a Username");
                            string UserN1 = Console.ReadLine();
                            Console.WriteLine("Enter a Password");
                            string PassW1 = Console.ReadLine();
                            Console.WriteLine("Enter an AccountNo");
                            int AccNo1 = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the Account holder's Name ");
                            string AccName1 = Console.ReadLine();
                            Console.WriteLine("Enter the Account type");
                            string AccType1 = Console.ReadLine();
                            Console.WriteLine("Enter the user's available Balance");
                            double AccBalance1 = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Account Status (true/false)");
                            bool AccActive1 = Convert.ToBoolean(Console.ReadLine());

                            AO.CreateNewUser(UserN1, PassW1, AccNo1, AccName1, AccType1, AccBalance1, AccActive1);

                            break;
                        case 2:
                            Console.WriteLine("Enter an AccountNo");
                            int AccNo2 = Convert.ToInt32(Console.ReadLine());

                            AO.DeleteUser(AccNo2);
                            break;
                        case 3:
                            Console.WriteLine("Enter an AccountNo");
                            int AccNo3 = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the Account holder's Name ");
                            string AccName3 = Console.ReadLine();
                            Console.WriteLine("Enter the Account type");
                            string AccType3 = Console.ReadLine();
                            Console.WriteLine("Enter the user's available Balance");
                            double AccBalance3 = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Account Status (true/false)");
                            bool AccActive3 = Convert.ToBoolean(Console.ReadLine());

                            AO.EditAccountDetails(AccNo3, AccName3, AccType3, AccBalance3, AccActive3);
                            break;
                        case 4:
                            Console.WriteLine("Enter an AccountNo");
                            int AccNo4 = Convert.ToInt32(Console.ReadLine());
                            
                            AO.DisplaySummary(AccNo4);
                            break;
                        case 5:
                            Console.WriteLine("Enter an AccountNo");
                            int AccNo5 = Convert.ToInt32(Console.ReadLine());

                            AO.ResetPassword(AccNo5);
                            break;
                        case 6:
                            Console.WriteLine("Enter an AccountNo");
                            int AccNo6 = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter a Request Id");
                            int ReqId6 = Convert.ToInt32(Console.ReadLine());

                            AO.ApproveChequeBookRequest(AccNo6, ReqId6);
                            break;
                        case 7:
                            break;
                        default:
                            Console.WriteLine("Invalid input, please choose from the provided options.");
                            break;
                    }
                }
            }else{
                Console.WriteLine("The provided credentials are invalid");
            }

            break;
        case 3://exit
            Console.WriteLine("Thank you for using our App");
            break;
        default:
            Console.WriteLine("Invalid Input, please choose from the provided options.");
            break;
}
}
}catch(Exception e){
    Console.WriteLine(e.Message);
}