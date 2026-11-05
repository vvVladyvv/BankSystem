
namespace BankSystem.Accounts;
public class BankAccount
{
    public int Id { get; set; } = 0;
    public string? Username { get; set; } = "";
    public string? Email { get; set; } = "";
    public string? Password { get; set; } = "";
    public decimal Balance { get; set; } = 0;

    public static List<BankAccount> users = new List<BankAccount>();
}
    
