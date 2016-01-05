//
//  Called whenever we open the file upload dialog to enable ajax file uploads.
//
function applyFileUploadEventListener()
{
    $('#fileupload').fileupload({
        dataType: "json",
        url: BASE_AJAX_PATH + "Api/Upload/Image",
        limitConcurrentUploads: 1,
        sequentialUploads: true,
        progressInterval: 100,
        maxChunkSize: 500000,
        add: function (e, data)
        {
            data.context = $('<div />').text(data.files[0].name).appendTo('#filelistholder');
            $('<span class="span-width-percentage"> - 0%</span>').appendTo(data.context);
            $('</div><div class="progress"><div class="progress-bar" role="progressbar" style="width:0%"></div></div>').appendTo(data.context);
            $('#btnUploadAll').click(function ()
            {
                data.submit();
            });
        },
        done: function (e, data)
        {
                $.ajax({
                    url: BASE_AJAX_PATH + "Api/Upload?origFileName=" + encodeURIComponent(data.files[0].name),
                    type: "post",
                    success: function (response)
                    {
                        viewModel.imageList().ImageList.unshift(ko.observable(ko.mapping.fromJSON(response)));

                        data.context.text(data.files[0].name + '... Completed');
                        $('</div><div class="progress"><div class="progress-bar" role="progressbar" style="width:100%"></div></div>').appendTo(data.context);
                    },
                    error: function ()
                    {
                        alert('Unable to upload file, please remember the maximum file size is 5mb.');
                    }
                });
        },
        progressall: function (e, data)
        {
            //var progress = parseInt(data.loaded / data.total * 100, 10);
            //$('#overallbar').css('width', progress + '%');
        },
        progress: function (e, data)
        {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            data.context.find('.span-width-percentage').html(' - ' + progress + '%');
            data.context.find('.progress-bar').css('width', progress + '%');
        }
    });
}