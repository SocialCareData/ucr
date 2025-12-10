using System.Text.Json;
using VDS.RDF;
using VDS.RDF.Parsing;

var source = args[0];
var target = args[1];

if (!Directory.Exists(target))
{
	Directory.CreateDirectory(target);
}

var g = Model.UcrGraph.Wrap(new Graph());
FileLoader.Load(g, source);

var options = new JsonSerializerOptions { WriteIndented = true };

var requirementsFile = File.OpenWrite(Path.Combine(target, "requirements.json"));
await JsonSerializer.SerializeAsync(
	requirementsFile,
	g.Requirements.Select(x => new { id = x.Id, title = x.Title, description = x.Description }),
	options);

using var useCasesFile = File.OpenWrite(Path.Combine(target, "useCases.json"));
await JsonSerializer.SerializeAsync(
	useCasesFile,
	g.UseCases.Select(x => new { id = x.Id, title = x.Title, description = x.Description, narrativeDocument = x.NarrativeDocument }),
	options);

using var linksFile = File.OpenWrite(Path.Combine(target, "links.json"));
await JsonSerializer.SerializeAsync(
	linksFile,
	g.Links.Select(x => new { requirement = x.Requirement.Id, useCase = x.Target.Id, comment = x.Comment }),
	options);
