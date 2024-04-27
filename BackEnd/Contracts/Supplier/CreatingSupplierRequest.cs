using Supabase.Postgrest.Attributes;

namespace BackEnd.Contracts.Supplier;

public class CreatingSupplierRequest
{
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}