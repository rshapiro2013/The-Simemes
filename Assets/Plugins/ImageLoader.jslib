mergeInto(LibraryManager.library, {

  OpenFileSelector: function() {
            var input = document.createElement('input');
            input.type = 'file';
            input.accept = 'image/*'; // Only accept image files

            input.onchange = function (event) {
                var file = event.target.files[0];
                if (file) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var imageData = e.target.result;
                        // Send base64 image data to Unity
                        unityInstance.SendMessage('ImageLoader', 'OnImageSelected', imageData);
                    };
                    reader.readAsDataURL(file); // Read file as base64
                }
            };

            input.click(); // Trigger file input click
        },

});