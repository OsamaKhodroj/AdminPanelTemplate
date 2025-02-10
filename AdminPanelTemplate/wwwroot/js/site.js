function funDeleteUser(id) {
    var confirmDelete = confirm("Are you sure you want to delete selected user?");
    if (confirmDelete)
        window.location = '/Users/Delete/' + id;
}