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

    static fetchLikedBook(url) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('access'),
                },
                method: "GET",
                success: function (result) {
                    resolve(result);
                },
                error: function (error) {
                    reject(error);
                }
            })
        });
    }
}