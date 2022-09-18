namespace Components.AzurePipelines.Client.Dto;

public record ReferenceLinksDto(
  SelfDto Self,
  WebDto Web,
  WebDto Pipelineweb,
  WebDto Pipeline
);