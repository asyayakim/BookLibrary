import { model } from "./model.js";
import {updateView} from "./main.js";
import {fetchBooks} from "./viewLibraryPage.js";

export function renderAdminDashboard() {
    document.getElementById('header').innerHTML = `
        <button id="viewLibraryPage">View Library</button>
        <button id="addBookPage">Add a Book</button>
        <button id="logoutButton">Log Out</button>
    `;
    document.getElementById('content').innerHTML = `
        <h2>Welcome, Admin!</h2>
        `;
        fetchBooks();
    document.getElementById('viewLibraryPage').addEventListener('click', () => {
        model.app.currentPage = 'homeLibrary';
        updateView();
    });
    document.getElementById('addBookPage').addEventListener('click', () => {
        model.app.currentPage = 'addBook';
        updateView();
    });
    document.getElementById('logoutButton').addEventListener('click', () => {
        model.app.loggedInUser = null;
        model.app.currentPage = 'login';
        updateView();
    });
}
