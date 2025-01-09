const API_URL = 'http://localhost:5066/api/Book';
const contentDiv = document.getElementById('content');
const viewLibraryPageBtn = document.getElementById('viewLibraryPage');

export async function fetchBooks() {
    try {
        const response = await fetch(API_URL);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const books = await response.json();
        renderBooks(books);
    } catch (error) {
        console.error('Error fetching books:', error);
    }
}

function renderBooks(books) {
    contentDiv.innerHTML = '';
    books.forEach(book => {
        const bookDiv = document.createElement('div');
        bookDiv.className = 'book';
        bookDiv.innerHTML = `
        
        <img src="${book.coverImageUrl}" alt="${book.title}" style="width:100%; height:auto; border-radius:5px; margin-bottom:10px;">
            <div class="book-info">
                <h3>${book.title}</h3>
                <p><strong>Author:</strong> ${book.author}</p>
                <p><strong>ISBN:</strong> ${book.isbn}</p>
                <p><strong>Total Copies:</strong> ${book.totalCopies}</p>
                <button onclick="deleteBook(${book.id})" style="background-color: #e75c5c; border: none; padding: 0.5rem 1rem; color: white; cursor: pointer; border-radius: 5px;">Delete</button>
            </div>
    `;
        contentDiv.appendChild(bookDiv);
    });
}
async function deleteBook(id) {
    try {
        const response = await fetch(`${API_URL}/${id}`, {method: 'DELETE'});
        if (response.ok) {
            fetchBooks();
        } else {
            console.error('Failed to delete book');
        }
    } catch (error) {
        console.error('Error deleting book:', error);
    }
}
window.deleteBook = deleteBook;
// viewLibraryPageBtn.addEventListener('click', fetchBooks);