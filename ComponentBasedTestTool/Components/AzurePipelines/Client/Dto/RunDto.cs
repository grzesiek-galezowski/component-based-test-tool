using System;

namespace Components.AzurePipelines.Client.Dto;

public record RunDto
(
  ReferenceLinksDto Links,
  PipelineDto Pipeline,
  string State,
  string Result,
  DateTime CreatedDate,
  string Url,
  ResourcesDto Resources,
  string FinalYaml,
  int Id,
  object Name
);