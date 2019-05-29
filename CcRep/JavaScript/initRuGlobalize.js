$.when(
    $.getJSON("/wwwroot/js/vendor/json/cldr/en/likelySubtags.json"),
    $.getJSON("/wwwroot/js/vendor/json/cldr/en/numbers.json"),
    $.getJSON("/wwwroot/js/vendor/json/cldr/en/numberingSystems.json"),
    $.getJSON("/wwwroot/js/vendor/json/cldr/en/ca-gregorian.json"),
    $.getJSON("/wwwroot/js/vendor/json/cldr/en/timeZoneNames.json"),
    $.getJSON("/wwwroot/js/vendor/json/cldr/en/timeData.json"),
    $.getJSON("/wwwroot/js/vendor/json/cldr/en/weekData.json"),

    //Русский формат
    $.getJSON("/wwwroot/js/vendor/json/cldr/ru/numbers.json"),
    $.getJSON("/wwwroot/js/vendor/json/cldr/ru/ca-gregorian.json"),
    $.getJSON("/wwwroot/js/vendor/json/cldr/ru/timeZoneNames.json")
).then(function () {
    return [].slice.apply(arguments, [0]).map(function (result) {
        return result[0];
    });
}).then(Globalize.load).then(function () {
    Globalize.locale("ru");
});