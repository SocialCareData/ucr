class UseCaseList extends HTMLElement {
	async connectedCallback() {
		const g = await new Promise(resolve => this.ownerDocument.addEventListener("data-ready", e => resolve(e.detail.data)))

		const ul = this.appendChild(this.ownerDocument.createElement("ul"))
		for (const useCase of g.useCasesDescending) {
			const li = ul.appendChild(this.ownerDocument.createElement("li"))
			const a = li.appendChild(this.ownerDocument.createElement("a"))
			const descriptionDiv = li.appendChild(this.ownerDocument.createElement("div"))
			const narrativeDocumentLink = li.appendChild(this.ownerDocument.createElement("a"))
			const commentDiv = li.appendChild(this.ownerDocument.createElement("div"))

			a.innerText = `${useCase.number} - ${useCase.title} (${useCase.requirements.length})`
			a.id = useCase.id
			a.href = `#${useCase.id}`
			li.useCase = useCase
			commentDiv.className = "comment"
			narrativeDocumentLink.href = useCase.narrativeDocument
			narrativeDocumentLink.innerText = "🔗"
			narrativeDocumentLink.target = "_blank"
			descriptionDiv.innerText = useCase.description
			descriptionDiv.className = "description"
		}

		this.dispatchEvent(new CustomEvent("usecases-loaded", { bubbles: true }))
	}
}

customElements.define("usecase-list", UseCaseList)
