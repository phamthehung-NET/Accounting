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