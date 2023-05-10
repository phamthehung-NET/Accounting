using Microsoft.AspNetCore.Identity;

namespace Accounting.Model
{
    public class CustomUser : IdentityUser
    {
        public DateTime UpdatedPriceDate { get; set; }
    }
}
