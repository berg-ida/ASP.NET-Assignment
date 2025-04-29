document.addEventListener('DOMContentLoaded', () => {

    //koden nedan är delvis skapad med ai för att göra en fungerande js funktion på status knapparna
    const statusButtons = document.querySelectorAll('.status-button');
    statusButtons.forEach(button => {
        button.addEventListener('click', function (e) {
            e.preventDefault();

            statusButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');

            const filter = this.dataset.filter ||
                this.textContent.split('[')[0].trim().toLowerCase();

            const projects = document.querySelectorAll('.project');
            projects.forEach(project => {
                const projectStatus = project.getAttribute('data-status') || '';
                project.style.display = (filter === 'all' || projectStatus === filter)
                    ? 'block'
                    : 'none';
            });
        });
    })

    //

    document.querySelectorAll('.edit-button').forEach(button => {
        button.addEventListener('click', function () {
            const projectId = this.getAttribute('data-project-id');
            document.getElementById('projectIdInput').value = projectId;
        });
    });
})

