var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(){
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        columns: [
            { data: 'name', "width": "15%" },
            { data: 'email', "width": "15%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'company.name', "width": "15%" },
            { data: 'role', "width": "15%" },
            {
                data: {id: "id", lockoutEnd: "lockoutEnd"}, "render": function (data) {

                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {

                         return `<div class="text-center">
                            <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-sm rounded-2 btn-warning text-white" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square"></i>" Permissions
                            </a>
                            <a onclick=LockUnlock('${data.id}') class="btn btn-sm rounded-2 btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-unlock-fill"></i>" Unlock
                            </a>
                         </div>
                    `
                    } else{

                        return `<div class="text-center">
                            <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-sm rounded-2 btn-warning text-white" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square"></i>" Permissions
                            </a>
                            <a onclick=LockUnlock('${data.id}') class="btn btn-sm rounded-2 btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-unlock-fill"></i>" Lock
                            </a>
                         </div>`
                    }                
                },
                "width": "25%"
            }
        ]
    });
}

function LockUnlock(id) {

    $.ajax({
        type: "POST",
        url: "/Admin/User/LockUnlock",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    })
}