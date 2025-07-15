document.addEventListener('DOMContentLoaded', function () {
    // Авто-сабмит формы при изменении селектов
    document.querySelectorAll('select[name]').forEach(select => {
        select.addEventListener('change', function () {
            this.form.submit();
        });
    });

    // Сброс фильтров
    document.getElementById('resetFilters').addEventListener('click', function () {
        window.location.href = window.location.pathname;
    });
});