public class Lease
{
    private int leaseID;
    private int vehicleID;
    private int customerID;
    private DateTime startDate;
    private DateTime endDate;
    private string type;

    public Lease() { }

    public Lease(int leaseID, int vehicleID, int customerID, DateTime startDate, DateTime endDate, string type)
    {
        this.leaseID = leaseID;
        this.vehicleID = vehicleID;
        this.customerID = customerID;
        this.startDate = startDate;
        this.endDate = endDate;
        this.type = type;
    }

    public int LeaseID { get => leaseID; set => leaseID = value; }
    public int VehicleID { get => vehicleID; set => vehicleID = value; }
    public int CustomerID { get => customerID; set => customerID = value; }
    public DateTime StartDate { get => startDate; set => startDate = value; }
    public DateTime EndDate { get => endDate; set => endDate = value; }
    public string Type { get => type; set => type = value; }
}