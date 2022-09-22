using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context;

public class PharmacyManagerContext : DbContext
{
    public PharmacyManagerContext(DbContextOptions<PharmacyManagerContext> options) : base(options) { }
    public PharmacyManagerContext() : base() { }

}