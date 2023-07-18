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

    static includeBooks(book, container, likeUrl) {

        let bookDiv = $('<div>').addClass('row border my-4 rounded border-secondary ');

        let figure = $('<figure>').addClass('figure border p-1');
        let cover = $('<img>')
            .addClass('img-fluid mx-auto d-block')
            .prop('src', 'data:image/png;base64,' + book.cover)
            .appendTo(figure);

        let authors = ViewHelper.NoramlizeAuthorArray(book.authors);

        authors.map(author => {
            let figureCaption = $('<a>')
                .text(author.fullName)
                .addClass(' mx-1')
                .prop('href', `/author/${author.id}`)
                .appendTo(figure);
        })

        let coverDiv = $('<div>').addClass('col-4 m-2').append(figure);
        bookDiv.append(coverDiv);

        let name = $('<h4>').addClass('col').text(book.name);
        let nameDiv = $('<div>').addClass('col-12 row').append(name);
        let rating = $('<p>').addClass('h6 col-6').text(`Book likes: ${book.rating}`)
        console.log(book);
        if (TokenClass.canUseToken()) {


            let like = $('<i>').addClass(`bi ${book.youLikeIt ? 'bi-heart-fill' : 'bi-heart'} col-1`).appendTo(nameDiv);

            like.click(function () {
                like.toggleClass('bi-heart bi-heart-fill');

                BookService.likeToBook(book, likeUrl).then(x => x).then(newLikes => { rating.text('Book likes: ' + newLikes); });

               
            });
        }

        let desc = $('<p>').addClass('h6 col-10 lead').text(book.description);
        let descDiv = $('<div>').addClass('col').append(desc);

        let textDiv = $('<div>').addClass('col parent-div d-flex flex-column').append(nameDiv, descDiv);
        bookDiv.append(textDiv);

        let manageButton = $('<a>').addClass('btn btn-info col-6').text('View more information')
            .prop('href', `/book/get/${book.id}`)

        let bottomPanel = $('<div>').addClass('child-div my-4 row')
            .append(manageButton, rating).appendTo(textDiv);

        container.append(bookDiv);
    }
}