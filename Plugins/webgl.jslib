mergeInto(LibraryManager.library, {
  GetWindowLocationAsJson: function () {
    var str = JSON.stringify(window.location)
    
    var bufferSize = lengthBytesUTF8(str) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(str, buffer, bufferSize);
    return buffer;
  },
})
