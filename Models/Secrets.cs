using System;
using Microsoft.Extensions.Configuration;

namespace ShopifyBilling{
 public class Secrets: ISecrets{
  public Secrets(IConfiguration config){
   string find(string key){
    var value=config.GetValue<string>(key);
    if (string.IsNullOrWhiteSpace(value)){
     throw new NullReferenceException(key);
    }
    return value;
   }
   ShopifySecretKey=find("SHOPIFY_SECRET_KEY");
   ShopifyPublicKey=find("SHOPIFY_API_KEY");
  }
  public string ShopifySecretKey{get;}
  public string ShopifyPublicKey{get;}
 }
}