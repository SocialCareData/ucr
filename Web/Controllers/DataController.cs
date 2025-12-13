using Microsoft.AspNetCore.Mvc;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace WebApplication1.Controllers;

[Route("data")]
public class RequirementsController
{
    [HttpGet("requirements.json")]
    public IEnumerable<Requirement> GetRequirements()
    {
        var g = Model.UcrGraph.Wrap(new Graph());
        FileLoader.Load(g, "./wwwroot/data/all.ttl");

        return g.Requirements.Select(x => new Requirement(x.Id, x.Title, x.Description));
    }

    [HttpGet("use-cases.json")]
    public IEnumerable<UseCase> GetUseCases()
    {
        var g = Model.UcrGraph.Wrap(new Graph());
        FileLoader.Load(g, "./wwwroot/data/all.ttl");

        return g.UseCases.Select(x => new UseCase(x.Id, x.Title, x.Description, x.NarrativeDocument));
    }

    [HttpGet("links.json")]
    public IEnumerable<Link> GetLinks()
    {
        var g = Model.UcrGraph.Wrap(new Graph());
        FileLoader.Load(g, "./wwwroot/data/all.ttl");

        return g.Links.Select(x => new Link(x.Requirement.Id, x.Target.Id, x.Comment));
    }

    public record Requirement(string Id, string Title, string Description);

    public record UseCase(string Id, string Title, string Description, Uri NarrativeDocument);

    public record Link(string Requirement, string UseCase, string Comment);
}
