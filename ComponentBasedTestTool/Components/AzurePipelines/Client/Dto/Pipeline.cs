namespace Components.AzurePipelines.Client.Dto;

public record Pipeline(
  ReferenceLinks Links,
  string Url,
  int Id,
  int Revision,
  string Name,
  string Folder
);