class BookService {

     static likeToBook(book, url) {
        let likes = book.rating;
        let likeUrl = url + "?bookId=" + book.id;

        return new Promise((resolve, reject) => {
            $.ajax({
                url: likeUrl,
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('access'),
                },
                contentType: "application/json",
                method: "PUT",
                success: function (result) {
                    likes = result;
                    resolve(likes);
                },
                error: function (error) {
                    reject(error);
                }
            });
        });
    }
}