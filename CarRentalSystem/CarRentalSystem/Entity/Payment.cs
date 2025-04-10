public class Payment
{
    private int paymentID;
    private int leaseID;
    private DateTime paymentDate;
    private double amount;

    public Payment() { }

    public Payment(int paymentID, int leaseID, DateTime paymentDate, double amount)
    {
        this.paymentID = paymentID;
        this.leaseID = leaseID;
        this.paymentDate = paymentDate;
        this.amount = amount;
    }

    public int PaymentID { get => paymentID; set => paymentID = value; }
    public int LeaseID { get => leaseID; set => leaseID = value; }
    public DateTime PaymentDate { get => paymentDate; set => paymentDate = value; }
    public double Amount { get => amount; set => amount = value; }
}
