bool on = true;
List<BankSystem> users = new List<BankSystem>();
int options = 0;
int id = 0;


while (on)
{
    try
    {

        Console.WriteLine("Welcome to the Bank System!\n\nMenu\n------------------------\n1. Create Account\n2. Login\n3. View Accounts\n4. Edit user\n5. Delete user\n6. Exit\n\nSelect an option: ");
        options = int.Parse(Console.ReadLine()!);
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid input. Please enter a number." + ex.Message);
    };
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

        BankSystem bankSystem = new BankSystem();
        id++;
        bankSystem.Id = id;
        bankSystem.username = username;
        bankSystem.email = email;
        bankSystem.password = password;
        users.Add(bankSystem);
        Console.WriteLine("Account created successfully!");
    }
    else if (options == 2)
    {
        Console.WriteLine("Enter your email: ");
        string? email = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        string? password = Console.ReadLine();

        BankSystem? user = users.FirstOrDefault(u => u.email == email && u.password == password);
        
        if (user != null)
        {
            bool bank_status = true;
            while (bank_status)
            {
                Console.WriteLine("Welcome to the Bank System!\n\nMenu\n------------------------\n1. Deposit\n2. Withdraw\n\n3. Transfer\n4. Check Balance\n5. Logout\n\nSelect an option: ");
                int bank_options = int.Parse(Console.ReadLine()!);
                if (bank_options == 1)
                {
                    Console.WriteLine("Enter the amount to deposit: ");
                    decimal depositAmount = decimal.Parse(Console.ReadLine()!);
                    user.balance += depositAmount;
                    Console.WriteLine($"Deposited {depositAmount}. New balance: {user.balance}");
                }
                else if (bank_options == 2)
                {
                    Console.WriteLine("Enter the amount to withdraw: ");
                    decimal withdrawAmount = decimal.Parse(Console.ReadLine()!);
                    if (withdrawAmount <= user.balance)
                    {
                        user.balance -= withdrawAmount;
                        Console.WriteLine($"Withdrew {withdrawAmount}. New balance: {user.balance}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds.");
                    }
                }
                else if(bank_options == 3)
                {
                    Console.WriteLine("Enter the email of the user to transfer to: ");
                    string? transferEmail = Console.ReadLine();
                    BankSystem? transferUser = users.FirstOrDefault(u => u.email == transferEmail);
                    if (transferUser != null)
                    {
                        Console.WriteLine("Enter the amount to transfer: ");
                        decimal transferAmount = decimal.Parse(Console.ReadLine()!);
                        if (transferAmount <= user.balance)
                        {
                            user.balance -= transferAmount;
                            transferUser.balance += transferAmount;
                            Console.WriteLine($"Transferred {transferAmount} to {transferUser.username}. New balance: {user.balance}");
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
                    Console.WriteLine($"Current balance: {user.balance}");
                }
                else if (bank_options == 5)
                {
                    bank_status = false;
                }
            }
            Console.WriteLine($"Welcome, {user.username}!");
        }
        else
        {
            Console.WriteLine("Invalid email or password.");
        }
    }
    else if (options == 3)
    {
        foreach (BankSystem user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.username}, Email: {user.email}");
        }
    }
    else if (options == 4)
    {
        Console.WriteLine("Enter the ID of the user you want to edit: ");
        int editId = int.Parse(Console.ReadLine()!);
        BankSystem? userToEdit = users.FirstOrDefault(u => u.Id == editId);

        if (userToEdit != null)
        {
            Console.WriteLine("Enter new username: ");
            userToEdit.username = Console.ReadLine();
            Console.WriteLine("Enter new email: ");
            userToEdit.email = Console.ReadLine();
            Console.WriteLine("Enter new password: ");
            userToEdit.password = Console.ReadLine();
            Console.WriteLine("User updated successfully!");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }
    else if (options == 5)
    {
        Console.WriteLine("Enter the ID of the user you want to delete: ");
        int deleteId = int.Parse(Console.ReadLine()!);
        BankSystem? userToDelete = users.FirstOrDefault(u => u.Id == deleteId);

        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            Console.WriteLine("User deleted successfully!");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }
    else if (options == 6)
    {
        on = false;
    }
}

public class BankSystem
{
    public int Id { get; set; }
    public string? username { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
   public decimal? balance { get; set; }
}
