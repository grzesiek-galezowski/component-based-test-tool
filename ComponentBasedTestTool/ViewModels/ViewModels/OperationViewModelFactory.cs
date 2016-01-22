using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
  public interface OperationViewModelFactory
  {
    OperationViewModel CreateOperationViewModel(OperationEntry o);
  }


}
