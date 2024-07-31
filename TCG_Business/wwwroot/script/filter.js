document.addEventListener('DOMContentLoaded', () => {
    const handleDropdownToggle = (toggle) => {
        const arrow = toggle.querySelector('.filter-arrow');
        const dropdownId = toggle.getAttribute('data-target');
        const dropdownContent = document.getElementById(dropdownId);

        if (!dropdownContent) {
            console.error(`Dropdown content with ID ${dropdownId} not found.`);
            return;
        }

        // Toggle arrow rotation
        arrow.classList.toggle('open-filter');

        // Toggle dropdown content visibility
        dropdownContent.classList.toggle('show');

        // Smoothly expand or collapse dropdown content
        if (dropdownContent.classList.contains('show')) {
            dropdownContent.style.maxHeight = dropdownContent.scrollHeight + 'px';
            dropdownContent.style.opacity = '1';
        } else {
            dropdownContent.style.maxHeight = '0';
            dropdownContent.style.opacity = '0';
        }
    };

    // Add event listeners for dropdown toggles
    document.querySelectorAll('.dropdown-toggle').forEach(toggle => {
        toggle.addEventListener('click', () => {
            handleDropdownToggle(toggle);
        });
    });

    // Add event listeners for show-more buttons
    document.querySelectorAll('.show-more').forEach(button => {
        button.addEventListener('click', () => {
            const targetId = button.getAttribute('data-target');
            const dropdownContent = document.getElementById(targetId);
            const extraOptions = dropdownContent.querySelector('.extra-options');

            if (!extraOptions) {
                console.error(`Extra options element not found in dropdown content with ID ${targetId}.`);
                return;
            }

            // Toggle extra options visibility
            const isExpanded = extraOptions.classList.toggle('show-all');
            button.textContent = isExpanded ? 'Show Less' : 'Show More';

            // Adjust dropdown content height to match the expanded content
            dropdownContent.style.maxHeight = dropdownContent.scrollHeight + 'px';
        });
    });

    // Handle checkbox changes
    const handleCheckboxChange = (checkbox) => {
        checkbox.closest('form').submit();
    };

    const checkboxes = document.querySelectorAll('.filter-container input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', () => {
            handleCheckboxChange(checkbox);
        });
    });

    // Optionally reset checkboxes if needed
    resetCheckboxes();
});

// Reset checkboxes (optional, depending on your needs)
function resetCheckboxes() {
    const checkboxes = document.querySelectorAll('.dropdown-content input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.checked = false;
    });
}
