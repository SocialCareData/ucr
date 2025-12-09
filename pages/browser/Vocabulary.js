export class Vocabulary {
	static get title() { return this.property("title") }
	static get description() { return this.property("description") }
	static get useCase() { return this.property("useCase") }
	static get target() { return this.property("target") }
	static get useCaseTitle() { return this.property("useCaseTitle") }
	static get useCaseDescription() { return this.property("useCaseDescription") }
	static get comment() { return this.property("comment") }
	static get narrativeDocument() { return this.property("useCaseNarrativeDocument") }

	static property(name) {
		return N3.DataFactory.namedNode(`https://github.com/SocialCareData/ucr/schema/${name}`)
	}
}
