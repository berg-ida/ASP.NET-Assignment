document.addEventListener('DOMContentLoaded', () => {
    const addProjectForm = document.querySelector('.addProject');
    if (addProjectForm) {
        addProjectForm.addEventListener('submit', async function (e) {
            e.preventDefault();

            const formData = new FormData(this)
            const response = await fetch('', {
                method: 'Post',
                body: formData
            });

            if (response.ok) {
                window.location.reload();
            }
            else {
                const result = await response.json();
                console.error(result.errors);
            }
        })
    }
})