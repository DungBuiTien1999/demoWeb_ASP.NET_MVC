const user = {
    init: () => {
        user.registerEvents();
    },
    registerEvents: () => {
        $('.btn-active').off('click').on('click', function(e)  {
            
            const btn = $(this);
            const id = btn.data("id");
            console.log(id);
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function(res) {
                    console.log(res);
                    res.status ? btn.text("Active") : btn.text("Block");
                }
            })
            e.preventDefault();
        })
    }
}

user.init();