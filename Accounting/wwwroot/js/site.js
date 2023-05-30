window.showToast = (color, content) => {
    $(".toast-container").empty()
    $(".toast-container").append(`
        <div class="toast align-items-center text-white bg-${color} border-0" role="alert" aria-live="assertive" aria-atomic="true">
          <div class="d-flex">
            <div class="toast-body">
                ${content}
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
          </div>
        </div>
    `)
    $('.toast').toast('show');
}

window.triggerBtn = (id) => {
    $(`#${id}`).trigger('click');
}

window.hideModal = (id) => {
    $(`#${id}`).modal('toggle');
}

window.addIndicator = () => {
    $("body").append(
        `<div class="spinner-container d-flex justify-content-center align-items-center h-100 w-100 top-0 start-0 position-fixed bg-secondary bg-opacity-75 text-info" style="z-index: 9999999999999" id="spinner-container">
            <div class="spinner-border" style="width: 3rem; height: 3rem;" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>`
    )
}

window.removeIndicator = () => {
    var t = $("#spinner-container")
    $("#spinner-container").remove()
}

window.toggleSideBar = () => {
    const $this = $('#btn-nav-toggler');
    const sidebar = $(`.sidebar`);
    const isExpanded = $this.attr('aria-expanded');
    if (isExpanded.toLowerCase() === "true") {
        sidebar.attr('hidden', true);
        $this.attr('aria-expanded', false);
        $this.children('i').removeClass('fa-regular fa-xmark').addClass('fa-regular fa-bars');
    } else {
        sidebar.removeAttr('hidden');
        $this.attr('aria-expanded', true);
        $this.children('i').removeClass('fa-regular fa-bars').addClass('fa-regular fa-xmark');
    }
    $this.blur();
}