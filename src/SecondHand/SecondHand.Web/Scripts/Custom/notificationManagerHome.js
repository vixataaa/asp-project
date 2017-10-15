var hub = $.connection.notificationHub;

$(() => {
    const currentChatId = $('#chat-id').val();

    const getNotificationCount = () => {
        return new Promise((resolve, reject) => {
            $.ajax({
                method: 'POST',
                url: '/chats/chats/getnotificationscount',
                dataType: 'json',
                success: resolve,
                error: reject
            });
        });
    };   

    const filterNotificationsRequest = (chatId) => {
        return new Promise((resolve, reject) => {
            $.ajax({
                method: 'POST',
                url: '/chats/chats/removenotification',
                data: {
                    chatId: chatId
                },
                dataType: 'json',
                success: resolve,
                error: reject
            });
        });
    };

    const appendMessage = (author, message) => {
        const messagesUl = document.getElementById('messages');

        const li = document.createElement('li');
        li.innerHTML = `${author} ---> ${message}`;

        messagesUl.appendChild(li);
    };


    hub.client.updateNotifications = (count, chatID, senderName, message) => {
        if (currentChatId !== chatID) {
            $('#notif-badge').html(count);
        }
        else {
            appendMessage(senderName, message);
            filterNotificationsRequest(currentChatId);
        }
    };

    hub.client.requestNotifications = () => {
        getNotificationCount()
            .then((data) => {
                if (data !== 0) {
                    $('#notif-badge').html(data);
                }
            });
    };

    $.connection.hub.start();
});