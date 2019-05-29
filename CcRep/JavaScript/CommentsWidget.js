function CommentsWidget(containerSelector, gComUri, sComUri, entityId) {
    this.getCommentUri = gComUri;
    this.setCommentUri = sComUri;

    this.entityId = entityId;

    this.containerSelector = containerSelector;

    this.messagesContainer = $("<div/>").attr({ class: "messagesContainer vertical-scroll scroll-example height-300 ps-container ps-theme-dark ps-active-y" }).appendTo(containerSelector);

    this.newMessageContainer = $("<div/>").attr({ class: "newMessageContainer" }).appendTo(containerSelector);

    this.init = function () {
        var context = this;
        $.ajax({
            type: "GET",
            url: this.getCommentUri
        }).done(function (data) {
            context.setMessages(data);
            context.setInputInterface();
        }).fail(function () {
            console.log('fail');
        });
    };

    this.setInputInterface = function () {
        this.newMessageContainer.empty();
        this.newMessageContainer.append([$('<br/>')]);

        var input = $("<textarea/>").attr({
            class: "form-control",
            maxlength: 100,
            required: true,
            rows: 4, placeholder: "Введите комментарий",
            style: "margin-top: 0px; margin-bottom: 0px; height: 92px;"
        }).appendTo(this.newMessageContainer);

        this.newMessageContainer.append([$('<br/>')]);

        var button = $("<button/>")
            .attr({ type: "button", class: "btn mb-1 btn-info btn-icon btn-md btn-block" })
            .appendTo(this.newMessageContainer).html("Отправить");

        var context = this;
        button.on("click", function () {
            var comment = $.trim(input.val());

            if (comment) {
                input.val("");
                $(containerSelector).block({
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

                $.ajax({
                    type: "POST",
                    url: context.setCommentUri,
                    data: { TranId: context.entityId, Comment: comment }
                }).done(function (data) {
                    console.log(data);
                    context.setMessages(data);
                    $(containerSelector).unblock();

                    context.messagesContainer.scrollTop(context.messagesContainer.prop("scrollHeight"));
                    context.messagesContainer.perfectScrollbar('update');

                }).fail(function () {
                    console.log('fail');
                });
            }
        });
    };

    this.setMessages = function (messages) {
        var context = this;

        this.messagesContainer.empty();

        $.each(messages, function (index, element) {
            console.log(element);
            var elementNew = context.getMessageItem(element.User, element.Date, element.Comment);

            elementNew.appendTo(context.messagesContainer);

            $("<br/>").appendTo(context.messagesContainer);
        });
    };

    this.getMessageItem = function (userName, date, message) {
        var a = $('<a/>').attr({
            href: 'javascript:void(0)'
        });

        var div = $('<div/>').attr({
            class: "media"

        }).appendTo(a);

        var mediaBody = $('<div/>').attr({ class: "media-body" }).appendTo(div);

        var h6 = $('<h6/>').attr({ class: "media-heading yellow darken-3" }).html(userName).appendTo(mediaBody);

        var p = $('<p/>').attr({ class: "notification-text font-small-3 text-muted" }).html(message).appendTo(mediaBody);

        var small = $('<small/>').appendTo(mediaBody);

        var time = $('<time/>').attr({ class: "media-meta text-muted" }).html(date).appendTo(small);

        return a;
    }

    this.drawMessages = function () {
    }
};