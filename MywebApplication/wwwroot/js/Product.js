var dTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dTable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Admin/Product/AllProduct"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "description", "width": "25%" },
            { "data": "price", "width": "15%" },
            {
                "data": "imageUrl",
                "render": function (data) {
                    return `<img src="${data}" width="50px" height="50px" style="border-radius:5px; object-fit:cover;" />`;
                },
                "width": "15%"
            },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/CreateUpdate?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a onClick="RemoveProduct('/Admin/Product/Delete/${data}')" class="btn btn-danger mx-2">
                                <i class="bi bi-trash"></i> Delete
                            </a>
                        </div>
                    `;
                },
                "width": "20%"
            }
        ]
    });
}

function RemoveProduct(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dTable.ajax.reload();
                        toastr.success(data.message || data.msg);
                    } else {
                        toastr.error(data.message || data.msg);
                    }
                },
                error: function (err) {
                    toastr.error("Kuch gadbad hui, dobara try karein!");
                }
            });
        }
    });
}