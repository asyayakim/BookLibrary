const API_URL = 'http://localhost:5066/api/Book';
const contentDiv = document.getElementById('content');
// const logout = document.getElementById('header');
export function renderAddBookPage() {
    contentDiv.innerHTML = `
        <h2>Add a New Book</h2>
        <form id="addBookForm">
            <label for="title">Title:</label>
            <input type="text" id="title" name="title" required>
            
            <label for="author">Author:</label>
            <input type="text" id="author" name="author" required>
            
            <label for="isbn">ISBN:</label>
            <input type="text" id="isbn" name="isbn" required>
            
            <label for="totalCopies">Total Copies:</label>
            <input type="number" id="totalCopies" name="totalCopies" required>
            
            <label for="coverImageUrl">Cover Image URL:</label>
            <input type="url" id="coverImageUrl" name="coverImageUrl" required>
            
            <button type="submit">Add Book</button>
        </form>
    `;

    addBookForm.addEventListener('submit', async (event) => {
        event.preventDefault();

        const title = document.getElementById('title').value;
        const author = document.getElementById('author').value;
        const isbn = document.getElementById('isbn').value;
        const totalCopies = parseInt(document.getElementById('totalCopies').value, 10);
        const coverImageUrl = document.getElementById('coverImageUrl').value;

        if (title && author && isbn && totalCopies && coverImageUrl) {
            try {
                const response = await fetch(API_URL, {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({title, author, isbn, totalCopies, coverImageUrl}),
                });

                if (response.ok) {
                    alert('Book added successfully!');
                    document.getElementById('addBookForm').reset(); // Clear the form
                    await fetchBooks(); // Refresh the book list
                } else {
                    console.error('Failed to add book');
                    alert('Failed to add book. Please try again.');
                }
            } catch (error) {
                console.error('Error adding book:', error);
            }
        } else {
            alert('Please fill out all fields.');
        }
    });
}
 //addBookPageBtn.addEventListener('click', renderAddBookPage);