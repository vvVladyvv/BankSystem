using BankSystem.Accounts;
using BankSystem.Services;

BankAccount BankOperations = new BankAccount();
BankServices bankServices = new BankServices();
int options = 0;
int id = 0;

while (true)
{
    Console.WriteLine("Welcome to the BankSystem!\n\nMenu\n------------------------\n1. Create Account\n2. Login\n3. View Accounts\n4. Edit user\n5. Delete user\n6. Exit\n\nSelect an option: ");
    bool success = int.TryParse(Console.ReadLine()!, out options);
    if (!success)
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    if (options < 1 || options > 6)
    {
        Console.WriteLine("Invalid option. Please select a valid option.");
        continue;
    }
    else if (options == 1)
    {
        Console.WriteLine("Enter your username: ");
        string? username = Console.ReadLine();
        Console.WriteLine("Enter your email: ");
        string? email = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        string? password = Console.ReadLine();

        BankAccount Account = new BankAccount();
        id++;
        Account.Id = id;
        Account.Username = username;
        Account.Email = email;
        Account.Password = password;

        bool accountCreated = bankServices.RegisterAccount(Account);
        if (accountCreated)
        {
            Console.WriteLine("Account registered successfully!");
        }
        else
        {
            Console.WriteLine("Failed to register account.");
        }
    }
    else if (options == 2)
    {
        Console.WriteLine("Enter your Email: ");
        string? email = Console.ReadLine();
        Console.WriteLine("Enter your Password: ");
        string? password = Console.ReadLine();

        BankAccount Account = new BankAccount();
        Account.Email = email;
        Account.Password = password;

        bool loginSuccess = bankServices.VerifyAccount(Account);

        if (loginSuccess)
        {
            BankAccount? user = BankAccount.users.FirstOrDefault(u => u.Email == Account.Email && u.Password == Account.Password);
            int bank_options = 0;
            bool bank_status = true;
            while (bank_status)
            {
                Console.WriteLine($"Welcome, {user!.Username}!");
                Console.WriteLine("Welcome to the Bank System!\n\nMenu\n------------------------\n1. Deposit\n2. Withdraw\n\n3. Transfer\n4. Check Balance\n5. Logout\n\nSelect an option: ");
                bool bank_options_success = int.TryParse(Console.ReadLine()!, out bank_options);
                if (!bank_options_success)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }
                if (bank_options < 1 || bank_options > 5)
                {
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    continue;
                }
                else if (bank_options == 1)
                {
                    Console.WriteLine("Enter the amount to deposit: ");
                    bool deposit_success = decimal.TryParse(Console.ReadLine()!, out decimal depositAmount);
                    if (!deposit_success)
                    {
                        Console.WriteLine("Invalid deposit amount.");
                    }
                    else if (depositAmount == 0 || depositAmount < 0)
                    {
                        Console.WriteLine("Invalid deposit amount.");
                    }
                    else
                    {
                        user.Balance += depositAmount;
                        Console.WriteLine($"Deposited {depositAmount}. New balance: {user.Balance}");
                    }
                }
                else if (bank_options == 2)
                {
                    Console.WriteLine("Enter the amount to withdraw: ");
                    bool withdraw_success = decimal.TryParse(Console.ReadLine()!, out decimal withdrawAmount);
                    if (!withdraw_success)
                    {
                        Console.WriteLine("Invalid withdrawal amount.");
                    }
                    else if (withdrawAmount == 0 || withdrawAmount < 0)
                    {
                        Console.WriteLine("Invalid withdrawal amount.");
                    }
                    else if (withdrawAmount <= user.Balance)
                    {
                        user.Balance -= withdrawAmount;
                        Console.WriteLine($"Withdrew {withdrawAmount}. New balance: {user.Balance}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds.");
                    }
                }
                else if (bank_options == 3)
                {
                    Console.WriteLine("Enter the email of the user to transfer to: ");
                    string? transferEmail = Console.ReadLine();
                    BankAccount? transferUser = BankAccount.users.FirstOrDefault(u => u.Email == transferEmail);
                    if (transferUser != null)
                    {
                        Console.WriteLine("Enter the amount to transfer: ");
                        bool transfer_success = decimal.TryParse(Console.ReadLine()!, out decimal transferAmount);
                        if (!transfer_success)
                        {
                            Console.WriteLine("Invalid transfer amount.");
                        }
                        else if (transferAmount == 0 || transferAmount < 0)
                        {
                            Console.WriteLine("Invalid transfer amount.");
                        }
                        else if (transferUser.Email == user.Email)
                        {
                            Console.WriteLine("You cannot transfer to yourself.");
                        }
                        else if (transferAmount <= user.Balance)
                        {
                            user.Balance -= transferAmount;
                            transferUser.Balance += transferAmount;
                            Console.WriteLine($"Transferred {transferAmount} to {transferUser.Username}. New balance: {user.Balance}");
                        }
                        else
                        {
                            Console.WriteLine("Insufficient funds.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }
                }
                else if (bank_options == 4)
                {
                    Console.WriteLine($"Current balance: {user.Balance}");
                }
                else if (bank_options == 5)
                {
                    bank_status = false;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid email or password.");
            continue;
        }
    }
    else if (options == 3)
    {
       Console.WriteLine("List of users:");
        List<BankAccount> users = bankServices.GetUsers();
        foreach (BankAccount user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}, Email: {user.Email}, Balance: {user.Balance}");
        }

    }
    else if (options == 4)
    {
        Console.WriteLine("Into userID for edit: ");
        bool parseSuccess = int.TryParse(Console.ReadLine(), out int userID);
        if (!parseSuccess)
        {
            Console.WriteLine("Invalid user ID. Please enter a valid number.");
            continue;
        }

        string? response = bankServices.EditUser(userID);

        if (response != null){
            Console.WriteLine(response);
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }
    else if (options == 5)
    {
        Console.WriteLine("Into userID for delete: ");
        bool parseSuccess = int.TryParse(Console.ReadLine(), out int userID);
        if (!parseSuccess)
        {
            Console.WriteLine("Invalid user ID. Please enter a valid number.");
            continue;
        }
        string? response = bankServices.DeleteUser(userID);

        if (response != null){
            Console.WriteLine(response);
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }
    else if (options == 6)
    {
        break;
    }
}

