import { updateViewLogin } from "./login.js";
import { fetchBooks } from "./viewLibraryPage.js";
import { renderAddBookPage } from "./addBook.js";
import { renderAdminDashboard } from "./adminDashboard.js";
import { model } from "./model.js";


export function updateView() {
    const page = model.app.currentPage;

    if (page === "login") {
        updateViewLogin(); 
    } else if (page === "homeLibrary") {
        fetchBooks(); 
    } else if (page === "adminDashboard") {
        renderAdminDashboard();
    } else if (page === "addBook") {
        renderAddBookPage();
    } else {
        document.getElementById("content").innerHTML = `<h1>Page not found</h1>`;
    }
}
