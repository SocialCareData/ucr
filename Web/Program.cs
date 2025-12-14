using AspNetStatic;
using Microsoft.AspNetCore.StaticFiles;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

var generating = args.Length == 2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddViewLocalization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc();

if (generating)
{
	var g = UcrGraph.Wrap(new Graph());
	FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

	builder.Services.AddSingleton<IStaticResourcesInfoProvider>(new StaticResourcesInfoProvider([
		new PageResource("/browser") { OutFile = "/browser/index.html" },
		.. g.Requirements.Select(x => new PageResource($"/requirement/{x.Number}")),
		new PageResource("/requirement-categories") { OutFile = "/requirement-categories/index.html" },
		.. g.RequirementCategories.Select(x => new PageResource($"/requirement-category/{x.Number}")),
		new PageResource("/requirements") { OutFile = "/requirements/index.html" },
		new PageResource("/table") { OutFile = "/table/index.html" },
		.. g.UseCases.Select(x => new PageResource($"/use-case/{x.Number}")),
		new PageResource("/use-cases") { OutFile = "/use-cases/index.html" },
		new PageResource("/"),

		new PageResource("/data/all.ttl"),
		new PageResource("/data/requirements.json"),
		new PageResource("/data/use-cases.json"),
		new PageResource("/data/links.json"),

		new PageResource("/script/browser.js"),
		new PageResource("/script/Graph.js"),
		new PageResource("/script/Requirement.js"),
		new PageResource("/script/RequirementList.js"),
		new PageResource("/script/table.js"),
		new PageResource("/script/UseCase.js"),
		new PageResource("/script/UseCaseLink.js"),
		new PageResource("/script/UseCaseList.js"),
		new PageResource("/script/Vocabulary.js"),

		new PageResource("/style/browser.css"),
		new PageResource("/style/site.css"),
		new PageResource("/style/table.css"),

		new BinResource("/Ucr.docx"),
		new BinResource("/Ucr.xlsx"),
	]));
}

var app = builder.Build();

app.MapRazorPages();

app.MapControllers();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".ttl"] = "text/turtle";

app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });

if (generating)
{
	var destinationRoot = args[0];
	var exitWhenDone = args[1] == "true";

	if (!Directory.Exists(destinationRoot))
	{
		Directory.CreateDirectory(destinationRoot);
	}

	app.GenerateStaticContent(destinationRoot, exitWhenDone: exitWhenDone, dontOptimizeContent: true);
}

app.Run();
