namespace Dataweb.Dilab.Web.Models
{
    public interface IHasTenant
    {
        Tenant Tenant { get; set; }
    }
}