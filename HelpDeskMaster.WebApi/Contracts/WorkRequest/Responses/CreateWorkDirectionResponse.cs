﻿namespace HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses
{
    public class CreateWorkDirectionResponse
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public required string Title { get; set; }
    }
}