using VDS.RDF;

namespace Model;

internal class Vocabulary
{
    internal const string VocabularyBaseUri = "https://socialcaredata.github.io/ucr/schema/";

    private static readonly NodeFactory Factory = new();

    internal static IUriNode Title { get; } = Node("title");
    internal static IUriNode Description { get; } = Node("description");
    internal static IUriNode UseCase { get; } = Node("useCase");
    internal static IUriNode Category { get; } = Node("category");
    internal static IUriNode Target { get; } = Node("target");
    internal static IUriNode Comment { get; } = Node("comment");
    internal static IUriNode UseCaseTitle { get; } = Node("useCaseTitle");
    internal static IUriNode UseCaseDescription { get; } = Node("useCaseDescription");
    internal static IUriNode UseCaseNarrativeDocument { get; } = Node("useCaseNarrativeDocument");
    internal static IUriNode CategoryTitle { get; } = Node("categoryTitle");
    internal static IUriNode CategoryDescription { get; } = Node("categoryDescription");

    private static IUriNode Node(string name) => AnyNode($"{VocabularyBaseUri}{name}");

    private static IUriNode AnyNode(string uri) => Factory.CreateUriNode(Factory.UriFactory.Create(uri));
}
