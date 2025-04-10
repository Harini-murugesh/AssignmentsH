public class Customer
{
    private int customerID;
    private string firstName;
    private string lastName;
    private string email;
    private string phoneNumber;

    public Customer() { }

    public Customer(int customerID, string firstName, string lastName, string email, string phoneNumber)
    {
        this.customerID = customerID;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
    }

    public int CustomerID { get => customerID; set => customerID = value; }
    public string FirstName { get => firstName; set => firstName = value; }
    public string LastName { get => lastName; set => lastName = value; }
    public string Email { get => email; set => email = value; }
    public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
}
