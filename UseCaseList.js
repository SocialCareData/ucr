class UseCaseList extends HTMLElement {
	async connectedCallback() {
		const g = await new Promise(resolve => this.ownerDocument.addEventListener("data-ready", e => resolve(e.detail.data)))

		const ul = this.appendChild(this.ownerDocument.createElement("ul"))
		for (const useCase of g.useCases) {
			const li = ul.appendChild(this.ownerDocument.createElement("li"))
			const a = li.appendChild(this.ownerDocument.createElement("a"))
			const descriptionDiv = li.appendChild(this.ownerDocument.createElement("div"))
			const commentDiv = li.appendChild(this.ownerDocument.createElement("div"))

			a.innerText = `${useCase.number} - ${useCase.title} (${useCase.requirements.length})`
			a.id = `usecase-${useCase.number}`
			a.href = `#usecase-${useCase.number}`
			li.useCase = useCase
			commentDiv.className = "comment"
			descriptionDiv.innerText = useCase.description
			descriptionDiv.className = "description"
		}

		this.dispatchEvent(new CustomEvent("usecases-loaded", { bubbles: true }))
	}
}

customElements.define("usecase-list", UseCaseList)