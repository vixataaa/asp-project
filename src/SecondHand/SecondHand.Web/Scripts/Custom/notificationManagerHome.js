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
        const row = document.createElement('div');
        row.classList.add('row');

        const col = document.createElement('div');
        col.classList.add('col-md-8');
        col.classList.add('col-md-offset-4');

        const listGroup = document.createElement('div');
        listGroup.classList.add('list-group');

        const a = document.createElement('a');
        a.classList.add('list-group-item');
        a.classList.add('right');
        a.classList.add('text-right');

        const h4 = document.createElement('h4');
        h4.classList.add('list-group-item-heading');

        const p = document.createElement('p');
        p.classList.add('list-group-item-text');

        const date = new Date()
        h4.innerHTML = `${author} (${date.getMonth()}/${date.getDay()}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()})`;
        p.innerHTML = message;

        row.appendChild(col);
        col.appendChild(listGroup);
        listGroup.appendChild(a);
        a.appendChild(h4);
        a.appendChild(p);

        document.getElementById('messages').appendChild(row);
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