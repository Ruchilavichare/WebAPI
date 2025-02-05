// Example: Add a confirmation dialog for delete actions
document.addEventListener("DOMContentLoaded", function () {
    var deleteButtons = document.querySelectorAll(".btn-delete");
    deleteButtons.forEach(function (button) {
        button.addEventListener("click", function (e) {
            if (!confirm("Are you sure you want to delete this item?")) {
                e.preventDefault();
            }
        });
    });
});