namespace Components.AzurePipelines.Client.Dto;

public record ListOfPipelinesDto(int Count, PipelineDto[] Value);