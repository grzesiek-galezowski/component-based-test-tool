using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionPoints
{
  public interface OperationsOutput
  {
    void Write(string str);
  }
}
