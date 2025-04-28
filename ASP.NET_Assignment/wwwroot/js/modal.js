document.addEventListener('DOMContentLoaded', () => {

    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal) {

                //koden nedan är skapad med ai för att dropdown ska följa givet projekt
                if (modalTarget === '#editAndDeleteModal') {

                    const buttonRect = button.getBoundingClientRect();
                    modal.style.top = `${buttonRect.bottom + window.scrollY}px`;
                    modal.style.left = `${buttonRect.left + window.scrollX - 140}px`;

                    const projectId = button.getAttribute('data-project-id');
                    if (projectId) {
                        document.getElementById('projectIdInput').value = projectId;
                    }
                }
                //

                const toggleButton = button.hasAttribute('data-close');
                if (toggleButton) {
                    if (modal.style.display === 'flex') {
                        modal.style.display = 'none';
                    }
                    else {
                        modal.style.display = 'flex';
                    }
                }
                else {
                    modal.style.display = 'flex';
                }
            }
        })
    })

    const closeButtons = document.querySelectorAll('[data-close="true"]:not([data-modal="true"])')
    closeButtons.forEach(button => {
        button.addEventListener('click', (e) => {
            e.preventDefault();
            const modal = button.closest('.modal')
            if (modal) {
                modal.style.display = 'none'

                modal.querySelectorAll('form').forEach(form => {
                    form.reset();
                })
            }
        })
    })

    const editForm = document.querySelector('#editProjectModal');
    if (editForm) {
        editForm.addEventListener('submit', async function (e) {
            e.preventDefault();

            const formData = new FormData(this);
            try {
                const response = await fetch(this.action, {
                    method: 'POST',
                    body: formData
                });

                if (response.ok) {
                    const result = await response.json();
                    const modal = this.closest('.modal');
                    if (modal) modal.style.display = 'none';
                    updateProjectCard(result.project);
                }
            } catch (error) {
                console.error('Error:', error);
            }
        })
    };

    const forms = document.querySelectorAll('form.ajax-form')
    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault()

            clearErrorMessages(form)

            const formData = new FormData(form)
            try {
                const res = await fetch(form.action, {
                    method: 'post',
                    body: formData
                })
                if (res.ok) {
                    const modal = form.closest('.modal')
                    if (modal) {
                        modal.style.display = 'none';
                    }
                    window.location.reload();
                }
                else if (res.status === 400) {
                    const data = await res.json()

                    if (data.errors) {
                        Object.keys(data.errors).forEach(key => {
                            const input = form.querySelector(`[name="${key}"]`)
                            if (input) {
                                input.classList.add('input-validation-error')
                            }

                            const span = form.querySelector(`[data-valmsg-for="${key}"]`)
                            if (span) {
                                span.innerText = data.errors[key].join('\n')
                                span.classList.add('field-validation-error')
                            }

                        })
                    }
                }

            }
            catch {
                console.log('error submitting the form')
            }
        })
    })



    function clearErrorMessages(form) {

        form.querySelectorAll('[data-val="true"]').forEach(input => {
            input.classList.remove('input-validation-error');
        });

        form.querySelectorAll('[data-valmsg-for]').forEach(span => {
            span.innerText = ''
            span.classList.remove('field-validation-error');
        });
    }

    function addErrorMessages(key, errorMessage) {
        const input = form.querySelector(`[name="${key}"]`)
        if (input) {
            input.classList.add('input-validation-error')
        }

        const span = form.querySelector(`[data-valmsg-for="${key}"]`)
        if (span) {
            span.innerText = errorMessage
            span.classList.add('field-validation-error')
        }
    }

    //koden nedan är delvis skapad med ai för att göra en fungerande js funktion på status knapparna
    function updateProjectCard(updatedProject) {
        const projectCard = document.querySelector(`.project[data-project-id="${updatedProject.id}"]`);
        if (!projectCard) return;

        projectCard.setAttribute('data-status', updatedProject.status.toLowerCase());
        const activeFilter = document.querySelector('.status-button.active');
        if (activeFilter) {
            const filter = activeFilter.textContent.split('[')[0].trim().toLowerCase();
            projectCard.style.display =
                (filter === 'all' || updatedProject.status.toLowerCase().includes(filter))
                    ? 'block'
                    : 'none';
        }
    }
})