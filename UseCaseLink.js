import { Vocabulary } from "./Vocabulary.js"
import { UseCase } from "./UseCase.js"
import { Requirement } from "./Requirement.js"

export class UseCaseLink {
	constructor(node, graph) {
		this.node = node
		this.graph = graph
	}

	get requirement() {
		return [...this.graph.match(undefined, Vocabulary.useCase, this.node)]
			.map(x => x.subject)
			.map(x => new Requirement(x, this.graph))
		[0]
	}

	get target() {
		return [...this.graph.match(this.node, Vocabulary.target)]
			.map(x => x.object)
			.map(x => new UseCase(x, this.graph))
		[0]
	}

	get comment() {
		return [...this.graph.match(this.node, Vocabulary.comment)]
			.map(x => x.object)
			.map(x => x.value)
		[0]
	}
}
