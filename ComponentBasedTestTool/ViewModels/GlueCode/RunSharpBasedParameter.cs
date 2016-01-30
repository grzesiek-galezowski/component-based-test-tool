﻿using System;
using ExtensionPoints;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.GlueCode
{
  public class RunSharpBasedParameter<T> : OperationParameter<T>
  {
    private readonly PropertyValueSource<T> _prop;

    public RunSharpBasedParameter(PropertyValueSource<T> prop)
    {
      _prop = prop;
    }

    public T Value => _prop.Value;
    public void StoreIn(PersistentStorage persistentStorage)
    {
      persistentStorage.StoreValue(_prop.Name, Value.ToString());
    }
  }
}