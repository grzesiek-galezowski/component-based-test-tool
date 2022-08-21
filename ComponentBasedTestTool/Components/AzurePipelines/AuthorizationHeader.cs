using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.AzurePipelines
{
  internal class AuthorizationHeader
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
