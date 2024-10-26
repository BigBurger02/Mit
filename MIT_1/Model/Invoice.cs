namespace MIT_1.Model;

public class Invoice
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Limit { get; set; }
    public int InTraffic { get; set; }
    public int OutTraffic { get; set; }
    public int Cost { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public User? User { get; set; }
}