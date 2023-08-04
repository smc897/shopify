using ShopifyBilling;

public class Session{
 public Session(UserAccount user){
  UserId=user.Id;
  ShopifyChargeId=user.ShopifyChargeId;
 }

 public Session(){}

 public int UserId{get; set;}
public long? ShopifyChargeId{get; set;}
}