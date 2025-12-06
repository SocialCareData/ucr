class RequirementList extends HTMLElement {
	async connectedCallback() {
		const g = await new Promise(resolve => this.ownerDocument.addEventListener("data-ready", e => resolve(e.detail.data)))

		const ul = this.appendChild(this.ownerDocument.createElement("ul"))
		for (const requirement of g.requirements) {
			const li = ul.appendChild(this.ownerDocument.createElement("li"))
			const a = li.appendChild(this.ownerDocument.createElement("a"))
			const descriptionDiv = li.appendChild(this.ownerDocument.createElement("div"))
			const commentDiv = li.appendChild(this.ownerDocument.createElement("div"))

			a.innerText = `${requirement.number} - ${requirement.title} (${requirement.useCases.length})`
			a.id = `requirement-${requirement.number}`
			a.href = `#requirement-${requirement.number}`
			li.requirement = requirement
			commentDiv.className = "comment"
			descriptionDiv.innerText = requirement.description
			descriptionDiv.className = "description"
		}

		this.dispatchEvent(new CustomEvent("requirements-loaded", { bubbles: true }))
	}
}

customElements.define("requirement-list", RequirementList)