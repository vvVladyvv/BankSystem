
using BankSystem.Accounts;


namespace BankSystem.Services;
public class BankServices{
    public bool RegisterAccount(BankAccount Account)
        {
            if (Account == null)
            {
                throw new ArgumentNullException(nameof(Account), "Account cannot be null.");
            }

            if (BankAccount.users.Any(u => u.Email == Account.Email))
            {
                Console.WriteLine("Email already exists. Please try again.");
                return false;
            }
            else
            {
                BankAccount.users.Add(Account);
                return true;
            }
        }

        public bool VerifyAccount(BankAccount Account)
        {
            if (Account == null)
            {
                throw new ArgumentNullException(nameof(Account), "Account cannot be null.");
            }

            if (BankAccount.users.Any(u => u.Email == Account.Email && u.Password == Account.Password))
            {
                Console.WriteLine("Account verified successfully!");
                return true;
            }
            else
            {
                return false;
            }
        }

        public BankAccount? GetUser(int user_ID)
        {
            foreach (BankAccount user in BankAccount.users)
            {
                if (user.Id == user_ID)
                {
                    return user;
                }
            }

            return null;
        }

        public List<BankAccount> GetUsers()
        {
            return BankAccount.users;
        }

        public string? EditUser(int userID)
        {
            BankAccount? user = BankAccount.users.FirstOrDefault(u => u.Id == userID);

            if (user == null)
            {
                return null;
            }

            Console.WriteLine("Enter new username: ");
            user.Username = Console.ReadLine();
            Console.WriteLine("Enter new email: ");
            user.Email = Console.ReadLine();
            Console.WriteLine("Enter new password: ");
            user.Password = Console.ReadLine();

            return "User updated successfully!";
        }

        public string? DeleteUser(int userID)
        {
            BankAccount? user = BankAccount.users.FirstOrDefault(u => u.Id == userID);

            if (user == null)
            {
                return null;
            }

            BankAccount.users.Remove(user);
            return "User deleted successfully!";
        }

}