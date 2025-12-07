using VDS.RDF;
using VDS.RDF.Wrapping;

namespace MakeWord.Wrapping;

internal class UseCase : GraphWrapperNode
{
    protected UseCase(INode node, IGraph graph) : base(node, graph) { }

    internal static UseCase Wrap(INode node, IGraph graph) => new(node, graph);

    internal static UseCase Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

	internal string Number { get => (this as IUriNode).Uri.ToString().Split("/").Last(); }
	internal string Title { get => this.Singular(Vocabulary.UseCaseTitle, ValueMappings.As<string>); }
	internal string Description { get => this.Singular(Vocabulary.UseCaseDescription, ValueMappings.As<string>); }

	internal ISet<Link> Links { get => this.Subjects(Vocabulary.Target, Link.Wrap, Link.Wrap); }
}
