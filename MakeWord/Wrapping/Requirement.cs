using VDS.RDF;
using VDS.RDF.Wrapping;

namespace MakeWord.Wrapping;

internal class Requirement : GraphWrapperNode
{
    protected Requirement(INode node, IGraph graph) : base(node, graph) { }

    internal static Requirement Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Requirement Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal string Number { get => (this as IUriNode).Uri.ToString().Split("/").Last(); }
    internal string Title { get => this.Singular(Vocabulary.Title, ValueMappings.As<string>); }
    internal string Description { get => this.Singular(Vocabulary.Description, ValueMappings.As<string>); }

    internal ISet<Link> Links { get => this.Objects(Vocabulary.UseCase, Link.Wrap, Link.Wrap); }
}
