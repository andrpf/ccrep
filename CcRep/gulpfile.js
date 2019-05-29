/// <binding />
var gulp = require('gulp');
var uglify = require('gulp-uglify');
var uglifycss = require('gulp-uglifycss');
var concat = require('gulp-concat');
var rimraf = require("rimraf");
var merge = require('merge-stream');

function Clean(cb) {
    return rimraf("wwwroot/", cb);
}

function CleanUserJs(cb) {
    return rimraf("wwwroot/js/app", cb);
}
function CleanUserCss(cb) {
    return rimraf("wwwroot/css/app", cb);
}

function Jsons() {
    var streams = [
        gulp.src([
            "node_modules/cldr-data/supplemental/likelySubtags.json",
            "node_modules/cldr-data/main/en/numbers.json",
            "node_modules/cldr-data/supplemental/numberingSystems.json",
            "node_modules/cldr-data/main/en/ca-gregorian.json",
            "node_modules/cldr-data/main/en/timeZoneNames.json",
            "node_modules/cldr-data/supplemental/timeData.json",
            "node_modules/cldr-data/supplemental/weekData.json"
        ])
            .pipe(gulp.dest("wwwroot/js/vendor/json/cldr/en")),

        gulp.src([
            "node_modules/cldr-data/main/ru/numbers.json",
            "node_modules/cldr-data/main/ru/ca-gregorian.json",
            "node_modules/cldr-data/main/ru/timeZoneNames.json"
        ])
            .pipe(gulp.dest("wwwroot/js/vendor/json/cldr/ru")),

    ];

    return merge(streams);
}

function CssVendorHead() {
    var streams = [
        gulp.src([
            "Theme/vendors/css/forms/listbox/bootstrap-duallistbox.min.css",
            "Theme/css/plugins/forms/dual-listbox.css",
            "node_modules/flatpickr/dist/flatpickr.css",
            "node_modules/jQuery-QueryBuilder/dist/css/query-builder.default.css",

            "Theme/css/bootstrap.min.css",
            "Content/fonts/flag-icon-css/css/flag-icon.css",
            "Theme/vendors/css/extensions/pace.css",
            "Theme/vendors/css/ui/prism.min.css",
            "Content/line_awesome/line-awesome.css",
            "Content/feather/style.css",

            "Theme/css/plugins/loaders/loaders.min.css",
            "Theme/css/core/colors/palette-loader.css",
            "Theme/css/core/menu/menu-types/vertical-menu.css",
            "Theme/css/core/colors/palette-gradient.css",
            "Theme/css/core/colors/palette-tooltip.css",

            "Content/icheck/minimal/_all.css",
            "Content/icheck/square/_all.css",
            "Content/icheck/flat/_all.css",
            "Content/icheck/line/_all.css",
            "Content/icheck/polaris/polaris.css",
            "Content/icheck/futurico/futurico.css",
            "Content/icheck/custom.css",

            "Theme/vendors/css/forms/selects/select2.min.css",
            "Theme/vendors/css/extensions/jquery.toolbar.css",

            "Theme/vendors/css/tables/datatable/datatables.min.css",
            "Theme/vendors/css/tables/datatable/buttons.bootstrap4.min.css",

            "Theme/css/bootstrap-extended.css",
            "Theme/css/colors.css",
            "Theme/css/components.css",

            "Theme/css/plugins/forms/wizard.css"
        ])
            .pipe(uglifycss({
                "uglyComments": true
            }))
            .pipe(concat("vendor-head.min.css"))
            .pipe(gulp.dest("wwwroot/css/vendor"))
    ];

    return merge(streams);
}

function CssHead() {
    var streams = [
        gulp.src([
            "Content/Site.css"
        ])
            .pipe(uglifycss({
                "uglyComments": true
            }))
            .pipe(concat("app.min.css"))
            .pipe(gulp.dest("wwwroot/css/app"))
    ];

    return merge(streams);
}

function JsVendorHead() {
    var streams = [
        gulp.src([
            "node_modules/jquery/dist/jquery.js",
            "Theme/vendors/js/modernizr/modernizr-2.8.3.js"
        ])
            .pipe(uglify())
            .pipe(concat("head.min.js"))
            .pipe(gulp.dest("wwwroot/js/vendor"))
    ];

    return merge(streams);
}

function JsVendor() {
    var streams = [
        gulp.src([
            "Theme/vendors/js/vendors.min.js",
            "Theme/vendors/js/ui/headroom.min.js",
            "Theme/vendors/js/ui/prism.min.js",
            "Theme/js/core/app-menu.js",
            "Theme/js/core/app.js",

            "Theme/js/scripts/tooltip/tooltip.js",
            "Theme/vendors/js/forms/icheck/icheck.min.js",
            "Theme/vendors/js/forms/select/select2.full.min.js",
            "Theme/js/scripts/ui/scrollable.min.js",
            "Theme/js/scripts/extensions/block-ui.js",

            //JQuery Toolabar
            "Theme/vendors/js/extensions/jquery.toolbar.min.js",

            "node_modules/flatpickr/dist/flatpickr.js",
            "node_modules/flatpickr/dist/l10n/ru.js",

            "node_modules/jquery-extendext/jQuery.extendext.js",
            "node_modules/dot/doT.js",

            "node_modules/jQuery-QueryBuilder/dist/js/query-builder.js",

            "Theme/vendors/js/forms/listbox/jquery.bootstrap-duallistbox.min.js",
            "Theme/js/scripts/forms/listbox/form-duallistbox.js",

            "node_modules/cldrjs/dist/cldr.js",
            "node_modules/cldrjs/dist/cldr/event.js",
            "node_modules/cldrjs/dist/cldr/supplemental.js",

            "node_modules/globalize/dist/globalize.js",
            "node_modules/globalize/dist/globalize/message.js",
            "node_modules/globalize/dist/globalize/number.js",
            "node_modules/globalize/dist/globalize/plural.js",
            "node_modules/globalize/dist/globalize/date.js",
            "node_modules/globalize/dist/globalize/currency.js",

            "node_modules/jquery-validation/dist/jquery.validate.js",
            "node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js",
            "Theme/vendors/js/validation/jquery.validate.globalize.js",

            // jQuery DataTables
            "Theme/vendors/js/tables/datatable/datatables.min.js",
            "Theme/vendors/js/tables/datatable/datatables.buttons.min.js",
            "Theme/vendors/js/tables/datatable/dataTables.colVis.js",
            "Theme/vendors/js/tables/buttons.flash.min.js",
            "Theme/vendors/js/tables/jszip.min.js",
            "Theme/vendors/js/tables/pdfmake.min.js",
            "Theme/vendors/js/tables/vfs_fonts.js",
            "Theme/vendors/js/tables/buttons.html5.min.js",
            "Theme/vendors/js/tables/buttons.print.min.js",

            "node_modules/jquery.cookie/jquery.cookie.js",
            "Theme/vendors/js/extensions/jquery.steps.min.js"
        ])
            .pipe(uglify())
            .pipe(concat("vendor.min.js"))
            .pipe(gulp.dest("wwwroot/js/vendor"))
    ];

    return merge(streams);
}

function AppJs() {
    var streams = [
        gulp.src([
            "JavaScript/opfilts.js",
            "JavaScript/common.js",
            "JavaScript/CommentsWidget.js",
            "JavaScript/initRuGlobalize.js"
        ])
            .pipe(uglify())
            .pipe(concat("app.min.js"))
            .pipe(gulp.dest("wwwroot/js/app"))
    ];

    return merge(streams);
}

let allInit = gulp.series(Clean, Jsons, CssVendorHead, CssHead, JsVendorHead, JsVendor, AppJs);

let usersScriptsInit = gulp.series(
    CleanUserCss,
    CleanUserJs,
    CssHead,
    AppJs
);

gulp.task("allInit", allInit);

gulp.task("usersScriptsInit", usersScriptsInit);