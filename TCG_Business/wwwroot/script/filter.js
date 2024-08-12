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
                    button.textContent = 'Show Less';
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

document.addEventListener('DOMContentLoaded', () => {
    const modal = document.getElementById('modal');
    const modalContent = document.getElementById('modal-content');
    const closeButton = document.getElementsByClassName('close')[0];

    window.openModal = function (singlesId) {
        fetch(`/singles?handler=SingleDetails&id=${singlesId}`)
            .then(response => response.json())
            .then(data => {
                document.getElementById('modal-image').src = `~${data.image}`;
                document.getElementById('modal-name').textContent = data.name;
                document.getElementById('modal-rarity').textContent = `Rarity: ${data.rarity}`;
                document.getElementById('modal-setName').textContent = `Set: ${data.setName}`;
                document.getElementById('modal-price').textContent = `Price: £${data.price.toFixed(2)}`;

                modal.style.display = "block";
            })
            .catch(error => {
                console.error('Error fetching item details:', error);
            });
    };

    window.closeModal = function () {
        modal.style.display = "none";
    };

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    };
});


function resetCheckboxes() {
    const checkboxes = document.querySelectorAll('.dropdown-content input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.checked = false;

    });
}

function applySortOrder(sortOrder) {
    document.getElementById('sortOrder').value = sortOrder;
    document.getElementById('filtersForm').submit();
}