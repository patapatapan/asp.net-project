function DeleteData(rowId) {
    swal({
        title: "您確定要刪除?",
        text: "請確認是否要刪除此筆資料?",
        showCancelButton: true,
        closeOnConfirm: false,
        cancelButtonText: "取消",
        cancelButtonClass: "btn btn-secondary",
        confirmButtonText: "刪除",
        confirmButtonClass: "btn-danger"
    },
        function () {
            $.ajax({
                url: "@Url.Action(ActionService.Delete , ActionService.Controller , new { area = ActionService.Area })",
                data:
                {
                    "id": rowId
                },
                type: "POST"
            })
                .done(function (data) {
                    sweetAlert
                        ({
                            title: "刪除成功!!",
                            text: "您的資料已經刪除完成!!",
                            type: "success"
                        },
                            function () {
                                window.location.href = '@Url.Action(ActionService.Index , ActionService.Controller , new { area = ActionService.Area })';
                            });
                })
                .error(function (data) {
                    swal("錯誤", "無法連線到網站!!", "error");
                });
        });
}