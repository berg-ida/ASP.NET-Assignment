document.addEventListener('DOMContentLoaded', () => {

    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal) {
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
        button.addEventListener('click', () => {
            const modal = button.closest('.modal')
            if (modal) {
                modal.style.display = 'none'
            }
        })
    })
})