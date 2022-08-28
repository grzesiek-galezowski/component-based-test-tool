using System;
using System.IO;
using System.Text;

namespace Components.AzurePipelines.Client
{
  internal static class AuthorizationHeader
  {
    //bug move closer to data
    public static string Value(string filePath)
    {
      var personalAccessToken = File.ReadAllText(filePath);
      var base64String = Convert.ToBase64String(
        Encoding.ASCII.GetBytes(
          $":{personalAccessToken}"));
      return $"Basic {base64String}";
    }
  }
}
