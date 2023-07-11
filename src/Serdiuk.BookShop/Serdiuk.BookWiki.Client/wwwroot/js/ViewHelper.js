class ViewHelper {

    static FromBase64ToImg(imageData) {
        var base64Data = btoa(String.fromCharCode.apply(null, new Uint8Array(imageData)));

        var imageUrl = 'data:image/png;base64,' + base64Data;
        return imageUrl;
    }
    static NoramlizeAuthorArray(authors) {
        var normalizedArray = [];

        authors.forEach(function (author) {
            var fullName = author.firstName + " " + author.lastName;
            var normalizedAuthor = {
                id: author.id,
                fullName: fullName
            };
            normalizedArray.push(normalizedAuthor);
        });

        return normalizedArray
    }
}