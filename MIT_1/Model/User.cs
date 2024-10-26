namespace MIT_1.Model;

public class User
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Description { get; set; }
    public bool Agree { get; set; }
    // public int? PlanId { get; set; }
    public string? Limit { get; set; }

    // public Plan? Plan { get; set; }
}