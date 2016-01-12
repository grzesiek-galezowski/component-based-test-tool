using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class OperationCommandFactory
  {
    private readonly ApplicationContext _applicationContext;

    public OperationCommandFactory(ApplicationContext applicationContext)
    {
      _applicationContext = applicationContext;
    }

    public RunOperationCommand CreateRunCommand(OperationViewModel operationViewModel)
    {
      return new RunOperationCommand(operationViewModel, _applicationContext);
    }

    public CancelOperationCommand CreateCancelCommand()
    {
      return new CancelOperationCommand(_applicationContext);
    }
  }
}