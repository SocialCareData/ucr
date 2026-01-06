using AspNetStatic;
using AspNetStatic.Optimizer;
using Microsoft.AspNetCore.StaticFiles;
using Model;

var generating = args.Length == 2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddViewLocalization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc();

if (generating)
{
	builder.Services.AddSingleton<IMarkupOptimizer, GithubPagesLinkRewriter>();

	builder.Services.AddSingleton<IStaticResourcesInfoProvider>(new StaticResourcesInfoProvider([
		new PageResource("/browser") { OutFile = "/browser/index.html" },
		.. UcrGraph.Instance.Requirements.Select(x => new PageResource($"/requirement/{x.Number}")),
		new PageResource("/requirement-categories") { OutFile = "/requirement-categories/index.html" },
		.. UcrGraph.Instance.RequirementCategories.Select(x => new PageResource($"/requirement-category/{x.Number}")),
		new PageResource("/requirements") { OutFile = "/requirements/index.html" },
		new PageResource("/table") { OutFile = "/table/index.html" },
		.. UcrGraph.Instance.UseCases.Select(x => new PageResource($"/use-case/{x.Number}")),
		new PageResource("/use-cases") { OutFile = "/use-cases/index.html" },
		new PageResource("/"),

		new BinResource("/data/all.ttl"),
		new BinResource("/data/requirements.json"),
		new BinResource("/data/use-cases.json"),
		new BinResource("/data/links.json"),

		new JsResource("/script/browser.js"),
		new JsResource("/script/Graph.js"),
		new JsResource("/script/Requirement.js"),
		new JsResource("/script/RequirementList.js"),
		new JsResource("/script/table.js"),
		new JsResource("/script/UseCase.js"),
		new JsResource("/script/UseCaseLink.js"),
		new JsResource("/script/UseCaseList.js"),
		new JsResource("/script/Vocabulary.js"),

		new CssResource("/style/browser.css"),
		new CssResource("/style/site.css"),
		new CssResource("/style/table.css"),

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

	app.GenerateStaticContent(destinationRoot, exitWhenDone: exitWhenDone);
}

app.Run();

internal class GithubPagesLinkRewriter : IMarkupOptimizer
{
	MarkupOptimizerResult IMarkupOptimizer.Execute(string content, PageResource resource, string outFilePathname) => new(content
		.Replace("href=\"/", "href=\"/ucr/")
		.Replace("src=\"/", "src=\"/ucr/")
	);
}