const category = {
    init: () => {
        category.registerEvents();
    },
    registerEvents: () => {
        $('.btn-active').off('click').on('click', function (e) {

            const btn = $(this);
            const id = btn.data("id");
            $.ajax({
                url: "/Admin/Category/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    res.status ? btn.text("Active") : btn.text("Blocking");
                }
            })
            e.preventDefault();
        })
    }
}

category.init();