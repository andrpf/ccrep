// Start Script
$(window).on('load', function () {
    $('.fadeInOnLoad').fadeIn(600);

    $('body').on("click", 'button.btn[href]', function () {
        window.location.href = $(this).attr("href");
    });

    transformDateTimeFields();
    transformCheckboxes();
    prepareScrollbar();
    transformSelects();

    $(".disableListBox").on("ifChanged", function (event) {
        var containerClass = $(this).attr("data-listboxcont");

        $("." + containerClass).find("*").prop("disabled", event.target.checked);
    });

    dlb = $(".disableListBox");

    if (dlb) {
        disLboxChecked = dlb.parent('[class*="icheckbox"]').hasClass("checked");

        if (disLboxChecked) {
            var containerClass = dlb.attr("data-listboxcont");

            $("." + containerClass).find("*").prop("disabled", disLboxChecked);
        }
    }

    // Tooltip Initialization
    $('[data-toggle="tooltip"]').tooltip({
        container: 'body'
    });
});

$('.onSubmitBlock').on("submit", function () {
    if ($(this).valid()) {
        $.blockUI({
            message: '<div class="ft-refresh-cw icon-spin font-medium-2"></div>',
            overlayCSS: {
                backgroundColor: '#FFF',
                cursor: 'wait'
            },
            css: {
                border: 0,
                padding: 0,
                backgroundColor: 'none'
            }
        });
    }
});

function transformCheckboxes() {
    var checkboxInpust = $("input[type=checkbox]");

    if (checkboxInpust.length !== 0) {

        checkboxInpust.iCheck({
            checkboxClass: 'icheckbox_square-red',
            radioClass: 'iradio_minimal-grey'
        });
    }
}

function transformSelects() {
    $.fn.select2.amd.define('select2/i18n/ru', [], function () {
        // Russian
        return {
            errorLoading: function () {
                return 'Результат не может быть загружен.';
            },
            inputTooLong: function (args) {
                var overChars = args.input.length - args.maximum;
                var message = 'Пожалуйста, удалите ' + overChars + ' символ';
                if (overChars >= 2 && overChars <= 4) {
                    message += 'а';
                } else if (overChars >= 5) {
                    message += 'ов';
                }
                return message;
            },
            inputTooShort: function (args) {
                var remainingChars = args.minimum - args.input.length;

                var message = 'Пожалуйста, введите ' + remainingChars + ' или более символов';

                return message;
            },
            loadingMore: function () {
                return 'Загружаем ещё ресурсы…';
            },
            maximumSelected: function (args) {
                var message = 'Вы можете выбрать ' + args.maximum + ' элемент';

                if (args.maximum >= 2 && args.maximum <= 4) {
                    message += 'а';
                } else if (args.maximum >= 5) {
                    message += 'ов';
                }

                return message;
            },
            noResults: function () {
                return 'Ничего не найдено';
            },
            searching: function () {
                return 'Поиск…';
            }
        };
    });
    var select2Inputs = $(".select2");
    select2Inputs.select2({
        width: '100%',
        language: "ru"
    });
}

function prepareScrollbar() {
    var scrollElements = $(".scrolling");

    scrollElements.perfectScrollbar();
}

function transformDateTimeFields() {
    flatpickr.localize(flatpickr.l10ns.ru);

    var dateInputs = $(".date-picker");

    if (dateInputs.length !== 0) {
        dateInputs.wrap('<div class="clearDatePick input-group"></div>');
        $('.clearDatePick').append(
            '<div class="input-group-append"><button data-toggle="tooltip" data-placement="top" data-original-title="Очистить" class="btn btn-info clearDateButton" type= "button"> <i class="la la-calendar-times-o"></i></button></div >'
        );
        dateInputs.flatpickr({
            dateFormat: "d.m.Y"
        });

        $(".clearDateButton").tooltip({ container: "body" });
        $(".clearDateButton").on("click", function () {
            $(this).closest("div.input-group").children().val("");
        });
    }
}

function RenderGridYesNo(data, type, full, meta) {
    var changedData = "";
    var cssClass = "badge-warning";

    switch (data) {
        case "Да":
            cssClass = "badge-info";
            break;
        case "Нет":
            cssClass = "badge-danger";
            break;
    }

    return "<div class='badge " + cssClass + " '>" + data + "</div>";
}
