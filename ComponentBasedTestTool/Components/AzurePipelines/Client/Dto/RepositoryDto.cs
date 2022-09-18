namespace Components.AzurePipelines.Client.Dto;

public record RepositoryDto(string FullName, ConnectionDto Connection, string Type);