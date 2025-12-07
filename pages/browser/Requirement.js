import { Vocabulary } from "./Vocabulary.js"
import { UseCaseLink } from "./UseCaseLink.js"

export class Requirement {
	constructor(node, graph) {
		this.node = node
		this.graph = graph
	}

	get id() {
		return this.node.id
	}

	get number() {
		return this.id.split("/").pop()
	}

	get title() {
		return [...this.graph.match(this.node, Vocabulary.title)]
			.map(x => x.object)
			.map(x => x.value)
		[0]
	}

	get description() {
		return [...this.graph.match(this.node, Vocabulary.description)]
			.map(x => x.object)
			.map(x => x.value)
		[0]
	}

	get useCases() {
		return [...this.graph.match(this.node, Vocabulary.useCase)]
			.map(x => x.object)
			.map(x => new UseCaseLink(x, this.graph))
	}
}
