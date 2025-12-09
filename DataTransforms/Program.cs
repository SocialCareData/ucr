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

await JsonSerializer.SerializeAsync(
	File.OpenWrite(Path.Combine(target, "requirements.json")),
	g.Requirements.Select(x => new { id = x.Id, title = x.Title, description = x.Description }),
	options);

await JsonSerializer.SerializeAsync(
	File.OpenWrite(Path.Combine(target, "useCases.json")),
	g.UseCases.Select(x => new { id = x.Id, title = x.Title, description = x.Description, narrativeDocument = x.NarrativeDocument }),
	options);

await JsonSerializer.SerializeAsync(
	File.OpenWrite(Path.Combine(target, "links.json")),
	g.Links.Select(x => new { requirement = x.Requirement.Id, useCase = x.Target.Id, comment = x.Comment }),
	options);
