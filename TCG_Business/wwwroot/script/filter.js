document.addEventListener('DOMContentLoaded', () => {
        document.querySelectorAll('.show-more').forEach(button => {
            button.addEventListener('click', () => {
                const targetId = button.getAttribute('data-target');
                const extraOptions = document.querySelector(`#${targetId} .extra-options`);
                const arrow = button.nextElementSibling;

                if (extraOptions.classList.contains('show-all')) {
                    extraOptions.classList.remove('show-all');
                    arrow.classList.remove('open-filter');
                    button.textContent = 'Show More';
                } else {
                    extraOptions.classList.add('show-all');
                    arrow.classList.add('open-filter');
                    button.textContent = 'Show Less'; // Optional: change button text
                }
            });
        });

    const handleCheckboxChange = (checkbox) => {
        checkbox.closest('form').submit();
    };

    const checkboxes = document.querySelectorAll('.filter-container input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', () => {
            handleCheckboxChange(checkbox);
        });
    });

    resetCheckboxes();
});

function resetCheckboxes() {
    const checkboxes = document.querySelectorAll('.dropdown-content input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.checked = false;
    });
}
