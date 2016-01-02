﻿using System;
using System.Threading.Tasks;
using ComponentBasedTestTool.ViewModels.OperationStates;
using ExtensionPoints;

namespace Components
{
  public class CdOperation : Operation
  {
    private readonly OperationsOutput _out;
    private OperationParameter<string> _path;

    public CdOperation(OperationsOutput @out)
    {
      _out = @out;
    }

    public async Task RunAsync()
    {
      _out.WriteLine("cd " + _path.Value);
    }

    public void FillParameters(OperationParametersListBuilder parameters)
    {
      _path = parameters.Path("Path", @"C:\");
    }
  }
}