namespace Components.AzurePipelines.Client.Dto;

public record PipelineDto(
  ReferenceLinksDto Links,
  string Url,
  int Id,
  int Revision,
  string Name,
  string Folder
);