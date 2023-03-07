namespace ProjetCSharp.Models.User;

class User
{
    public int ID;
    public string Firstname;
    public string LastName;
    public int Old;
    public int Salary;
    public double Tax;

    public User(int id, string firstname, string lastName, int old, int salary, double tax)
    {
        this.ID = id;
        this.Firstname = firstname;
        this.LastName = lastName;
        this.Old = old;
        this.Salary = salary;
        this.Tax = tax;
    }
}