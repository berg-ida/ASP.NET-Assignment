namespace Data.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }
    public Client Client { get; set; } = null!;
    public Status Status { get; set; } = null!;
}
