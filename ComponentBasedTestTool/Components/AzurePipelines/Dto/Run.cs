using System;

namespace Components.AzurePipelines.Dto;

public record Run
(
  ReferenceLinks Links,
  Pipeline Pipeline,
  string State,
  DateTime CreatedDate,
  string Url,
  Resources Resources,
  string FinalYaml,
  int Id,
  object Name
);