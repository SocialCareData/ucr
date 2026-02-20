using Microsoft.AspNetCore.Mvc;
using Model;

namespace Web.Controllers;

[Route("data")]
public class RequirementsController
{
    [HttpGet("requirements.json")]
    public IEnumerable<Requirement> GetRequirements() =>
        UcrGraph.Instance.Requirements.Select(x => new Requirement(x.Id, x.Title, x.Description));

    [HttpGet("use-cases.json")]
    public IEnumerable<UseCase> GetUseCases() =>
        UcrGraph.Instance.UseCases.Select(x => new UseCase(x.Id, x.Title, x.Description, x.NarrativeDocument));

    [HttpGet("links.json")]
    public IEnumerable<Link> GetLinks() =>
        UcrGraph.Instance.Links.Select(x => new Link(x.Requirement.Id, x.Target.Id, x.Comment));

    [HttpGet("core-standards.json")]
    public IEnumerable<CoreStandard> GetCoreStandards() =>
        UcrGraph.Instance.CoreStandards.Select(x => new CoreStandard(x.Id, x.Title, x.Description));

    public record Requirement(string Id, string Title, string Description);

    public record UseCase(string Id, string Title, string Description, Uri NarrativeDocument);

    public record Link(string Requirement, string UseCase, string Comment);

    public record CoreStandard(string Id, string Title, string Description);
}
