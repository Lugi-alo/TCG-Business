document.addEventListener('DOMContentLoaded', (event) => {
    const handleDropdownToggle = (toggle) => {
        const arrow = toggle.querySelector('.filter-arrow');
        const dropdownId = toggle.getAttribute('data-toggle');
        const dropdownContent = document.getElementById(dropdownId);

        arrow.classList.toggle('open-filter');
        dropdownContent.classList.toggle('show');

        const isVisible = dropdownContent.classList.contains('show');

        if (isVisible) {
            dropdownContent.style.height = dropdownContent.scrollHeight + 'px';
            dropdownContent.style.opacity = '1';
        } else {
            dropdownContent.style.height = '0';
            dropdownContent.style.opacity = '0';

            dropdownContent.addEventListener('transitionend', () => {
                dropdownContent.style.height = '';
            }, { once: true });
        }
    };

    const dropdownToggles = document.querySelectorAll('.dropdown-toggle');
    dropdownToggles.forEach(toggle => {
        toggle.addEventListener('click', () => {
            handleDropdownToggle(toggle);
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