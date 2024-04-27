using Supabase.Gotrue;

namespace BackApi.SupaBaseContext;

public class SupaBaseConnection
{
    public string SupaBaseUrl { get; set; }
    public string SupaBaseKey { get; set; }
    public Session Session { get; set;}

}