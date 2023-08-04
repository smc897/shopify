using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using ShopifyBilling.models;

namespace ShopifyBilling.Data{
  public class DataContext: DbContext{
  public DataContext(){}
public DataContext(DbContextOptions<DataContext> options): base(options){}
public DbSet<UserAccount>? Users{get; set;}
public DbSet<OAuthState>? LoginStates{get; set;}
  }
 }