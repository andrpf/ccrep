﻿var filter = {
    builder: null,

    applyFiltersFunc: function () { },
    getRules: function () {
        var rules = this.builder.queryBuilder('getRules');

        if (rules) {
            return rules.rules;
        }
        else {
            return rules;
        }
    },
    language: {
        "__locale": "Russian (ru)",

        "add_rule": "Добавить",
        "add_group": "Добавить группу",
        "delete_rule": " ",
        "delete_group": "Удалить",

        "conditions": {
            "AND": "Логическое И",
            "OR": "ИЛИ"
        },

        "operators": {
            "equal": "равно",
            "not_equal": "не равно",
            "in": "из указанных",
            "not_in": "не из указанных",
            "less": "меньше",
            "less_or_equal": "меньше или равно",
            "greater": "больше",
            "greater_or_equal": "больше или равно",
            "between": "между",
            "begins_with": "начинается с",
            "not_begins_with": "не начинается с",
            "contains": "содержит",
            "not_contains": "не содержит",
            "ends_with": "оканчивается на",
            "not_ends_with": "не оканчивается на",
            "is_empty": "пустая строка",
            "is_not_empty": "не пустая строка",
            "is_null": "пусто",
            "is_not_null": "не пусто"
        },

        "errors": {
            "no_filter": "Фильтр не выбран",
            "empty_group": "Группа пуста",
            "radio_empty": "Не выбранно значение",
            "checkbox_empty": "Не выбранно значение",
            "select_empty": "Не выбранно значение",
            "string_empty": "Не заполненно",
            "string_exceed_min_length": "Должен содержать больше {0} символов",
            "string_exceed_max_length": "Должен содержать меньше {0} символов",
            "string_invalid_format": "Неверный формат ({0})",
            "number_nan": "Не число",
            "number_not_integer": "Не число",
            "number_not_double": "Не число",
            "number_exceed_min": "Должно быть больше {0}",
            "number_exceed_max": "Должно быть меньше, чем {0}",
            "number_wrong_step": "Должно быть кратно {0}",
            "datetime_empty": "Не заполненно",
            "datetime_invalid": "Неверный формат даты ({0})",
            "datetime_exceed_min": "Должно быть, после {0}",
            "datetime_exceed_max": "Должно быть, до {0}",
            "boolean_not_valid": "Не логическое",
            "operator_not_multiple": "Оператор \"{1}\" не поддерживает много значений"
        }
    },

    init: function (tagId, data) {
        this.builder = $('#' + tagId);
        this.builder.queryBuilder({
            filters: jQuery.parseJSON(data),
            rules: [],
            allow_groups: false,
            allow_empty: true,
            sort_filters: true,
            display_errors: true,
            conditions: ['AND'],
            lang: this.language,
            icons: {
                remove_rule: "la la-remove",
                error: "la la-warning",
                add_rule: "la la-plus"
            }
        });

        $('#builderQuery').on('afterCreateRuleInput.queryBuilder', function (e, rule) {
            if (rule.filter.type === "datetime") {
                var $input = rule.$el.find('.rule-value-container [name*=_value_]');

                $input.flatpickr({
                    dateFormat: "d.m.Y"
                });
            }
        });

        $('#builderQuery').on('afterCreateRuleFilters.queryBuilder', function (e, rule) {
            var $input = rule.$el.find('.rule-filter-container select');

            $input.select2({
                language: "ru", tags: true,
                dropdownParent: $("#builderQuery")
            });

        });
    }
};