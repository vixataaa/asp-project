/*eslint-disable*/
const clientId = '9dd341bb27efbfd';
const uploadUrl = 'https://api.imgur.com/3/image';

const uploadToApi = (url, client, file) => {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: url,
            type: 'POST',
            headers: {
                'Authorization': 'Client-ID ' + client,
            },
            data: file,
            success: resolve,
            error: reject,
            processData: false,
        });
    });
};

const attachImgUploadEvent = (id, targetId, statusId) => {
    const selector = '#' + id;

    $(selector).on('change', () => {
        const file = document.getElementById(id).files[0];

        const statusSelector = '#' + statusId;

        $(statusSelector).removeClass('btn-primary');
        $(statusSelector).addClass('btn-default');
        $(statusSelector).html('Uploading...');
        $('#submit-btn').prop('disabled', true);

        uploadToApi(uploadUrl, clientId, file)
            .then((res) => {
                const targetInput = '#' + targetId;
                $(targetInput).val(res.data.link);

                $(statusSelector).addClass('disabled');
                $(statusSelector).html('Uploaded');
                $(selector).prop('disabled', true);
                $('#submit-btn').prop('disabled', false);
            })
            .catch((res) => {
                $(statusSelector).addClass('btn-primary');
                $(statusSelector).removeClass('btn-default');
                $(statusSelector).removeClass('disabled');
                $(statusSelector).html('Failed');
                $('#submit-btn').prop('disabled', false);
            });
    });
};

attachImgUploadEvent('img1-input', 'photo-1-url', 'img1-status');
attachImgUploadEvent('img2-input', 'photo-2-url', 'img2-status');
attachImgUploadEvent('img3-input', 'photo-3-url', 'img3-status');